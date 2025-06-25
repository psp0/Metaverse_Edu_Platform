using UnityEngine.UIElements;
using UnityEngine;

namespace Demos
{

    /// <summary>
    /// The UxmlElement Attribute makes the custom element available in the UI Builder and UXML.
    /// </summary>
    [UxmlElement]
    public partial class ArcVectorElement : VisualElement
    {
        [UxmlAttribute] public float LineWidth { get; set; } = 10f;
        [UxmlAttribute] public Color StrokeColor { get; set; } = Color.white;
        [UxmlAttribute] public Color FillColor { get; set; }  = new Color(0f, 127f / 255f, 217f / 255f, 1f);
        
        [UxmlAttribute] public Vector2 StartPosition { get; set;  }
        [UxmlAttribute] public float Radius { get; set; } = 250f;
        [UxmlAttribute] public float StartAngle { get; set; } = 0f;
        [UxmlAttribute] public float EndAngle { get; set; } = 90f;
        
        public ArcVectorElement()
        {
            generateVisualContent += OnGenerateVisualContent;
        }

        /// <summary>
        /// Draw an arc using the MeshGenerationContext.
        /// </summary>
        /// <param name="ctx">The MeshGenerationContext associated with this element.</param>
        void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            
            // if (FillColor == default(Color))
            //     FillColor = new Color(0f, 127f / 255f, 217f / 255f, 1f); 
            
            var painter2D = ctx.painter2D;

            painter2D.lineWidth = LineWidth;
            painter2D.strokeColor = StrokeColor;
            painter2D.fillColor = FillColor;
            
            // Initialize to new Color(0f, 127f / 255f, 217f / 255f, 1f);

            painter2D.BeginPath();

            // Move to the arc center
            painter2D.MoveTo(StartPosition);

            // Draw the arc, and close the path
            painter2D.Arc(StartPosition, Radius, StartAngle, EndAngle);
            painter2D.ClosePath();

            // Fill and stroke the path
            painter2D.Fill();
            painter2D.Stroke();
        }
    }

}