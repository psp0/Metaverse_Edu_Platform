using UnityEngine;
using UnityEngine.UI;

public class GenderToggle : MonoBehaviour
{
    public Toggle maleToggle;
    public Toggle femaleToggle;

    void Start()
    {
        // 시작 시 둘 다 비활성화
        maleToggle.isOn = false;
        femaleToggle.isOn = false;

        maleToggle.onValueChanged.AddListener(OnMaleToggled);
        femaleToggle.onValueChanged.AddListener(OnFemaleToggled);
    }

    void OnMaleToggled(bool isOn)
    {
        if (isOn)
        {
            femaleToggle.isOn = false;
        }
    }

    void OnFemaleToggled(bool isOn)
    {
        if (isOn)
        {
            maleToggle.isOn = false;
        }
    }
}
