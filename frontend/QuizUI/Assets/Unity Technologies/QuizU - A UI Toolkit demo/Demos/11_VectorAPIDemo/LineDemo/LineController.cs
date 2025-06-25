using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace Demos
{
    /// <summary>
    /// Updates a LineVectorElement drawn between two VisualElements.
    /// </summary>
    public class LineController
    {
        // Line segment element
         LineVectorElement m_LineSegment;
         
         // Elements to connet
         VisualElement m_StartElement;
         VisualElement m_EndElement;

         public event Action LineMoved;

         public LineVectorElement LineElement => m_LineSegment;
         
         /// <summary>
         /// Constructor.
         /// </summary>
         /// <param name="container">The container to add the line segment.</param>
         /// <param name="startElement">The element that is the starting point.</param>
         /// <param name="endElement">The element that is the end point</param>
         /// <param name="makeDraggable">Makes the end points draggable.</param>
         public LineController(VisualElement container, VisualElement startElement, VisualElement endElement, bool makeDraggable = true)
         {
             m_StartElement = startElement;
             m_EndElement = endElement;

             // Create and configure the LineVectorElement
             Vector2 start = GetCenter(startElement);
             Vector2 end = GetCenter(endElement);
             m_LineSegment = new LineVectorElement(start, end);
             m_LineSegment.SendToBack();
             container.Add(m_LineSegment);

             // Draggable behavior
             if (makeDraggable)
             {
                 startElement.AddManipulator(new SimpleDragManipulator());
                 endElement.AddManipulator(new SimpleDragManipulator());

                 startElement.BringToFront();
                 endElement.BringToFront();
             }

             // Update when the geometry changes
             startElement.RegisterCallback<GeometryChangedEvent>(_ => UpdateLine());
             endElement.RegisterCallback<GeometryChangedEvent>(_ => UpdateLine());

             UpdateLine();
         }
         
        
        public void UpdateLine()
        {
            Vector2 start = GetCenter(m_StartElement);
            Vector2 end = GetCenter(m_EndElement);

            m_LineSegment.UpdateStartPoint(start);
            m_LineSegment.UpdateEndPoint(end);
            LineMoved?.Invoke();
        }
        
        /// <summary>
        /// Calculates the center point of a VisualElement.
        /// </summary>
        static Vector2 GetCenter(VisualElement element)
        {
            return element.layout.position + new Vector2(element.resolvedStyle.width / 2, element.resolvedStyle.height / 2);
        }
    }
}