using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class ScnValueDisplay : MonoBehaviour, IValueDisplay<ScientificNotation>
    {
        [SerializeField] private TextMeshProUGUI _tmpValue;

        private IValue<ScientificNotation> _valueObserver;

        public void Init(IValue<ScientificNotation> valueObserver)
        {
            _valueObserver = valueObserver;

            OnEnable();
        }

        private void Render(ScientificNotation value)
        {
            _tmpValue.text = value.ToString();
        }

        private void OnEnable()
        {
            if (_valueObserver == null)
                return;

            Render(_valueObserver.Value);

            _valueObserver.ValueChanged += Render;
        }

        private void OnDisable()
        {
            if (_valueObserver == null)
                return;

            _valueObserver.ValueChanged -= Render;
        }
    }
}
