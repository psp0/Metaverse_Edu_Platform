using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Demos
{
    [CreateAssetMenu(fileName = "TeamSO", menuName = "Scriptable Objects/TeamSO")]
    public class TeamSO : ScriptableObject
    {
        [SerializeField] string m_TeamName;
        
        [SerializeField] List<PlayerDataSO> m_Players;
        
        
        [CreateProperty] public List<PlayerDataSO> Players => m_Players;
        [CreateProperty] public string TeamName => m_TeamName;

    }
}