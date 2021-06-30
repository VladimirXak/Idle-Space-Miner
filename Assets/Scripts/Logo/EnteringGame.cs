using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logo
{
    public class EnteringGame : MonoBehaviour
    {
        [SerializeField] private Logo _logo;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            _logo.Appear();
        }

        private void OnLogoAppeared()
        {
            _logo.Fade();
        }

        private void OnLogoFaded()
        {
            SceneManager.LoadSceneAsync(1);
        }

        private void OnEnable()
        {
            _logo.Appeared += OnLogoAppeared;
            _logo.Faded += OnLogoFaded;
        }

        private void OnDisable()
        {
            _logo.Appeared -= OnLogoAppeared;
            _logo.Faded -= OnLogoFaded;
        }
    }
}
