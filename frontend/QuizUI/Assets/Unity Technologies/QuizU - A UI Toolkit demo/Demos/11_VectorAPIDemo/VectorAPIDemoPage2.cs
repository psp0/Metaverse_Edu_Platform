using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// This controls the line segment behavior on demo-page-2
    /// </summary>
    public class VectorAPIDemoPage2 : DemoBase
    {
        // Defines the class name of draggable elements.
        const string k_DraggableClass = "draggable";
        const string k_RootName = "demo__page-2";

        // List of VisualElement objects that are draggable.
        List<VisualElement> m_DraggableElements;

        // Line demo elements
        VisualElement m_StartPoint;
        VisualElement m_EndPoint;
        VisualElement m_LineContainer;
        Slider m_LineWidthSlider;
        ArcVectorElement m_ArcVectorElement;

        // Curve demo elements
        VisualElement m_CurveStart;
        VisualElement m_CurveControl;
        VisualElement m_CurveEnd;
        VisualElement m_CurveContainer;

        bool m_IsLineInitialized;

        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            // Store all elements containing the k_DraggableClass name in a list
            m_DraggableElements = m_Document.rootVisualElement.Query(className: k_DraggableClass).ToList();

            VisualElement pageRoot = m_Document.rootVisualElement.Q<VisualElement>(k_RootName);

            // Line elements
            m_StartPoint = pageRoot.Q<VisualElement>("line__start-point");
            m_EndPoint = pageRoot.Q<VisualElement>("line__end-point");
            m_LineContainer = pageRoot.Q<VisualElement>("line__container");

            // Curve elements
            m_CurveStart = pageRoot.Q<VisualElement>("curve__start-point");
            m_CurveControl = pageRoot.Q<VisualElement>("curve__control-point");
            m_CurveEnd = pageRoot.Q<VisualElement>("curve__end-point");
            m_CurveContainer = pageRoot.Q<VisualElement>("curve__container");

            // Delay setting up the line until the geometry is loaded
            m_LineContainer.RegisterCallback<GeometryChangedEvent>(evt => SetupLineManipulators());


        }



        /// <summary>
        /// Sets up the elements on page 2 with draggable control points
        /// </summary>
        void SetupLineManipulators()
        {
            if (m_IsLineInitialized || m_StartPoint == null || m_EndPoint == null)
                return;

            if (m_CurveStart == null || m_CurveControl == null || m_CurveEnd == null || m_CurveContainer == null)
                return;

            // Set up the line
            _ = new LineController(
                m_LineContainer,
                m_StartPoint,
                m_EndPoint,
                makeDraggable: true);


            // Set up the quadratic curve
            _ = new QuadraticCurveController(
                m_CurveContainer,
                m_CurveStart,
                m_CurveControl,
                m_CurveEnd,
                makeDraggable: true);

            m_IsLineInitialized = true;
        }
    }
}