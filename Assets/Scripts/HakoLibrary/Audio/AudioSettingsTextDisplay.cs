using HakoLibrary.LocalizationSpace;
using TMPro;
using UnityEngine;


namespace HakoLibrary.Audio
{
    public class AudioSettingsTextDisplay : MonoBehaviour, ILocalizationItem
    {
        [SerializeField] private AudioSettingsDisplay _audioSettingsDisplay;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _tmpStatusSound;
        [SerializeField] private TextMeshProUGUI _tmpStatusMusic;

        private Localization Localization => Singleton<Localization>.Instance;

        private void ChangeTextStatusSound(bool isActive)
        {
            string keySound = $"SOUND.{GetStringStatus(isActive)}";
            _tmpStatusSound.text = Localization.GetValue(keySound);
        }

        private void ChangeTextStatusMusic(bool isActive)
        {
            string keyMusic = $"MUSIC.{GetStringStatus(isActive)}";
            _tmpStatusMusic.text = Localization.GetValue(keyMusic);
        }

        private string GetStringStatus(bool isActive)
        {
            return isActive ? "ON" : "OFF";
        }

        private void OnEnable()
        {
            _audioSettingsDisplay.OnChangeActivitySound += ChangeTextStatusSound;
            _audioSettingsDisplay.OnChangeActivityMusic += ChangeTextStatusMusic;
        }

        private void OnDisable()
        {
            _audioSettingsDisplay.OnChangeActivitySound -= ChangeTextStatusSound;
            _audioSettingsDisplay.OnChangeActivityMusic -= ChangeTextStatusMusic;
        }

        public void ChangeLocalization()
        {
            Audio audio = Singleton<Audio>.Instance;

            ChangeTextStatusSound(audio.IsPlaySound);
            ChangeTextStatusMusic(audio.IsPlayMusic);
        }
    }
}
