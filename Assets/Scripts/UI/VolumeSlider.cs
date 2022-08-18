using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _mixerGroup;
        
        // Start is called before the first frame update
        void Start()
        {
            var slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(SliderValueChanged);
            
            var value = PlayerPrefs.GetFloat(_mixerGroup, 0.75f);
            slider.value = value;
        }

        private void SliderValueChanged(float newValue)
        {
            _mixer.SetFloat(_mixerGroup, Mathf.Log10(newValue) * 20f);
            PlayerPrefs.SetFloat(_mixerGroup, newValue);
            PlayerPrefs.Save();
        }
    }
}
