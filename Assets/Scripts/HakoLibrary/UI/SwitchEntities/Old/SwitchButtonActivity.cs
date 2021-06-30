using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    [RequireComponent(typeof(Button))]
    public class SwitchButtonActivity : SwitchTwoEntities
    {
        private Button _button;
        public Button Button
        {
            get
            {
                if (_button == null)
                    _button = GetComponent<Button>();

                return _button;
            }
        }

        private bool _isActive = true;

        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }

        public override void Switch(bool isActive)
        {
            Button.interactable = isActive;
            _isActive = isActive;
        }

        public override bool TrySwitch(bool isActive)
        {
            if (_isActive == isActive)
                return false;

            Switch(isActive);

            return true; ;
        }
    }
}
