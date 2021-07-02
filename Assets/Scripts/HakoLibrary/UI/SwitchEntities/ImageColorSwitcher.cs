using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    public class ImageColorSwitcher : MonoBehaviour
    {
        [SerializeField] private Switch _switch;
        [SerializeField] private Image _image;
        [Space(10)]
        [SerializeField] private Color _colorOn;
        [SerializeField] private Color _colorOff;

        private void OnSwitchStateChanged(bool isOn)
        {
            _image.color = isOn ? _colorOn : _colorOff;
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
