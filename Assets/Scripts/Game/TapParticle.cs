using UnityEngine;
using HakoLibrary.Pooling;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class TapParticle : PoolObject
    {
        private ParticleSystem _particleSystem;

        private bool _isPlaying;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Play()
        {
            gameObject.SetActive(true);

            _particleSystem.Play();
            _isPlaying = true;
        }

        private void Update()
        {
            if (_isPlaying == false)
                return;

            if (_particleSystem.isPlaying == false)
            {
                _isPlaying = false;
                Return();
            }
        }
    }
}
