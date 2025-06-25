using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace Demos.Localization
{

    /// <summary>
    ///  This demo shows how to set up text localization via C# script.
    /// </summary>
    public class StringLocalizationDemo : MonoBehaviour, ILocalizationDemo
    {

        [Header("Set via script")]
        [Tooltip("Localized string set via scripting")]
        [SerializeField] LocalizedString m_ScriptLocalizedString;

        // This Label as the text binding is set in UXML via the UI Builder
        Label m_TextLabelDirectLocalized;

        // This Label has the text binding set via C# scripting
        Label m_TextLabelScriptLocalized;

        public void Initialize(VisualElement root)
        {
            // Noted for reference only; UI Builder handles the data binding in this case
            m_TextLabelDirectLocalized = root.Q<Label>("text__label-direct-localized");

            // Label to use scripted data binding
            m_TextLabelScriptLocalized = root.Q<Label>("text__label-script-localized");

            // Links the "text" property of the Label with the corresponding LocalizedString
            m_TextLabelScriptLocalized.SetBinding("text", m_ScriptLocalizedString);
        }

        /// <summary>
        /// Clears all Label bindings to prevent memory leaks. LocalizedString bindings on text
        /// properties are not automatically released when the VisualElements are destroyed.
        /// Prevents dangling references to localization data if the UI is reset or changed.
        /// </summary>
        public void Cleanup()
        {
            if (m_TextLabelDirectLocalized != null)
            {
                m_TextLabelDirectLocalized.Unbind();
                m_TextLabelDirectLocalized = null;
            }

            if (m_TextLabelScriptLocalized != null)
            {
                m_TextLabelScriptLocalized.Unbind();
                m_TextLabelScriptLocalized = null;
            }
        }
    }
}