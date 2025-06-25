using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// This Editor script shows the corresponding PlayerDataSO in the Inspector when the HealthBar is
    /// selected.
    /// </summary>
    [CustomEditor(typeof(HealthBar), true)]
    public class HealthBarEditor : Editor
    {
        VisualElement m_RootElement;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override VisualElement CreateInspectorGUI()
        {
            var styleSheet = Resources.Load<StyleSheet>("Uss/HealthBarInspector");

            var healthBar = (HealthBar)target;

            // Create a root container
            m_RootElement = new VisualElement();

            // Add default Inspector fields
            var defaultInspector = new IMGUIContainer(() => DrawDefaultInspector());
            m_RootElement.Add(defaultInspector);
            m_RootElement.AddToClassList("rootElement");
            
            // Add StyleSheet from Resource
            // Apply the style if found
            if (styleSheet != null)
            {
                m_RootElement.styleSheets.Add(styleSheet);
            }
            else
            {
                Debug.LogWarning("Failed to load HealthBarInspector.uss from Resources.");
            }
            
            
            // Display PlayerDataSO properties if available
            if (healthBar.HealthData != null)
            {
                var healthData = healthBar.HealthData;

                // Create a UI container for the health bar
                var healthBarContainer = new VisualElement();
                healthBarContainer.style.flexDirection = FlexDirection.Column;
                healthBarContainer.style.borderBottomWidth = new StyleFloat(1f);

                // Add a label for the player's name
                var playerNameLabel = new Label($"Player Name: {healthData.PlayerName}");
                
                playerNameLabel.AddToClassList("label");
                healthBarContainer.Add(playerNameLabel);

                // Add a slider for CurrentHealth
                var healthSlider = new SliderInt("Current Health", 0, healthData.MaximumHealth);
                healthSlider.AddToClassList("slider");
                healthSlider.value = healthData.CurrentHealth;
                healthSlider.RegisterValueChangedCallback(evt =>
                {
                    healthData.SetHealth(evt.newValue);
                    EditorUtility.SetDirty(healthData); // Mark the SO as dirty to save changes
                });
                healthBarContainer.Add(healthSlider);

                // Add a label for Max Health
                var maxHealthLabel = new Label($"Maximum Health: {healthData.MaximumHealth}");
                healthBarContainer.Add(maxHealthLabel);

                m_RootElement.Add(healthBarContainer);
                
            }
            else
            {
                m_RootElement.Add(new Label("No PlayerData ScriptableObject assigned."));
            }

            return m_RootElement;
        }
    }
}