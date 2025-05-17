using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BirthDate : MonoBehaviour
{
    public TMP_Dropdown DropYear;
    public TMP_Dropdown DropMonth;
    public TMP_Dropdown DropDay;

    void Start()
    {
        SetupYearDropdown();
        SetupMonthDropdown();
        SetupDayDropdown();

        DropYear.onValueChanged.AddListener(delegate { UpdateDayDropdown(); });
        DropMonth.onValueChanged.AddListener(delegate { UpdateDayDropdown(); });
    }

    void SetupYearDropdown()
    {
        List<string> options = new List<string> { "년도" };
        for (int year = 2025; year >= 1920; year--)
        {
            options.Add(year.ToString());
        }
        DropYear.ClearOptions();
        DropYear.AddOptions(options);
    }

    void SetupMonthDropdown()
    {
        List<string> options = new List<string> { "월" };
        for (int month = 1; month <= 12; month++)
        {
            options.Add(month.ToString("00"));
        }
        DropMonth.ClearOptions();
        DropMonth.AddOptions(options);
    }

    void SetupDayDropdown()
    {
        UpdateDayOptions(31);
    }

    void UpdateDayDropdown()
    {
        int selectedYear = GetDropdownValue(DropYear);
        int selectedMonth = GetDropdownValue(DropMonth);

        if (selectedYear > 0 && selectedMonth > 0)
        {
            int daysInMonth = GetDaysInMonth(selectedYear, selectedMonth);
            UpdateDayOptions(daysInMonth);
        }
    }

    void UpdateDayOptions(int days)
    {
        List<string> options = new List<string> { "일" };
        for (int day = 1; day <= days; day++)
        {
            options.Add(day.ToString("00"));
        }
        DropDay.ClearOptions();
        DropDay.AddOptions(options);
    }

    int GetDropdownValue(TMP_Dropdown dropdown)  // ✅ 여기를 수정!
    {
        if (dropdown.value == 0) return -1;
        return int.Parse(dropdown.options[dropdown.value].text);
    }

    int GetDaysInMonth(int year, int month)
    {
        switch (month)
        {
            case 2:
                return IsLeapYear(year) ? 29 : 28;
            case 4:
            case 6:
            case 9:
            case 11:
                return 30;
            default:
                return 31;
        }
    }

    bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
