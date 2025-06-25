using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace Demos.Localization
{
    public interface ILocalizationDemo
    {
        void Initialize(VisualElement root);
        void Cleanup();
    }

    /// <summary>
    /// Demonstrates localization system features including string, smart string, and texture localization.
    /// Provides UI controls for switching between different locales/languages.
    /// </summary>
    public class LocalizationDemo : DemoBase
    {
        [Tooltip("Demo component showing basic string localization")] [SerializeField]
        StringLocalizationDemo m_StringLocalizationDemo;

        [Tooltip("Demo component showing smart string localization with dynamic content")] [SerializeField]
        SmartStringDemo m_SmartStringDemo;

        [Tooltip("Demo component showing how to use StringChanged event with LocalizedStrings")] [SerializeField]
        PreprocessDemo m_PreprocessDemo;

        [Tooltip("Demo component showing texture/image localization")] [SerializeField]
        TextureLocalizationDemo m_TextureLocalizationDemo;

        Button m_ButtonEnglish;
        Button m_ButtonFrench;
        Button m_ButtonSpanish;
        Button m_ButtonDanish;





        protected override void OnEnable()
        {
            base.OnEnable();
            SetVisualElements();
            RegisterCallbacks();

            m_StringLocalizationDemo.Initialize(m_Root);
            m_SmartStringDemo.Initialize(m_Root);
            m_PreprocessDemo.Initialize(m_Root);
            m_TextureLocalizationDemo.Initialize(m_Root);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            m_StringLocalizationDemo.Cleanup();
            m_SmartStringDemo.Cleanup();
            m_TextureLocalizationDemo.Cleanup();
            m_PreprocessDemo.Cleanup();
        }

        /// <summary>
        /// Queries and caches UI button references from the root visual element.
        /// </summary>
        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_ButtonDanish = m_Root.Q<Button>("language__button--da");
            m_ButtonEnglish = m_Root.Q<Button>("language__button--en");
            m_ButtonFrench = m_Root.Q<Button>("language__button--fr");
            m_ButtonSpanish = m_Root.Q<Button>("language__button--es");

            LocalizedString localizedString = new LocalizedString
            {
                TableReference = "DemoStringTable",
                TableEntryReference = "Text_Label1"
            };
        }


        /// <summary>
        /// Registers click handlers for language selection buttons.
        /// Shows both clicked manipulator and RegisterCallback syntax.
        /// </summary>
        void RegisterCallbacks()
        {
            // Uses the clicked manipulator to handle the button click
            m_ButtonDanish.clicked += () => SelectLocale("da");
            m_ButtonEnglish.clicked += () => SelectLocale("en");
            m_ButtonSpanish.clicked += () => SelectLocale("es");
            m_ButtonFrench.clicked += () => SelectLocale("fr");

            // Alternatively, use the RegisterCallback<ClickEvent>():
            //    m_ButtonDanish.RegisterCallback<ClickEvent>((x) => SwitchLocales("da"));
            //    m_ButtonEnglish.RegisterCallback<ClickEvent>((x) => SwitchLocales("en"));
            //    m_ButtonSpanish.RegisterCallback<ClickEvent>((x) => SwitchLocales("es"));
            //    m_ButtonFrench.RegisterCallback<ClickEvent>((x) => SwitchLocales("fr"));
        }

        /// <summary>
        /// Convert the unique two-letter identifier into a Locale and select it.
        /// </summary>
        /// <param name="localeCode">Unique two-letter string ID associated with the target Locale.</param>
        void SelectLocale(string localeCode)
        {
            // Convert the code into a Locale
            Locale locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);

            // Set the Locale as active
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}