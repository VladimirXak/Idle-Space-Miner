using TMPro;
using UnityEngine;

namespace Game
{
    public class IntValueDisplay : MonoBehaviour, IValueDisplay<int>
    {
        [SerializeField] private TextMeshProUGUI _tmpValue;

        private IValue<int> _valueObserver;

        public void Init(IValue<int> valueObserver)
        {
            _valueObserver = valueObserver;

            OnEnable();
        }

        private void Render(int value)
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
            _valueObserver.ValueChanged -= Render;
        }
    }
}
