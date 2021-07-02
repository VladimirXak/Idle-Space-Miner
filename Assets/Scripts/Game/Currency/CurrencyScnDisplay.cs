using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class CurrencyScnDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmpValue;

        private ICurrency<ScientificNotation> _currencyScn;

        public void Init(ICurrency<ScientificNotation> currencyScn)
        {
            _currencyScn = currencyScn;

            Render(_currencyScn.Value);

            _currencyScn.ValueChanged += Render;
        }

        public void Render(ScientificNotation value)
        {
            _tmpValue.text = value.ToString();
        }

        private void OnDestroy()
        {
            _currencyScn.ValueChanged -= Render;
        }

        //[SerializeField] private Button _addCurrency;

        //private void Awake()
        //{
        //    _addCurrency = GetComponent<Button>();
        //    _addCurrency.onClick.AddListener(AddCurrency);
        //}

        //private void AddCurrency()
        //{
        //    _currencyScn.Add(_currencyScn.Value * 4);
        //}
    }
}
