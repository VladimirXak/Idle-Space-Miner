using DG.Tweening;
using System;
using UnityEngine;

namespace HakoLibrary.UI
{
    public class MovableSideUiAxisY : MovableSideUI
    {
        [SerializeField] private MoveType _moveType;

        private void Awake()
        {
            if (_offset == 0)
                _offset = (int)_sideUI.rect.height;

            _offset *= ((_moveType == MoveType.Up) ? 1 : -1);

            _positionMoving.x = _sideUI.anchoredPosition.y;
            _positionMoving.y = _positionMoving.x + _offset;
        }

        public override void Show(Action callback = null)
        {
            _sideUI.DOKill();
            _sideUI.gameObject.SetActive(true);

            _sideUI.DOAnchorPosY(_positionMoving.y, _moveTime).OnComplete(delegate
            {
                callback?.Invoke();
            });
        }

        public override void Hide(Action callback = null)
        {
            _sideUI.DOKill();
            _sideUI.DOAnchorPosY(_positionMoving.x, _moveTime).OnComplete(delegate
            {
                _sideUI.gameObject.SetActive(false);
                callback?.Invoke();
            });
        }

        public enum MoveType
        {
            Up,
            Down
        }
    }
}
