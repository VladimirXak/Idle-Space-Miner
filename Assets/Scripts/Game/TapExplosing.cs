using UnityEngine;
using HakoLibrary.Pooling;

namespace Game
{
    public class TapExplosing : MonoBehaviour
    {
        [SerializeField] private TapOnField _tapOnField;
        [SerializeField] private PoolContainer _poolContainer;

        private void SpawnExplosing()
        {
            var item = _poolContainer.GetItem<TapParticle>();

            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            item.transform.position = worldMousePosition;
            item.Play();
        }

        private void OnEnable()
        {
            _tapOnField.OnTap += SpawnExplosing;
        }

        private void OnDisable()
        {
            _tapOnField.OnTap -= SpawnExplosing;
        }
    }
}
