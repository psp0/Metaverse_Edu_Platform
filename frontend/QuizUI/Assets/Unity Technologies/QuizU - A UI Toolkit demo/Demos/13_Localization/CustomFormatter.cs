using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CustomFormatter", menuName = "Localization/CustomFormatter")]
public class CustomFormatter : ScriptableObject
{
    public string AddPrefix(string value) => $"[Prefix] {value}";
}

class Example
{
    LocalizedString m_LocalizedString;
    
    [CreateProperty]
    public string ProcessedString => $"Welcome, {m_LocalizedString.GetLocalizedString()}!";
    
    
    Label m_TextLabel;
    CustomFormatter m_CustomFormatter;
    
    void OnLocalizedStringChanged(string value)
    {
        m_TextLabel.text = m_CustomFormatter.AddPrefix(value);
    }
}



