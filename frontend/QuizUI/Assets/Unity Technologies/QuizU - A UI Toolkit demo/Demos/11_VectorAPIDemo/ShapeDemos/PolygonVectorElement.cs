using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// A custom UI element that draws an N-sided polygon using Vector API.
    /// </summary>
    [UxmlElement]
    public partial class PolygonVectorElement : VisualElement
    {
        [UxmlAttribute] public int Sides { get; set; } // Number of sides (3 = Triangle, 4 = Square, etc.)
        [UxmlAttribute] public float Radius { get; set; } // Size from the center of the polygon
        [UxmlAttribute] public float LineWidth { get; set; }
        [UxmlAttribute] public float Rotation { get; set; } // Rotation in degrees
        [UxmlAttribute] public Color StrokeColor { get; set; }
        [UxmlAttribute] public Color FillColor { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PolygonVectorElement()
        {
            // Default values
            Sides = 5; // Pentagon
            Radius = 50f;
            LineWidth = 5f;
            Rotation = 0f;
            StrokeColor = Color.white;
            FillColor = new Color32(0, 127, 217, 255);

            generateVisualContent += OnGenerateVisualContent;
        }

        /// <summary>
        /// Generates the visual content for the polygon.
        /// </summary>
        /// <param name="context">Provides access to the drawing surface.</param>
        void OnGenerateVisualContent(MeshGenerationContext context)
        {
            // Must be at least a triangle
            if (Sides < 3) 
                return; 

            var painter2D = context.painter2D;
            painter2D.fillColor = FillColor;
            painter2D.strokeColor = StrokeColor;
            painter2D.lineWidth = LineWidth;

            float centerX = layout.width / 2;
            float centerY = layout.height / 2;
            float angleStep = 2 * Mathf.PI / Sides;
            float rotationRad = Rotation * Mathf.Deg2Rad;

            // Generate polygon points
            Vector2[] points = new Vector2[Sides];
            
            for (int i = 0; i < Sides; i++)
            {
                float angle = i * angleStep + rotationRad;
                points[i] = new Vector2(
                    centerX + Mathf.Cos(angle) * Radius,
                    centerY + Mathf.Sin(angle) * Radius
                );
            }

            // Draw the polygon
            painter2D.BeginPath();
            painter2D.MoveTo(points[0]);
            for (int i = 1; i < Sides; i++)
            {
                painter2D.LineTo(points[i]);
            }
            painter2D.ClosePath();

            painter2D.Fill();
            painter2D.Stroke();
        }
    }
}
