using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ProgressBarHealth : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;

        private ScientificNotation _maxValue;

        private IHealth<ScientificNotation> _health;

        public void Init(IHealth<ScientificNotation> health)
        {
            _health = health;
            _health.Initialized += ResetProgress;

            OnEnable();
        }

        private void ResetProgress(ScientificNotation value)
        {
            _maxValue = value;
            _progressBar.fillAmount = 1;
        }

        private void RenderFillAmount(ScientificNotation value)
        {
            float fillAmount = ScientificNotation.Procent(_maxValue, value);

            _progressBar.fillAmount = fillAmount;
        }

        private void OnEnable()
        {
            if (_health == null)
                return;

            RenderFillAmount(_health.Value);

            _health.ValueChanged += RenderFillAmount;
        }

        private void OnDisable()
        {
            _health.ValueChanged -= RenderFillAmount;
        }

        private void OnDestroy()
        {
            _health.Initialized -= ResetProgress;
        }
    }
}
