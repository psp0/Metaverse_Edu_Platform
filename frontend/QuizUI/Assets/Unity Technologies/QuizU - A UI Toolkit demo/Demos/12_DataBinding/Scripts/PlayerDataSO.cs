using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Demos/Player_Data")]
public class PlayerDataSO : ScriptableObject
{

    [CreateProperty] public string PlayerName => m_PlayerName;
    [CreateProperty] public string Description => m_Description;
    [CreateProperty] public string Title => m_Title;
    
    // Percentage health value between 0 and 1
    [CreateProperty] public float HealthPercentage => (float)CurrentHealth / (float)m_MaximumHealth;

    // CurrentHealth value clamped between 0 and maximum health
    [CreateProperty] public int CurrentHealth => Mathf.Clamp(m_CurrentHealth, 0, m_MaximumHealth);
    [CreateProperty] public int MaximumHealth => m_MaximumHealth;

    // Pre-formats the MaximumHealth for a UI Label (e.g. CurrentHealth / 100 Max)
    [CreateProperty] public string MaximumHealthFormatted => $" / {MaximumHealth}";

    // Translates the current health into a StyleLength for a progress bar
    [CreateProperty]
    public StyleLength CurrentHealthProgress =>
        new StyleLength(new Length(HealthPercentage * 100f, LengthUnit.Percent));

    // Icon representing the character's class (scout/support/tank/tactical)
    [CreateProperty] public Texture2D ClassIcon => m_ClassIcon;

    // Icon representing rank/progress
    [CreateProperty] public Texture2D RankIcon => m_RankIcon;
    
    // Notify any listeners that health has changed - unused in this demo, included for reference
    public event Action<int> OnHealthChanged;
    
    [SerializeField] string m_PlayerName;
    [SerializeField] string m_Description;

    
    [Header("Health")]
    [SerializeField] int m_MaximumHealth = 100;
    [SerializeField] [Range(0, k_MaxHealthRange)]
    int m_CurrentHealth = 100;

    [Header("Team")]
    [SerializeField] string m_Title;
    [SerializeField] Texture2D m_ClassIcon;
    [SerializeField] Texture2D m_RankIcon;

    const int k_MaxHealthRange = 200;
  
    void OnEnable()
    {
        ResetHealth();
    }

    // Decrease health by a specified amount, clamped to 0
    public void DecrementHealth(int decrement)
    {
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - decrement, 0, m_MaximumHealth);
        NotifyHealthChanged();
    }

    // Increase health by a specified amount, clamped to maximum health
    public void IncrementHealth(int increment)
    {
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth + increment, 0, m_MaximumHealth);
        NotifyHealthChanged();
    }

    // Set health to a specific value, clamped to valid range
    public void SetHealth(int health)
    {
        m_CurrentHealth = Mathf.Clamp(health, 0, m_MaximumHealth);
        NotifyHealthChanged();
    }

    // Reset health to maximum value
    public void ResetHealth()
    {
        m_CurrentHealth = m_MaximumHealth;
        NotifyHealthChanged();
    }

    /// <summary>
    /// 
    /// Unused in this demo but included for reference
    /// </summary>
    void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(m_CurrentHealth);
    }
}
