using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Demos.Localization
{
    /// <summary>
    /// Demonstrates the usage of SmartStrings with localization, showing examples where the
    /// user-entered fields can affect the string values, even when localizing.
    /// </summary>
    public class SmartStringDemo : MonoBehaviour, ILocalizationDemo
    {
        [Header("LocalizedStrings")]
        [Tooltip("Displays a welcome message with the player's name, e.g., 'Welcome, Player One!'")]
        [SerializeField] LocalizedString m_PlaceholderMessage;
        
        // Chooses between 1, 2, 3, or default: "Good {0:choose(1|2|3):morning|afternoon|evening|anytime}, {1}!"
        [Tooltip("Display a time-based greeting, e.g. Good (morning, afternoon, evening), followed by the player's name.")]
        [SerializeField] LocalizedString m_ChooseFormatterMessage;
        
        [Tooltip("Displays item count with plural handling, e.g., 'You have one item' or 'You have # items.'")]
        [SerializeField] LocalizedString m_PluralizationMessage;
        
        [Header("SmartString initial values")]
        [Tooltip("The player's name to display in localized messages.")]
        [SerializeField] string m_PlayerName = "Player One";
        
        [Tooltip("Hour of day to determine greeting: morning (5-11), afternoon (12-16), evening (other).")]
        [SerializeField] int m_HourOfDay;
        
        [Tooltip("The number of items the player has, used for singular/plural localization in item count messages.")]
        [SerializeField] int m_ItemCount;
        
        Label m_PlaceholderLabel;
        Label m_PluralizationLabel;
        Label m_ChooseFormatterLabel;
        
        /// <summary>
        /// Queries for Label elements in the UI, setsSmartString arguments,
        /// and binds each Label's text to its corresponding LocalizedString.
        /// 
        /// Note: Serialized fields only update once on Initialize.
        /// </summary>
        /// <param name="root">The root VisualElement containing the UI Labels to bind.</param>

        public void Initialize(VisualElement root)
        {
            // Set up the welcome message with the player's name
            m_PlaceholderLabel = root.Q<Label>("welcome__label");
            m_PlaceholderMessage.Arguments = new object[] { m_PlayerName };
            m_PlaceholderLabel.SetBinding("text", m_PlaceholderMessage);

            // Set up the time-based greeting (morning/afternoon/evening) and player's name
            m_ChooseFormatterLabel = root.Q<Label>("time-based__label");
            int hour = GetHourOfDay();
            m_ChooseFormatterMessage.Arguments = new object[] { hour, m_PlayerName };
            m_ChooseFormatterLabel.SetBinding("text", m_ChooseFormatterMessage);
            
            // Set up item count message with singular/plural handling
            m_PluralizationLabel = root.Q<Label>("item-count__label");
            m_PluralizationMessage.Arguments = new object[] { m_ItemCount };
            m_PluralizationLabel.SetBinding("text", m_PluralizationMessage);

        }
        /// <summary>
        /// Clears all Label bindings to prevent memory leaks. LocalizedString bindings on text
        /// properties are not automatically released when the VisualElements are destroyed.
        /// Prevents dangling references to localization data if the UI is reset or changed.
        /// </summary>
        public void Cleanup()
        {
            if (m_PlaceholderLabel != null)
            {
                m_PlaceholderLabel.Unbind();
                m_PlaceholderLabel = null;
            }

            if (m_ChooseFormatterLabel != null)
            {
                m_ChooseFormatterLabel.Unbind();
                m_ChooseFormatterLabel = null;
            }
            
            if (m_PluralizationLabel != null)
            {
                m_PluralizationLabel.Unbind();
                m_PluralizationLabel = null;
            }
        }

        /// <summary>
        /// Determines the appropriate greeting based on the time of day.
        /// - Returns 1 for hours between 5-11.
        /// - Returns 2 for hours between 12-16.
        /// - Returns 3 for other hours.
        /// </summary>
        /// <returns>An int representing the hour of day.</returns>

        int GetHourOfDay()
        {
            int hour = (m_HourOfDay is >= 0 and <= 24) ? m_HourOfDay : DateTime.Now.Hour;

            if (hour >= 5 && hour < 12)
            {
                return 1; // morning
            }
            else if (hour >= 12 && hour < 17)
            {
                return 2; // afternoon
            }
            else
            {
                return 3; // evening
            }
        }
    }
}