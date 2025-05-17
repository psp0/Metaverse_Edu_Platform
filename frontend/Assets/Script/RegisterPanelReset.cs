using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegisterPanelReset : MonoBehaviour
{
    public TMP_InputField RegisterName;
    public TMP_InputField RegisterID;
    public TMP_InputField RegisterPW;
    public TMP_InputField RegisterPhone;

    public TMP_Dropdown DropYear;
    public TMP_Dropdown DropMonth;
    public TMP_Dropdown DropDay;

    public Toggle GenderMale;
    public Toggle GenderFemale;

    void OnEnable()  // RegisterPanel.SetActive(true) 될 때 자동 실행
    {
        ClearRegisterFields();
    }

    void ClearRegisterFields()
    {
        RegisterName.text = "";
        RegisterID.text = "";
        RegisterPW.text = "";
        RegisterPhone.text = "";

        DropYear.value = 0;
        DropMonth.value = 0;
        DropDay.value = 0;

        GenderMale.isOn = false;
        GenderFemale.isOn = false;
    }
}
