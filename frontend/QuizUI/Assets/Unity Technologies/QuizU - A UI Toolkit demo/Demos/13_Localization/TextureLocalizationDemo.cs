using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace Demos.Localization
{
    /// <summary>
    /// Demonstrates how to localize a texture/sprite asset and bind it to a VisualElement's background image
    /// using Unity's Localization system. This shows how to set up texture localization in C# rather than use
    /// a UXML binding.
    /// </summary>
    public class TextureLocalizationDemo : MonoBehaviour, ILocalizationDemo
    {
        LocalizedTexture m_LocalizedTexture;
        VisualElement m_IconElement;

        /// <summary>
        /// Sets up the localized texture and binds it to a VisualElement background image.
        /// </summary>
        /// <param name="root">The root VisualElement containing the target image element</param>
        public void Initialize(VisualElement root)
        {
            // Set reference to the LocalizedTexture with key "HelloWorld_Icon" in the DemoAssetTable
            m_LocalizedTexture = new LocalizedTexture()
            {
                TableReference = "DemoAssetTable",
                TableEntryReference = "HelloWorld_Icon"
            };

            // Query for the VisualElement and set the data binding to the backgroundImage
            m_IconElement = root.Q<VisualElement>("icon__hello-world");
            m_IconElement.SetBinding("style.backgroundImage", m_LocalizedTexture);
        }

        /// <summary>
        /// Unbinds the LocalizedTexture binding (optional). LocalizedTexture bindings on style properties
        /// (like backgroundImage) are generally handled automatically, unlike LocalizedString bindings.
        /// </summary>
        public void Cleanup()
        {
            if (m_IconElement != null)
            {
                m_IconElement.Unbind();
                m_IconElement = null;
            }
        }
    }
}