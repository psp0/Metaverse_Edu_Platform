using UnityEngine;
using Layer_lab._3D_Casual_Character;

public class ChairSeat : MonoBehaviour
{
    public GameObject seatedCharacter;   // 앉은 캐릭터 프리팹 (기본 비활성)
    public bool IsOccupied { get; private set; }
    [HideInInspector] public int occupiedActor = -1;

    public void SetState(bool occupied, string json)
    {
        IsOccupied = occupied;

        if (seatedCharacter != null)
            seatedCharacter.SetActive(occupied);

        if (occupied && !string.IsNullOrEmpty(json))
            ApplyCustomization(JsonUtility.FromJson<CharacterData>(json));
    }

    private void ApplyCustomization(CharacterData data)
    {
        if (data == null || seatedCharacter == null) return;

        var allParts = seatedCharacter.GetComponentsInChildren<Parts>(true);
        foreach (var partData in data.parts)
        {
            foreach (var part in allParts)
            {
                if (part.PartsType == partData.type)
                {
                    for (int i = 0; i < part.parts.Count; i++)
                        part.parts[i].gameObject.SetActive(i == partData.index);
                    break;
                }
            }
        }

        Debug.Log($"🎨 [ChairSeat] {gameObject.name} 커스터마이징 적용 완료");
    }
}
