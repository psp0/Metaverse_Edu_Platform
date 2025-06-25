using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace Demos
{
    /// <summary>
    /// Connects three VisualElements to a QuadraticCurveVectorElement and keeps the curve updated as they move.
    /// </summary>
    public class QuadraticCurveController
    {
        readonly QuadraticCurveVectorElement m_Element;
        readonly VisualElement m_StartElement;
        readonly VisualElement m_ControlElement;
        readonly VisualElement m_EndElement;

        /// <summary>
        /// Triggered when the curve points are updated.
        /// </summary>
        public event Action CurveMoved;

        public QuadraticCurveVectorElement Element => m_Element;

        /// <summary>
        /// Initializes the controller to bind UI elements to the curve.
        /// </summary>
        /// <param name="container">Container to add the curve element to.</param>
        /// <param name="startElement">VisualElement representing the start point.</param>
        /// <param name="controlElement">VisualElement representing the control point.</param>
        /// <param name="endElement">VisualElement representing the end point.</param>
        /// <param name="makeDraggable">If true, adds drag manipulators to all points.</param>
        public QuadraticCurveController(
            VisualElement container,
            VisualElement startElement,
            VisualElement controlElement,
            VisualElement endElement,
            bool makeDraggable = true)
        {
            m_StartElement = startElement;
            m_ControlElement = controlElement;
            m_EndElement = endElement;

            // Create and configure the curve element
            Vector2 start = GetCenter(startElement);
            Vector2 control = GetCenter(controlElement);
            Vector2 end = GetCenter(endElement);

            m_Element = new QuadraticCurveVectorElement(start, control, end);
            m_Element.SendToBack();
            container.Add(m_Element);

            if (makeDraggable)
            {
                startElement.AddManipulator(new SimpleDragManipulator());
                controlElement.AddManipulator(new SimpleDragManipulator());
                endElement.AddManipulator(new SimpleDragManipulator());

                startElement.BringToFront();
                controlElement.BringToFront();
                endElement.BringToFront();
            }

            // Watch for changes to element geometry
            startElement.RegisterCallback<GeometryChangedEvent>(_ => UpdateCurve());
            controlElement.RegisterCallback<GeometryChangedEvent>(_ => UpdateCurve());
            endElement.RegisterCallback<GeometryChangedEvent>(_ => UpdateCurve());

            // Initial update
            UpdateCurve();
        }

        /// <summary>
        /// Updates the curve element with the current control point positions.
        /// </summary>
        public void UpdateCurve()
        {
            m_Element.UpdateStartPoint(GetCenter(m_StartElement));
            m_Element.UpdateControlPoint(GetCenter(m_ControlElement));
            m_Element.UpdateEndPoint(GetCenter(m_EndElement));

            CurveMoved?.Invoke();
        }

        /// <summary>
        /// Computes the center of a VisualElement using layout and resolved width/height.
        /// </summary>
        static Vector2 GetCenter(VisualElement el)
        {
            return el.layout.position + new Vector2(el.resolvedStyle.width / 2, el.resolvedStyle.height / 2);
        }
    }
}