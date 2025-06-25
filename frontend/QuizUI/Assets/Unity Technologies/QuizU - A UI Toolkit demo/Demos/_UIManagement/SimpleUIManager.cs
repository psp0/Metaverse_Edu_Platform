using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Quiz
{
    /// <summary>
    /// This is a simplified version of UIManager from the demo application. This class shows and hides SimpleUIScreens
    /// using the DisplayStyle property.
    /// 
    /// Compared to the original UIManager, this simplified version omits features such as event subscriptions,
    /// coroutine handling, screen transitions, and a history stack for screen navigation.
    /// 
    /// This simplified version assumes that all SimpleUIScreens are part of the same UXML document.
    /// </summary>
    public class SimpleUIManager : MonoBehaviour
    {
        [Tooltip("Required UI Document")]
        [SerializeField]  UIDocument m_Document;

        // Instances of all the screens in the application
        SimpleSplashScreen m_SplashScreen;
        SimpleStartScreen m_StartScreen;
        SimpleHomeScreen m_HomeScreen;
        SimpleSettingsScreen m_SettingsScreen;
        SimpleLevelSelectionScreen m_LevelSelectionScreen;
        SimpleGameScreen m_GameScreen;
        SimplePauseScreen m_PauseScreen;
        SimpleEndScreen m_EndScreen;

        // The currently active screen
        private SimpleUIScreen m_CurrentScreen;

        // List of all screens for easy management
        private List<SimpleUIScreen> m_Screens = new List<SimpleUIScreen>();

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Sets up all UI screens and initial state.
        /// </summary>
        private void Initialize()
        {
            VisualElement root = m_Document.rootVisualElement;

            // Initialize screens with their corresponding VisualElements
            m_SplashScreen = new SimpleSplashScreen(root.Q<VisualElement>("splash__container"));
            m_StartScreen = new SimpleStartScreen(root.Q<VisualElement>("start__container"));
            m_HomeScreen = new SimpleHomeScreen(root.Q<VisualElement>("menu__container"));
            m_SettingsScreen = new SimpleSettingsScreen(root.Q<VisualElement>("settings__container"));
            m_LevelSelectionScreen = new SimpleLevelSelectionScreen(root.Q<VisualElement>("select__container"));
            m_GameScreen = new SimpleGameScreen(root.Q<VisualElement>("question-screen__parent"));
            m_PauseScreen = new SimplePauseScreen(root.Q<VisualElement>("pause__container"));
            m_EndScreen = new SimpleEndScreen(root.Q<VisualElement>("end-screen__container"));

            // Add screens to the list
            m_Screens.Add(m_SplashScreen);
            m_Screens.Add(m_StartScreen);
            m_Screens.Add(m_HomeScreen);
            m_Screens.Add(m_SettingsScreen);
            m_Screens.Add(m_LevelSelectionScreen);
            m_Screens.Add(m_GameScreen);
            m_Screens.Add(m_PauseScreen);
            m_Screens.Add(m_EndScreen);

            // Hide all screens to start
            foreach (var screen in m_Screens)
            {
                screen.Hide();
            }

            // Show the initial screen, e.g., the SplashScreen
            ShowScreen(m_SplashScreen);

            // Optionally, set up event handlers or timers to transition between screens
        }

        /// <summary>
        /// Shows the specified screen and hides the current one.
        /// </summary>
        /// <param name="screen">The screen to display.</param>
        public void ShowScreen(SimpleUIScreen screen)
        {
            if (m_CurrentScreen != null)
            {
                m_CurrentScreen.Hide();
            }

            m_CurrentScreen = screen;
            m_CurrentScreen.Show();
        }

        // Methods to show specific screens
        public void ShowSplashScreen()
        {
            ShowScreen(m_SplashScreen);
        }

        public void ShowStartScreen()
        {
            ShowScreen(m_StartScreen);
        }

        public void ShowHomeScreen()
        {
            ShowScreen(m_HomeScreen);
        }

        public void ShowSettingsScreen()
        {
            ShowScreen(m_SettingsScreen);
        }

        public void ShowLevelSelectionScreen()
        {
            ShowScreen(m_LevelSelectionScreen);
        }

        public void ShowGameScreen()
        {
            ShowScreen(m_GameScreen);
        }

        public void ShowPauseScreen()
        {
            ShowScreen(m_PauseScreen);
        }

        public void ShowEndScreen()
        {
            ShowScreen(m_EndScreen);
        }
    }
}