using Unity.Properties;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Manages the ListView for displaying a team's players.
    /// </summary>
    public class TeamList : MonoBehaviour
    {
        [Tooltip("PlayerData for the team displayed in the ListView")]
        [SerializeField] TeamSO m_TeamData;

        [Tooltip("The height in pixels of each item in the ListView")]
        [SerializeField] int m_FixedItemHeight = 150;

        ListView m_ListView;
        VisualElement m_ListViewContainer;

        /// <summary>
        /// Initializes the ListView with a given VisualElement.
        /// </summary>
        /// <param name="root">The root VisualElement containing the ListView.</param>
        public void Initialize(VisualElement root)
        {
            if (root == null)
            {
                Debug.LogError("[TeamList] Root VisualElement is null.");
                return;
            }

            m_ListViewContainer = root.Q<VisualElement>("demo__list-view-container");

            if (m_ListViewContainer == null)
            {
                Debug.LogError("[TeamList] Missing ListView container.");
                return;
            }

            m_ListView = m_ListViewContainer.Q<ListView>();
            if (m_ListView == null)
            {
                Debug.LogError("[TeamList] Missing ListView in the container.");
                return;
            }

            SetupListView();
        }

        /// <summary>
        /// Configures the ListView and binds it to the team data.
        /// </summary>
        void SetupListView()
        {
            if (m_TeamData == null)
            {
                Debug.LogError("[TeamList] Missing TeamSO data.");
                return;
            }

            // Set the data source
            m_ListViewContainer.dataSource = m_TeamData;


            // Bind the "itemsSource"  to the Players list in TeamSO
            m_ListView.SetBinding("itemsSource", new DataBinding
            {
                dataSourcePath = new PropertyPath("Players")
            });

            // Set a fixed item height for virtualization
            m_ListView.fixedItemHeight = m_FixedItemHeight;

            Debug.Log($"[TeamList] Number of players: {m_TeamData.Players.Count}");
            m_TeamData.Players.ForEach(player => Debug.Log($"[TeamList] Player Loaded: {player.PlayerName}"));
        }
    }
}