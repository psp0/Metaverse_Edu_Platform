using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Draws a line between two points on screen using the Vector API;
    /// </summary>
    [UxmlElement]
    public partial class LineVectorElement: VisualElement
    {
        [UxmlAttribute]
        public Vector2 StartPoint { get; set; }
        
        [UxmlAttribute]
        public Vector2 EndPoint { get; set; }

        [UxmlAttribute] public float LineThickness { get; set; }

        public LineVectorElement()
        {
            LineThickness = 10f;

            // Set a size that fits the full drawing area
            style.width = new StyleLength(Length.Percent(100));
            style.height = new StyleLength(Length.Percent(100));
            
            // Non interactive element
            this.pickingMode = PickingMode.Ignore;
        }
        
        public LineVectorElement(Vector2 initialStartPoint, Vector2 initialEndPoint): this()
        {
            StartPoint = initialStartPoint;
            EndPoint = initialEndPoint;
            
            // Register for the paint event
            generateVisualContent += OnGenerateVisualContent;
        }

        void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;

            painter.strokeColor = new Color(0f, 127f / 255f, 217f / 255f, 1f);
            painter.lineWidth =  LineThickness;

            // Draw the line between the start and end points
            painter.BeginPath();
            painter.MoveTo(StartPoint);
            painter.LineTo(EndPoint);
            painter.Stroke();
        }
        
        // Methods to update the positions of the line's endpoints
        public void UpdateStartPoint(Vector2 newStart)
        {
            StartPoint = newStart;
            MarkDirtyRepaint(); // Request a repaint after changes
        }

        public void UpdateEndPoint(Vector2 newEnd)
        {
            EndPoint = newEnd;
            MarkDirtyRepaint(); // Request a repaint after changes
        }

    }

}