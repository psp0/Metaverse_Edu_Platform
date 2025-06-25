using UnityEngine;
using UnityEngine.UIElements;
using Unity.Properties;

namespace Demos
{
    /// <summary>
    /// Controller script for the demo HealthBar. This script binds the HealthSO data
    /// to the health bar UI elements.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        
        [Tooltip("ScriptableObject containing health data")]
        [SerializeField] PlayerDataSO m_HealthData;
        
        Label m_PlayerName;
        Label m_CurrentHealth;
        Label m_MaxHealth;
        VisualElement m_Progress;
        
        public PlayerDataSO HealthData => m_HealthData;

        /// <summary>
        /// Initializes the health bar UI by binding HealthSO data to the elements.
        /// </summary>
        /// <param name="root">The root VisualElement for the UI hierarchy.</param>

        public void Initialize(VisualElement root)
        {
            SetVisualElements(root);

            UpdateDataSource(root);

            UpdatePaths();
        }

        /// <summary>
        /// Query the given root element and set references to VisualElements.
        /// </summary>
        /// <param name="root"></param>
        protected void SetVisualElements(VisualElement root)
        {
            m_PlayerName = root.Q<Label>("health-bar__player-name");
            m_CurrentHealth = root.Q<Label>("health-bar__current-health");
            m_MaxHealth = root.Q<Label>("health-bar__max-health");
            m_Progress = root.Q<VisualElement>("health-bar__progress");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        protected void UpdateDataSource(VisualElement root)
        {
            // Set the data source on the root element
            if (m_HealthData != null)
                root.dataSource = m_HealthData;
        }

        /// <summary>
        /// Update the data source paths.
        /// </summary>
        protected virtual void UpdatePaths()
        {
            // Bind the CurrentHealth label's text property to the CurrentHealth property in HealthSO
            m_PlayerName.SetBinding("text", new DataBinding()
            {
                dataSourcePath = new PropertyPath(nameof(PlayerDataSO.PlayerName)),
                bindingMode = BindingMode.ToTarget
            });
            
            // Bind the CurrentHealth label's text property to the CurrentHealth property in HealthSO
            m_CurrentHealth.SetBinding("text", new DataBinding()
            {
                dataSourcePath = new PropertyPath(nameof(PlayerDataSO.CurrentHealth)),
                bindingMode = BindingMode.ToTarget
            });

            // Bind the MaxHealth label to the MaximumHealth property (preformatted with "/" in front)
   
            m_MaxHealth.SetBinding("text", new DataBinding()
            {
                dataSourcePath = new PropertyPath(nameof(PlayerDataSO.MaximumHealthFormatted)),
                bindingMode = BindingMode.ToTarget
            });

            // Bind the ProgressBar's width to the HealthPercentage property in HealthSO
            m_Progress.SetBinding("style.width", new DataBinding()
            {
                dataSourcePath = new PropertyPath(nameof(PlayerDataSO.CurrentHealthProgress)),
                bindingMode = BindingMode.ToTarget
            });
        }

        /// <summary>
        /// Cleans up bindings and references. 
        /// </summary>
        public void Cleanup()
        {
            // Clean up or unbind any resources if necessary
        }
    }
}