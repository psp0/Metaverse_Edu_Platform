using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace Demos.Localization
{
    /// <summary>
    /// Demonstrates preprocessing of LocalizedStrings to dynamically update UI dropdown choices 
    /// when the selected locale changes.
    /// </summary>
    public class PreprocessDemo : MonoBehaviour, ILocalizationDemo
    {
        // These fields allow setting localized strings for each dropdown choice from the Inspector
        [Tooltip("First choice to appear in dropdown")] [SerializeField]
        LocalizedString m_Choice1LocalizedString;

        [Tooltip("Second choice to appear in dropdown")] [SerializeField]
        LocalizedString m_Choice2LocalizedString;

        [Tooltip("Third choice to appear in dropdown")] [SerializeField]
        LocalizedString m_Choice3LocalizedString;

        [Tooltip("Fourth choice to appear in dropdown")] [SerializeField]
        LocalizedString m_Choice4LocalizedString;

        LocalizedString m_LocalizedString;
        
        [CreateProperty]
        public string FormattedString => $"[Custom Format] {m_LocalizedString.GetLocalizedString()}";
        
        Label m_TextLabel;
        
        DropdownField m_DropdownField;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public void Initialize(VisualElement root)
        {

            m_DropdownField = root.Q<DropdownField>("dropdown__field");

            if (m_DropdownField.choices.Count > 0)
                m_DropdownField.value = m_DropdownField.choices[0];

            // Register callbacks to update the dropdown choices whenever the localized strings change.
            // StringChanged event triggers whenever the application's locale changes. This keeps the UI
            // in sync with the selected Locale.

            m_Choice1LocalizedString.StringChanged += UpdateDropdownChoices;

            m_Choice2LocalizedString.StringChanged += UpdateDropdownChoices;

            m_Choice3LocalizedString.StringChanged += UpdateDropdownChoices;

            m_Choice4LocalizedString.StringChanged += UpdateDropdownChoices;

            // Initial population of choices
            UpdateDropdownChoices(null);
        }

        
        /// <summary>
        /// Unregisters event listeners and removes references to dropdown when
        /// the object is destroyed.
        /// </summary>
        public void Cleanup()
        {
            // Remove callbacks to prevent memory leaks
            if (m_Choice1LocalizedString != null)
                m_Choice1LocalizedString.StringChanged -= UpdateDropdownChoices;

            if (m_Choice2LocalizedString != null)
                m_Choice2LocalizedString.StringChanged -= UpdateDropdownChoices;

            if (m_Choice3LocalizedString != null)
                m_Choice3LocalizedString.StringChanged -= UpdateDropdownChoices;

            if (m_Choice4LocalizedString != null)
                m_Choice4LocalizedString.StringChanged -= UpdateDropdownChoices;
        }

        /// <summary>
        /// Updates the dropdown choices whenever any of the localized strings change.
        /// </summary>
        /// <param name="value">Unused but required by delegate.</param>
        void UpdateDropdownChoices(string value)
        {
            if (m_DropdownField == null)
                return;

            // Save the existing selection by index
            int selection = m_DropdownField.index;

            // Clear existing choices
            m_DropdownField.choices.Clear();

            // Add current localized values
            m_DropdownField.choices.Add(m_Choice1LocalizedString.GetLocalizedString());
            m_DropdownField.choices.Add(m_Choice2LocalizedString.GetLocalizedString());
            m_DropdownField.choices.Add(m_Choice3LocalizedString.GetLocalizedString());
            m_DropdownField.choices.Add(m_Choice4LocalizedString.GetLocalizedString());

            // Restore selected index and value
            m_DropdownField.index = selection;

            // Update the UI without raising a ChangeEvent
            m_DropdownField.SetValueWithoutNotify(m_DropdownField.choices[selection]);
        }
    }
}