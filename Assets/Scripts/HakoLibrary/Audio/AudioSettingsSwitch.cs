using HakoLibrary.UI;
using UnityEngine;

namespace HakoLibrary.Audio
{
    public class AudioSettingsSwitch : MonoBehaviour
    {
        [SerializeField] private AudioSettingsDisplay _audioSettingsDisplay;
        [Space(10)]
        [SerializeField] private Switch _switchSound;
        [SerializeField] private Switch _switchMusic;

        private void OnActivitySoundChanged(bool isActive)
        {
            _switchSound.TrySetState(isActive);
        }

        private void OnActivityMusicChanged(bool isActive)
        {
            _switchMusic.TrySetState(isActive);
        }

        private void OnEnable()
        {
            _audioSettingsDisplay.OnChangeActivitySound += OnActivitySoundChanged;
            _audioSettingsDisplay.OnChangeActivityMusic += OnActivityMusicChanged;
        }

        private void OnDisable()
        {
            _audioSettingsDisplay.OnChangeActivitySound -= OnActivitySoundChanged;
            _audioSettingsDisplay.OnChangeActivityMusic -= OnActivityMusicChanged;
        }
    }
}
