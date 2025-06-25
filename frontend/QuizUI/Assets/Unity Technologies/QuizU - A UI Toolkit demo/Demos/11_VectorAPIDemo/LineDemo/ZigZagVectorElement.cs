using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// A custom VisualElement that draws a  zig-zag pattern.
    ///
    /// Modified from version here:
    /// https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-generate-2d-visual-content.html
    /// </summary>
    [UxmlElement]
    public partial class ZigZagVectorElement : VisualElement
    {
        [UxmlAttribute]
        public Color Color { get; set; }

        [UxmlAttribute]
        public int Oscillations { get; set; }

        [UxmlAttribute]
        public float LineWidth { get; set; }

        [UxmlAttribute]
        public float AmplitudeFactor { get; set; }

        [UxmlAttribute]
        public float StartXOffset { get; set; }

        public ZigZagVectorElement()
        {
            Color = Color.white;
            Oscillations = 5;
            LineWidth = 10.0f;
            AmplitudeFactor = 0.25f;
            StartXOffset = 0.0f;

            generateVisualContent += OnGenerateVisualContent;
            RegisterCallback<GeometryChangedEvent>(evt => MarkDirtyRepaint());

            style.flexGrow = 1;
            style.minHeight = 100;
            style.minWidth = 100;
            style.flexShrink = 1;
            style.flexBasis = 0;
        }

        private void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter2D = ctx.painter2D;

            if (painter2D == null || resolvedStyle.width <= 0 || resolvedStyle.height <= 0)
                return;

            float width = resolvedStyle.width;
            float height = resolvedStyle.height;
            float step = width / (Oscillations * 2);
            float amplitude = height * AmplitudeFactor;

            painter2D.lineWidth = LineWidth;
            painter2D.strokeColor = Color;
            painter2D.lineJoin = LineJoin.Round;
            painter2D.lineCap = LineCap.Round;

            painter2D.BeginPath();

            float startX = StartXOffset * width;
            Vector2 startPoint = new Vector2(startX, height / 2);
            painter2D.MoveTo(startPoint);

            for (int i = 1; i <= Oscillations * 2; i++)
            {
                float x = startX + (i * step);
                float y = (i % 2 == 0) ? startPoint.y : startPoint.y - amplitude;
                painter2D.LineTo(new Vector2(x, y));
            }

            painter2D.Stroke();
        }
    }
}