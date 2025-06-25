using UnityEngine;
using UnityEngine.UIElements;

namespace Quiz
{
    /// <summary>
    /// This is a simplified version of UIScreen from the demo application. This illustrates the core functionality of the UI Screen component:
    /// showing and hiding screens using the DisplayStyle.
    ///
    /// To more easily manage each screen, create a separate class for each. Then instantiate (e.g. new SimpleSplashScreen(), new SimpleStartScreen)
    /// in the UIManager.
    /// </summary>
    public class SimpleUIScreen
    {
        // The root VisualElement of the screen
        protected VisualElement m_RootElement;

        // Constructor
        public SimpleUIScreen(VisualElement rootElement)
        {
            m_RootElement = rootElement;
            Initialize();
        }

        // Initialize the screen (can be overridden in derived classes)
        public virtual void Initialize()
        {
            Hide();
        }

        // Show the screen
        public virtual void Show()
        {
            m_RootElement.style.display = DisplayStyle.Flex;
        }

        // Hide the screen
        public virtual void Hide()
        {
            m_RootElement.style.display = DisplayStyle.None;
        }
    }
    
    // 
    
    public class SimpleSplashScreen : SimpleUIScreen
    {
        public SimpleSplashScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimpleStartScreen : SimpleUIScreen
    {
        public SimpleStartScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimpleHomeScreen : SimpleUIScreen
    {
        public SimpleHomeScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimpleSettingsScreen : SimpleUIScreen
    {
        public SimpleSettingsScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimpleLevelSelectionScreen : SimpleUIScreen
    {
        public SimpleLevelSelectionScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimpleGameScreen : SimpleUIScreen
    {
        public SimpleGameScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimplePauseScreen : SimpleUIScreen
    {
        public SimplePauseScreen(VisualElement rootElement) : base(rootElement) { }
    }

    public class SimpleEndScreen : SimpleUIScreen
    {
        public SimpleEndScreen(VisualElement rootElement) : base(rootElement) { }
    }
    
}