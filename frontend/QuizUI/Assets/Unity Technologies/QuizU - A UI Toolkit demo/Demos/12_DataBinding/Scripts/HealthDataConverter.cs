using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Provides custom converters for health data for color, percentage, and status.
    /// </summary>
public static class HealthDataConverter
{
    static readonly Color s_FullColor = new Color(0.2f, 1f, 0.2f);
    static readonly Color s_MidColor = Color.yellow;
    static readonly Color s_LowColor = new Color(1f, 0.3f, 0f);
    static readonly Color s_CriticalColor = Color.red;

    /// <summary>
    /// Registers all health data converters globally for use in UI data binding.
    /// </summary>
    public static void Register()
    {
        RegisterHealthColorConverter();
        RegisterHealthPercentageConverter();
        RegisterStatusConverter();
    }
    
    /// <summary>
    /// Registers a converter group for displaying health percentages as text (e.g., "75%").
    /// </summary>
    static void RegisterHealthPercentageConverter()
    {
        // Create a converter group for the health percentage text
        var percentageConverter = new ConverterGroup("HealthPercentage");
        percentageConverter.AddConverter((ref float healthPercentage) =>
        {
            int percentage = Mathf.RoundToInt(healthPercentage * 100);
            return percentage.ToString() + "%";
        });

        ConverterGroups.RegisterConverterGroup(percentageConverter);
    }

    /// <summary>
    /// Registers a converter group for showing colors based on health percentage.
    /// </summary>
    static void RegisterHealthColorConverter()
    {
        // Create a converter group for health-based color transitions
        var colorConverter = new ConverterGroup("HealthColor");

        // Add a converter to interpolate color between colors based on health percentage
        colorConverter.AddConverter((ref float healthPercentage) =>
        {
            if (healthPercentage > 0.5f)
            {
                // Interpolate between green and yellow for health above 50%
                return new StyleColor(Color.Lerp(s_MidColor, s_FullColor, (healthPercentage - 0.5f) * 2f));
            }
            else if (healthPercentage > 0.25f)
            {
                // Interpolate between yellow and orange for health between 25% and 50%
                return new StyleColor(Color.Lerp(s_LowColor, s_MidColor, (healthPercentage - 0.25f) * 4f));
            }
            else
            {
                // Interpolate between orange and red for health below 25%
                return new StyleColor(Color.Lerp(s_CriticalColor, s_LowColor, healthPercentage * 4f));
            }
        });
        // Register the converter group globally
        ConverterGroups.RegisterConverterGroup(colorConverter);
    }
    
    /// <summary>
    /// Registers a converter group for determining health status strings 
    /// ( "Full", "Mid", "Low", "Critical") based on health percentage.
    /// </summary>
    static void RegisterStatusConverter()
    {
        var statusConverter = new ConverterGroup("HealthStatus");

        // Status string ("Full", "Mid", "Low", "Critical")
        statusConverter.AddConverter((ref float healthPercentage) =>
        {
            return healthPercentage switch
            {
                > 0.75f => "Full",
                > 0.50f => "Mid",
                > 0.25f => "Low",
                _ => "Critical"
            };
        });
        // Register the converter group globally
        ConverterGroups.RegisterConverterGroup(statusConverter);
    }

}
}