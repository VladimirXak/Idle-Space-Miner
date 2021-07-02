using System;
using UnityEngine;

namespace HakoLibrary.UI
{
    public abstract class MovableSideUI : MonoBehaviour
    {
        [SerializeField] protected RectTransform _sideUI;
        [SerializeField] protected int _offset;
        [SerializeField] protected float _moveTime = 0.35f;

        protected Vector2 _positionMoving;

        public abstract void Show(Action callback = null);
        public abstract void Hide(Action callback = null);
    }
}
