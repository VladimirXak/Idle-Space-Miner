using TMPro;
using UnityEngine;

namespace HakoLibrary.UI
{
    public class MultiplierDisplay : MonoBehaviour
    {
        [SerializeField] private Multiplier _multiplier;
        [SerializeField] private TextMeshProUGUI _tmpValue;

        private void Render(int value)
        {
            _tmpValue.text = $"x{value}";
        }

        private void OnEnable()
        {
            Render(_multiplier.Value);
            _multiplier.OnChange += Render;
        }

        private void OnDisable()
        {
            _multiplier.OnChange -= Render;
        }
    }
}
