using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    public class SwitchTwoImages : SwitchTwoEntities
    {
        [SerializeField] private Image _image;
        [Space(10)]
        [SerializeField] private Sprite _firstSprite;
        [SerializeField] private Sprite _secondSprite;

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
                _image.sprite = _firstSprite;
            else
                _image.sprite = _secondSprite;
        }
    }
}
