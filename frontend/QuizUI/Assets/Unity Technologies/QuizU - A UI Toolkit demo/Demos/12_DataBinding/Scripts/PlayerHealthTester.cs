using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace Demos
{
    /// <summary>
    /// These are playtest buttons for incrementing, decrementing, and selecting a PlayerData health source.
    /// </summary>
    public class PlayerHealthTester : MonoBehaviour
    {
        [Tooltip("The ScriptableObject to playtest/control")] 
        [SerializeField] PlayerDataSO m_PlayerData;

        [Tooltip("Name of the root VisualElement for use with this user interface")]
        [SerializeField] string m_RootElementID;

        Button m_DecrementButton; // Button to decrease health
        Button m_SelectButton; // Button to select the health source in the editor
        Button m_IncrementButton; // Button to increase health

        const int k_Increment = 5; // The amount to increment or decrement health

        UIDocument m_UIDocument;

        void OnEnable()
        {
            m_UIDocument = GetComponent<UIDocument>();
        }

        public void Start()
        {
            if (m_UIDocument == null || m_PlayerData == null)
                return;

            VisualElement rootElement = m_UIDocument.rootVisualElement.Q<VisualElement>(m_RootElementID);

            m_DecrementButton = rootElement.Q<Button>("decrement__button");
            m_SelectButton = rootElement.Q<Button>("select__button");
            m_IncrementButton = rootElement.Q<Button>("increment__button");

            m_DecrementButton.clicked += () => m_PlayerData.DecrementHealth(k_Increment);
            m_IncrementButton.clicked += () => m_PlayerData.IncrementHealth(k_Increment);
            m_SelectButton.clicked += () => SelectHealthSource(m_PlayerData);
        }

        /// <summary>
        /// Selects the health source object in the Unity Editor.
        /// </summary>
        /// <param name="healthData">The PlayerDataSO instance to select.</param>
        void SelectHealthSource(PlayerDataSO healthData)
        {
            Selection.activeObject = healthData;
            EditorGUIUtility.PingObject(healthData);
        }
    }
}