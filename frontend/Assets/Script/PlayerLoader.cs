using UnityEngine;
using System.IO;
using System.Collections;
using Layer_lab._3D_Casual_Character;

public class PlayerLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // 씬 로드 안정화 대기

        string path = Application.persistentDataPath + "/characterData.json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("⚠ 저장된 캐릭터 데이터가 없습니다.");
            yield break;
        }

        string json = File.ReadAllText(path);
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        // ✅ CharacterBase 초기화
        CharacterBase.Instance.Init();

        // ✅ 모든 Parts 자식 자동 등록
        var allParts = FindObjectsOfType<Parts>();
        foreach (var part in allParts)
            part.AddPartsItem();

        // ✅ 저장된 외형 적용
        foreach (var part in data.parts)
        {
            if (part.index >= 0)
                CharacterBase.Instance.SetItem(part.type, part.index);
        }

        Debug.Log("🎉 캐릭터 불러오기 완료 (MainMap)");
    }
}
