using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Layer_lab._3D_Casual_Character;

public class CharacterCustomizer : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            // 로컬 플레이어의 데이터는 이미 로드됨 → PhotonManager에서 처리
        }
        else
        {
            // 다른 플레이어의 데이터 받기
            ApplyFromPhotonProperties(photonView.Owner);
        }
    }

    public void ApplyFromPhotonProperties(Player player)
    {
        if (player.CustomProperties.Count == 0) return;

        var allParts = GetComponentsInChildren<Parts>(true);

        foreach (var key in player.CustomProperties.Keys)
        {
            string partType = key.ToString();
            int index = (int)player.CustomProperties[key];

            foreach (var part in allParts)
            {
                if (part.PartsType.ToString() == partType)
                {
                    for (int i = 0; i < part.parts.Count; i++)
                        part.parts[i].gameObject.SetActive(i == index);
                    break;
                }
            }
        }

        Debug.Log($"🎨 {player.NickName} 외형 커스터마이징 적용 완료");
    }
}
