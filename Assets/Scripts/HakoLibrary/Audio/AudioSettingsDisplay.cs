using UnityEngine;
using UnityEngine.UI;
using System;
using HakoLibrary.UI;

namespace HakoLibrary.Audio
{
    public class AudioSettingsDisplay : MonoBehaviour
    {
        [SerializeField] private Button _switchSoundButton;
        [SerializeField] private Button _switchMusicButton;

        public event Action<bool> OnChangeActivitySound;
        public event Action<bool> OnChangeActivityMusic;

        private Audio _audio;

        private void Awake()
        {
            _switchSoundButton.onClick.AddListener(ChangeStatusSound);
            _switchMusicButton.onClick.AddListener(ChangeStatusMusic);

            _audio = Singleton<Audio>.Instance;
        }

        private void Start()
        {
            OnChangeActivitySound?.Invoke(_audio.IsPlaySound);
            OnChangeActivityMusic?.Invoke(_audio.IsPlayMusic);
        }

        private void ChangeStatusSound()
        {
            _audio.ChangeStatusSound();
            OnChangeActivitySound?.Invoke(_audio.IsPlaySound);
        }

        private void ChangeStatusMusic()
        {
            _audio.ChangeStatusMusic();
            OnChangeActivityMusic?.Invoke(_audio.IsPlayMusic);
        }
    }
}
