using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    public class SwitchTwoImageColors : SwitchTwoEntities
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _first;
        [SerializeField] private Color _second;

        protected bool _isFirst = true;

        public override bool TrySwitch(bool isFirst)
        {
            if (_isFirst == isFirst)
                return false;

            Switch(isFirst);

            return true;
        }

        public override void Switch(bool isFirst)
        {
            _isFirst = isFirst;

            if (isFirst)
                _image.color = _first;
            else
                _image.color = _second;
        }
    }
}
