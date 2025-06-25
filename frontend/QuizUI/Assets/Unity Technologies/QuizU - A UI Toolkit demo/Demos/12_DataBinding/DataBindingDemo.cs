using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    public interface IDataBindingDemo
    {
        void Initialize(VisualElement root);
        void Cleanup();
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataBindingDemo : DemoBase
    {
        [Tooltip("Health bar component with C# bindings")] [SerializeField]
        HealthBar m_HealthBar;

        [Tooltip("Health bar component with data converters for colors/percentage/status")] [SerializeField]
        HealthBarWithConverter m_HealthBarWithConverter;

        [Tooltip("Component that binds team data to a ListView")] [SerializeField]
        TeamList m_TeamList;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            var containerScripted = m_Root.Q<VisualElement>("demo__container-script");
            m_HealthBar.Initialize(containerScripted);

            var containerConverted = m_Root.Q<VisualElement>("demo__container-converted");
            m_HealthBarWithConverter.Initialize(containerConverted);

            var listViewContainer = m_Root.Q<VisualElement>("demo__list-view-container");
            m_TeamList.Initialize(listViewContainer);
        }
    }
}