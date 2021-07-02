using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class TapOnField : MonoBehaviour
    {
        public event Action OnTap;

        private void Update()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject(0))
                return;
#else
            if (EventSystem.current.IsPointerOverGameObject())
                return;
#endif

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Collider2D tap = Physics2D.OverlapPoint(worldMousePosition);

                if (tap != null)
                    OnTap?.Invoke();
            }
        }
    }
}
