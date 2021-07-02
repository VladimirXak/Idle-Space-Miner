using DG.Tweening;
using System;

namespace HakoLibrary.UI
{
    public class MovableSideUIAxisX : MovableSideUI
    {
        private void Awake()
        {
            _positionMoving.x = _sideUI.anchoredPosition.x;
            _positionMoving.y = _positionMoving.x + _sideUI.rect.width;
        }

        public override void Show(Action callback = null)
        {
            _sideUI.DOAnchorPosX(_positionMoving.y, _moveTime).OnComplete(delegate 
            {
                callback?.Invoke();
            });
        }

        public override void Hide(Action callback = null)
        {
            _sideUI.DOAnchorPosX(_positionMoving.x, _moveTime).OnComplete(delegate 
            {
                callback?.Invoke();
            });
        }
    }
}
