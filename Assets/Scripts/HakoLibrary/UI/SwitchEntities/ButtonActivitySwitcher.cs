using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    public class ButtonActivitySwitcher : MonoBehaviour
    {
        [SerializeField] private Switch _switch;
        [SerializeField] private Button _button;

        private void OnSwitchStateChanged(bool isOn)
        {
            _button.interactable = isOn;
        }

        private void OnEnable()
        {
            OnSwitchStateChanged(_switch.IsOn);

            _switch.StateChanged += OnSwitchStateChanged;
        }

        private void OnDisable()
        {
            _switch.StateChanged -= OnSwitchStateChanged;
        }
    }
}
