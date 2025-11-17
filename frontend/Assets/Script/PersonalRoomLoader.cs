using UnityEngine;
using System.IO;
using System.Collections;
using Layer_lab._3D_Casual_Character;

public class PersonalRoomLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // 씬 로드 안정화 대기

        string path = Application.persistentDataPath + "/characterData.json";
        if (!File.Exists(path))
        {
            Debug.Log("⚠ 저장된 캐릭터 데이터 없음 (새 기본 상태로 시작)");
            yield break;
        }

        string json = File.ReadAllText(path);
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        // ✅ 캐릭터 초기화
        CharacterBase.Instance.Init();

        // ✅ 모든 파츠 자동 등록
        var allParts = FindObjectsOfType<Parts>();
        foreach (var part in allParts)
            part.AddPartsItem();

        // ✅ 저장된 외형 복원
        foreach (var part in data.parts)
        {
            if (part.index >= 0)
                CharacterBase.Instance.SetItem(part.type, part.index);
        }

        Debug.Log("🎉 PersonalRoom 커스터마이징 캐릭터 복원 완료");
    }
}
