using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Controls the hole fill demo on demo-page-3
    /// </summary>
    public class VectorAPIDemoPage3 : DemoBase
    {
        const string k_RootName = "demo__page-3";

        Toggle m_ReverseWindingToggle;
        Toggle m_ShowArrowsToggle;

        FillHoleVectorElement m_CanvasNonzero;
        FillHoleVectorElement m_CanvasOddeven;

        Label m_LabelNonzero;
        Label m_LabelOddeven;

        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            var root = m_Document.rootVisualElement.Q<VisualElement>(k_RootName);

            m_ReverseWindingToggle = root.Q<Toggle>("toggle__reverse-winding");
            m_ShowArrowsToggle = root.Q<Toggle>("toggle__show-arrows");

            m_CanvasNonzero = root.Q<FillHoleVectorElement>("fill__canvas-nonzero");
            m_CanvasOddeven = root.Q<FillHoleVectorElement>("fill__canvas-oddeven");

            m_LabelNonzero = root.Q<Label>("label__nonzero-status");
            m_LabelOddeven = root.Q<Label>("label__oddeven-status");

            if (m_CanvasNonzero == null)
                Debug.LogWarning("[VectorAPIDemoPage3]: Missing FillHoleElement with name 'fill__canvas-nonzero'");
            else
                m_CanvasNonzero.FillRule = FillRule.NonZero;

            if (m_CanvasOddeven == null)
                Debug.LogWarning("[VectorAPIDemoPage3]: Missing FillHoleElement with name 'fill__canvas-oddeven'");
            else
                m_CanvasOddeven.FillRule = FillRule.OddEven;

            // Sync initial toggle state to canvases
            bool reverse = m_ReverseWindingToggle?.value ?? false;
            bool showArrows = m_ShowArrowsToggle?.value ?? true;

            if (m_CanvasNonzero != null)
            {
                m_CanvasNonzero.ReverseWinding = reverse;
                m_CanvasNonzero.ShowArrows = showArrows;
            }

            if (m_CanvasOddeven != null)
            {
                m_CanvasOddeven.ReverseWinding = reverse;
                m_CanvasOddeven.ShowArrows = showArrows;
            }

            UpdateStatusLabels();
            RegisterCallbacks();
        }

        void RegisterCallbacks()
        {
            m_ReverseWindingToggle.RegisterValueChangedCallback(evt =>
            {
                bool reverse = evt.newValue;

                if (m_CanvasNonzero != null)
                    m_CanvasNonzero.ReverseWinding = reverse;

                if (m_CanvasOddeven != null)
                    m_CanvasOddeven.ReverseWinding = reverse;

                UpdateStatusLabels();
            });

            m_ShowArrowsToggle.RegisterValueChangedCallback(evt =>
            {
                bool show = evt.newValue;

                if (m_CanvasNonzero != null)
                    m_CanvasNonzero.ShowArrows = show;

                if (m_CanvasOddeven != null)
                    m_CanvasOddeven.ShowArrows = show;
            });
        }

        void UpdateStatusLabels()
        {
            string windingText = m_ReverseWindingToggle.value ? "Reversed" : "Normal";

            if (m_CanvasNonzero != null && m_LabelNonzero != null)
                m_LabelNonzero.text = $"FillRule: {m_CanvasNonzero.FillRule} — Winding: {windingText}";

            if (m_CanvasOddeven != null && m_LabelOddeven != null)
                m_LabelOddeven.text = $"FillRule: {m_CanvasOddeven.FillRule} — Winding: {windingText}";
        }
    }
}