using UnityEngine;
using UnityEngine.UIElements;
using Quiz;

namespace Demos
{
    
    public class MuteAudioToggle : MonoBehaviour
    {
        [SerializeField] AudioSettingsSO m_AudioSettingsSO;
        [SerializeField] UIDocument m_Document;

        void OnEnable()
        {
            var root = m_Document.rootVisualElement;
            SlideToggle slideToggle = root.Q<SlideToggle>("master-audio-toggle");

            if (slideToggle != null)
            {
                slideToggle.value = !m_AudioSettingsSO.IsMasterMuted;

                slideToggle.RegisterValueChangedCallback(evt => m_AudioSettingsSO.IsMasterMuted = !evt.newValue);
            }
        }
    }

}