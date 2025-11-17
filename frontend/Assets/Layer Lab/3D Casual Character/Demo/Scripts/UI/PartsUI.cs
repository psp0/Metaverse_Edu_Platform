using UnityEngine;
using System.Collections.Generic;

namespace Layer_lab._3D_Casual_Character
{
    public class PartsUI : MonoBehaviour
    {
        [SerializeField] private ButtonParts button;   // 버튼 프리팹
        [SerializeField] private Transform content;
        private readonly List<ButtonParts> _buttonParts = new();

        private Parts[] PartsArray;

        public void Init()
        {
            PartsArray = FindObjectsOfType<Parts>();

            for (int i = 0; i < PartsArray.Length; i++)
            {
                var t = PartsArray[i];
                var buttonParts = Instantiate(button, content, false);
                _buttonParts.Add(buttonParts);
                buttonParts.SetButton(t);

                // ✅ 9번째(인덱스 8) / 12번째(인덱스 11) 잠금 표시
                if (i == 8 || i == 11)
                {
                    var jujak = buttonParts.transform.Find("Jujak");
                    if (jujak != null)
                        jujak.gameObject.SetActive(true);
                }
                else
                {
                    var jujak = buttonParts.transform.Find("Jujak");
                    if (jujak != null)
                        jujak.gameObject.SetActive(false);
                }
            }

            // 원본 버튼은 숨기기
            button.gameObject.SetActive(false);
        }
    }
}
