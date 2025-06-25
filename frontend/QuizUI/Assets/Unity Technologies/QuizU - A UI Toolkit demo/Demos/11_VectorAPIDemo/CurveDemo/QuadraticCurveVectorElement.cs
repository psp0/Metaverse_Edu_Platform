using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Draws a quadratic Bézier curve between two points on screen using the Vector API.
    /// </summary>
    [UxmlElement]
    public partial class QuadraticCurveVectorElement : VisualElement
    {
        [UxmlAttribute] public Vector2 StartPoint { get; set; }
        [UxmlAttribute] public Vector2 ControlPoint { get; set; }
        [UxmlAttribute] public Vector2 EndPoint { get; set; }
        [UxmlAttribute] public float LineWidth { get; set; }
        [UxmlAttribute] public Color StrokeColor { get; set; }
        

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuadraticCurveVectorElement()
        {
            // Reasonable defaults for standalone testing or UXML fallback
            StartPoint = new Vector2(0, 100);
            ControlPoint = new Vector2(50, 0);
            EndPoint = new Vector2(100, 100);
            LineWidth = 10f;
            StrokeColor = new Color(0f, 127f / 255f, 217f / 255f, 1f);

            style.width = new StyleLength(Length.Percent(100));
            style.height = new StyleLength(Length.Percent(100));
            pickingMode = PickingMode.Ignore;

            generateVisualContent += OnGenerateVisualContent;
        }

        /// <summary>
        /// Manual constructor to pass start, control, and end points.
        /// </summary>
        public QuadraticCurveVectorElement(Vector2 startPoint, Vector2 controlPoint, Vector2 endPoint) : this()
        {
            StartPoint = startPoint;
            ControlPoint = controlPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        /// Generates the quadratic Bézier curve visuals.
        /// </summary>
        /// <param name="ctx">Mesh generation context for drawing.</param>
        void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;

            painter.strokeColor = StrokeColor;
            painter.lineWidth = LineWidth;

            // Draw the quadratic Bézier curve
            painter.BeginPath();
            painter.MoveTo(StartPoint);
            painter.QuadraticCurveTo(ControlPoint, EndPoint);
            painter.Stroke();
        }

        /// <summary>
        /// Updates the start point of the quadratic Bézier curve and repaints.
        /// </summary>
        /// <param name="newStart">New start point position.</param>
        public void UpdateStartPoint(Vector2 newStart)
        {
            StartPoint = newStart;
            MarkDirtyRepaint();
        }

        /// <summary>
        /// Updates the control point of the quadratic Bézier curve and repaints.
        /// </summary>
        /// <param name="newControl">New control point position.</param>
        public void UpdateControlPoint(Vector2 newControl)
        {
            ControlPoint = newControl;
            MarkDirtyRepaint();
        }

        /// <summary>
        /// Updates the end point of the quadratic Bézier curve and repaints.
        /// </summary>
        /// <param name="newEnd">New end point position.</param>
        public void UpdateEndPoint(Vector2 newEnd)
        {
            EndPoint = newEnd;
            MarkDirtyRepaint();
        }
    }
}