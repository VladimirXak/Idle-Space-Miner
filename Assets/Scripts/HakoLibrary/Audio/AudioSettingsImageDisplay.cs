using UnityEngine;
using HakoLibrary.UI;

namespace HakoLibrary.Audio
{
    public class AudioSettingsImageDisplay : MonoBehaviour
    {
        [SerializeField] private AudioSettingsDisplay _audioSettingsDisplay;
        [Space(10)]
        [SerializeField] private SwitchTwoImageColors _switchImageSpriteSound;
        [SerializeField] private SwitchTwoImageColors _switchImageSpriteMusic;

        private void ChangeImageActivitySound(bool isActive)
        {
            _switchImageSpriteSound.TrySwitch(isActive);
        }

        private void ChangeImageActivityMusic(bool isActive)
        {
            _switchImageSpriteMusic.TrySwitch(isActive);
        }

        private void OnEnable()
        {
            _audioSettingsDisplay.OnChangeActivitySound += ChangeImageActivitySound;
            _audioSettingsDisplay.OnChangeActivityMusic += ChangeImageActivityMusic;
        }

        private void OnDisable()
        {
            _audioSettingsDisplay.OnChangeActivitySound -= ChangeImageActivitySound;
            _audioSettingsDisplay.OnChangeActivityMusic -= ChangeImageActivityMusic;
        }
    }
}
