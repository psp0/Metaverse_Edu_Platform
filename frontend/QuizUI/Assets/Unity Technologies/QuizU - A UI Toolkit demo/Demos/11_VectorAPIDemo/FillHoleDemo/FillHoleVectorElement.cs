using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Draws a filled rectangle with an optional diamond-shaped hole using the Vector API.
    /// 
    /// This is a variation of the simpler example found in the documentation:
    /// https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-generate-2d-visual-content.html
    /// </summary>
    [UxmlElement]
    public partial class FillHoleVectorElement : VisualElement
    {
        FillRule m_FillRule = FillRule.OddEven;
        bool m_ReverseWinding = false;
        bool m_ShowArrows = true;
        float m_ArrowAnimT = 0f;

        const float k_ArrowAnimSpeed = 0.5f; // 0–1 per second


        [UxmlAttribute]
        public FillRule FillRule
        {
            get => m_FillRule;
            set
            {
                if (m_FillRule != value)
                {
                    m_FillRule = value;
                    MarkDirtyRepaint();
                }
            }
        }

        [UxmlAttribute]
        public bool ReverseWinding
        {
            get => m_ReverseWinding;
            set
            {
                if (m_ReverseWinding != value)
                {
                    m_ReverseWinding = value;
                    MarkDirtyRepaint();
                }
            }
        }


        [UxmlAttribute]
        public bool ShowArrows
        {
            get => m_ShowArrows;
            set
            {
                if (m_ShowArrows != value)
                {
                    m_ShowArrows = value;
                    MarkDirtyRepaint();
                }
            }
        }

        // Outer rectangle corners
        [UxmlAttribute] public Vector2 OuterTopLeft { get; set; } = new(10, 10);
        [UxmlAttribute] public Vector2 OuterTopRight { get; set; } = new(300, 10);
        [UxmlAttribute] public Vector2 OuterBottomRight { get; set; } = new(300, 150);
        [UxmlAttribute] public Vector2 OuterBottomLeft { get; set; } = new(10, 150);

        // Inner diamond points
        [UxmlAttribute] public Vector2 InnerTop { get; set; } = new(150, 50);
        [UxmlAttribute] public Vector2 InnerRight { get; set; } = new(175, 75);
        [UxmlAttribute] public Vector2 InnerBottom { get; set; } = new(150, 100);
        [UxmlAttribute] public Vector2 InnerLeft { get; set; } = new(125, 75);

        public FillHoleVectorElement()
        {
            style.width = Length.Percent(100);
            style.height = Length.Percent(100);
            pickingMode = PickingMode.Ignore;

            generateVisualContent += OnGenerateVisualContent;

            // Arrow animation
            _ = schedule.Execute(() =>
            {
                m_ArrowAnimT += k_ArrowAnimSpeed * Time.deltaTime;
                if (m_ArrowAnimT > 1f)
                    m_ArrowAnimT -= 1f;

                if (ShowArrows)
                    MarkDirtyRepaint();
            }).Every(16);
        }

        void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;
            painter.fillColor = new Color(0f, 127f / 255f, 217f / 255f, 1f);

            painter.BeginPath();

            // Outer rectangle (always drawn clockwise)
            painter.MoveTo(OuterTopLeft);
            painter.LineTo(OuterTopRight);
            painter.LineTo(OuterBottomRight);
            painter.LineTo(OuterBottomLeft);
            painter.ClosePath();

            // Inner diamond (direction based on ReverseWinding)
            if (!ReverseWinding)
            {
                painter.MoveTo(InnerTop);
                painter.LineTo(InnerRight);
                painter.LineTo(InnerBottom);
                painter.LineTo(InnerLeft);
            }
            else
            {
                painter.MoveTo(InnerTop);
                painter.LineTo(InnerLeft);
                painter.LineTo(InnerBottom);
                painter.LineTo(InnerRight);
            }

            painter.ClosePath();
            painter.Fill(FillRule);

            if (ShowArrows)
            {
                // Outer rectangle
                painter.strokeColor = Color.white;
                painter.lineWidth = 4f;

                DrawArrow(painter, OuterTopLeft, OuterTopRight, m_ArrowAnimT);
                DrawArrow(painter, OuterTopRight, OuterBottomRight, m_ArrowAnimT);
                DrawArrow(painter, OuterBottomRight, OuterBottomLeft, m_ArrowAnimT);
                DrawArrow(painter, OuterBottomLeft, OuterTopLeft, m_ArrowAnimT);

                // Inner diamond
                painter.strokeColor = Color.cyan;

                if (!ReverseWinding)
                {
                    DrawArrow(painter, InnerTop, InnerRight, m_ArrowAnimT);
                    DrawArrow(painter, InnerRight, InnerBottom, m_ArrowAnimT);
                    DrawArrow(painter, InnerBottom, InnerLeft, m_ArrowAnimT);
                    DrawArrow(painter, InnerLeft, InnerTop, m_ArrowAnimT);
                }
                else
                {
                    DrawArrow(painter, InnerTop, InnerLeft, m_ArrowAnimT);
                    DrawArrow(painter, InnerLeft, InnerBottom, m_ArrowAnimT);
                    DrawArrow(painter, InnerBottom, InnerRight, m_ArrowAnimT);
                    DrawArrow(painter, InnerRight, InnerTop, m_ArrowAnimT);
                }
            }
        }
        void DrawArrow(Painter2D painter, Vector2 from, Vector2 to, float t, float size = 10f)
        {
            Vector2 dir = (to - from).normalized;
            Vector2 perp = new Vector2(-dir.y, dir.x);

            Vector2 tip = Vector2.Lerp(from, to, t);
            Vector2 left = tip - dir * size + perp * size * 0.5f;
            Vector2 right = tip - dir * size - perp * size * 0.5f;

            painter.BeginPath();
            painter.MoveTo(left);
            painter.LineTo(tip);
            painter.LineTo(right);
            painter.Stroke();
        }
    }
}
