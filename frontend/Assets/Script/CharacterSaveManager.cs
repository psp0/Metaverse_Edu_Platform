using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using Layer_lab._3D_Casual_Character;

public class CharacterSaveManager : MonoBehaviour
{
    [Header("저장 후 이동할 씬 이름")]
    public string nextSceneName = "MainMap";

    public void OnClickApply()
    {
        SaveCharacter();
        SceneManager.LoadScene(nextSceneName);
    }

    private void SaveCharacter()
    {
        var allParts = FindObjectsOfType<Parts>();
        var data = new CharacterData();
        var list = new List<SerializablePart>();

        foreach (var part in allParts)
        {
            int equippedIndex = -1;
            for (int i = 0; i < part.parts.Count; i++)
            {
                if (part.parts[i].gameObject.activeSelf)
                {
                    equippedIndex = i;
                    break;
                }
            }

            list.Add(new SerializablePart
            {
                type = part.PartsType,
                index = equippedIndex
            });
        }

        data.parts = list.ToArray();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/characterData.json", json);

        Debug.Log($"✅ 캐릭터 저장 완료: {Application.persistentDataPath}/characterData.json");
    }
}
