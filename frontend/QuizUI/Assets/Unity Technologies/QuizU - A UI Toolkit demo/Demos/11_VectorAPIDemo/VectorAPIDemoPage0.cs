using Demos;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class VectorAPIDemoPage0 : DemoBase
{

    const string k_RootName = "demo__page-0";
    Slider m_LineWidthSlider;

    // Elements that can adjust line width
    ZigZagVectorElement m_ZigZagVectorElement;
    QuadraticCurveVectorElement m_CurveVectorElement;
    ArcVectorElement m_ArcVectorElement1;
    ArcVectorElement m_ArcVectorElement2;
    PolygonVectorElement m_PolygonVectorElement1;
    PolygonVectorElement m_PolygonVectorElement2;

    /// <summary>
    /// Set up references to UI Elements
    /// </summary>
    protected override void SetVisualElements()
    {
        base.SetVisualElements();
        VisualElement pageRoot = m_Document.rootVisualElement.Q<VisualElement>(k_RootName);

        m_LineWidthSlider = pageRoot.Q<Slider>("demo__line-width-slider");
        m_ZigZagVectorElement = pageRoot.Q<ZigZagVectorElement>("line-seg__element");
        m_CurveVectorElement = pageRoot.Q<QuadraticCurveVectorElement>("curve__quadratic");
        m_ArcVectorElement1 = pageRoot.Q<ArcVectorElement>("arc__element-1");
        m_ArcVectorElement2 = pageRoot.Q<ArcVectorElement>("arc__element-2");
        m_PolygonVectorElement1 = pageRoot.Q<PolygonVectorElement>("polygon__element-1");
        m_PolygonVectorElement2 = pageRoot.Q<PolygonVectorElement>("polygon__element-2");

        // Update elements to use the Line Width Slider
        m_LineWidthSlider.RegisterValueChangedCallback(OnLineWidthSliderValueChanged);

        UpdateLineWidth(m_LineWidthSlider.value);

    }

    /// <summary>
    /// Update method for slider.
    /// </summary>
    /// <param name="evt"></param>
    void OnLineWidthSliderValueChanged(ChangeEvent<float> evt)
    {
        UpdateLineWidth(evt.newValue);
    }

    /// <summary>
    /// Change the line width of specified elements.
    /// </summary>
    /// <param name="widthValue"The new width value</param>
    void UpdateLineWidth(float widthValue)
    {
        m_ZigZagVectorElement.LineWidth = widthValue;
        m_CurveVectorElement.LineWidth = widthValue;
        m_ArcVectorElement1.LineWidth = widthValue;
        m_ArcVectorElement2.LineWidth = widthValue;
        m_PolygonVectorElement1.LineWidth = widthValue;
        m_PolygonVectorElement2.LineWidth = widthValue;

        m_ZigZagVectorElement.MarkDirtyRepaint();
        m_CurveVectorElement.MarkDirtyRepaint();
        m_ArcVectorElement1.MarkDirtyRepaint();
        m_ArcVectorElement2.MarkDirtyRepaint();
        m_PolygonVectorElement1.MarkDirtyRepaint();
        m_PolygonVectorElement2.MarkDirtyRepaint();
    }
}
