using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    public class SwitchCustomButtonImageActivity : SwitchTwoEntities
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [Space(10)]
        [SerializeField] private Sprite _active;
        [SerializeField] private Sprite _inactive;

        public Button Button => _button;

        private bool _isActive = true;

        public override void Switch(bool isActive)
        {
            _button.interactable = isActive;
            _isActive = isActive;

            _image.sprite = isActive ? _active : _inactive;
        }

        public override bool TrySwitch(bool isActive)
        {
            if (_isActive == isActive)
                return false;

            Switch(isActive);

            return true;
        }
    }
}
