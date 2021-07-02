using DG.Tweening;
using HakoLibrary.Common;
using System;
using UnityEngine;

namespace HakoLibrary.UI
{
    public class MovableFullRectSideUIAxisY : MovableSideUI
    {
        private void Awake()
        {
            _positionMoving.x = transform.localPosition.y;
            _positionMoving.y = transform.localPosition.y - Screen.safeArea.height;

            transform.SetLocalY(_positionMoving.y);
        }

        public override void Show(Action callback = null)
        {
            _sideUI.DOKill();
            _sideUI.gameObject.SetActive(true);

            transform.DOLocalMoveY(_positionMoving.x, _moveTime).OnComplete(delegate
            {
                callback?.Invoke();
            });
        }

        public override void Hide(Action callback = null)
        {
            _sideUI.DOKill();

            transform.DOLocalMoveY(_positionMoving.y, _moveTime).OnComplete(delegate
            {
                gameObject.SetActive(false);
                callback?.Invoke();
            });
        }
    }
}
