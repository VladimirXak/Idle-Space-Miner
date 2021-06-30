using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.Audio
{
    public class Audio : Singleton<Audio>
    {
        [SerializeField] private AudioSource _audioSourceSound;
        [SerializeField] private AudioSource _audioSourceMusic;
        [Space(10)]
        [SerializeField] private List<DataAudioClipTypeSound> _listDataAudioClipTypeSounds;

        public bool IsPlayMusic { get; private set; }
        public bool IsPlaySound { get; private set; }

        private AudioActivitySettings _audioActivitySettings;

        protected override void OnAwake()
        {
            _audioActivitySettings = new AudioActivitySettings();

            IsPlaySound = _audioActivitySettings.GetStatusSound();
            IsPlayMusic = _audioActivitySettings.GetStatusMusic();

            PlayMusic();
        }

        public void PlayMusic()
        {
            if (IsPlayMusic)
            {
                if (!_audioSourceMusic.isPlaying)
                    _audioSourceMusic.Play();
            }
            else
                _audioSourceMusic.Stop();
        }

        public void PlaySound(SoundType typeSound)
        {
            if (!IsPlaySound)
                return;

            AudioClip audioClip = _listDataAudioClipTypeSounds.Find(item => item.Type == typeSound).AudioClip;

            if (audioClip != null)
                _audioSourceSound.PlayOneShot(audioClip);
        }

        public void PlaySound(AudioClip audioClip)
        {
            if (!IsPlaySound)
                return;

            _audioSourceSound.PlayOneShot(audioClip);
        }

        public void ChangeStatusMusic()
        {
            IsPlayMusic = !IsPlayMusic;

            _audioActivitySettings.ChangeStatusMusic(IsPlayMusic);

            PlayMusic();
        }

        public void ChangeStatusSound()
        {
            IsPlaySound = !IsPlaySound;

            _audioActivitySettings.ChangeStatusSound(IsPlaySound);
        }
    }

    [System.Serializable]
    public class DataAudioClipTypeSound
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private SoundType _type;

        public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }
        public SoundType Type { get => _type; set => _type = value; }
    }
}
