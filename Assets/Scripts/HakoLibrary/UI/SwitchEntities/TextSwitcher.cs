using HakoLibrary.LocalizationSpace;
using UnityEngine;

namespace HakoLibrary.UI
{
    public class TextSwitcher : MonoBehaviour
    {
        [SerializeField] private Switch _switch;
        [SerializeField] private LocalizationTmp _text;
        [Space(10)]
        [SerializeField] private string _textOn;
        [SerializeField] private string _textOff;

        private void OnSwitchStateChanged(bool isOn)
        {
            _text.SetKey(isOn ? _textOn : _textOff);
        }

        private void OnEnable()
        {
            _switch.StateChanged += OnSwitchStateChanged;
        }

        private void OnDisable()
        {
            _switch.StateChanged -= OnSwitchStateChanged;
        }
    }
}
