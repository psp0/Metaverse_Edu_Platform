
namespace Demos
{
    /// <summary>
    /// Extended HealthBar that changes the progress bar color dynamically based on health.
    /// </summary>
public class HealthBarWithConverter : HealthBar
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod] // Register in Editor for UI Builder
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] // Ensure it's registered at runtime
#endif
    public static void RegisterConverters()
    {
        HealthDataConverter.Register();
    }
}
}