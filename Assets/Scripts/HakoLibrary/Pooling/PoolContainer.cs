using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.Pooling
{
    public class PoolContainer : MonoBehaviour, IPoolContainer
    {
        [SerializeField] private PoolObject _prefabPoolObject;
        [SerializeField] private Transform _parentItems;
        [SerializeField] private int _initialQuantityItems;
        [SerializeField] private bool _isReturnToParent;

        private Queue<PoolObject> _pool = new Queue<PoolObject>();

        private void Awake()
        {
            if (_parentItems == null)
                _parentItems = transform;

            for (int i = 0; i < _initialQuantityItems; i++)
                AddItem();
        }

        public T GetItem<T>() where T : PoolObject
        {
            if (_pool.Count == 0)
                AddItem();

            return (T)_pool.Dequeue();
        }

        public void Return(PoolObject item)
        {
            item.transform.gameObject.SetActive(false);

            if (_isReturnToParent)
                item.transform.SetParent(_parentItems);

            _pool.Enqueue(item);
        }

        private void AddItem()
        {
            var newItem = (PoolObject)Instantiate(_prefabPoolObject, _parentItems);
            newItem.PoolContainer = this;

            var itemTransform = newItem.transform;
            itemTransform.SetParent(_parentItems);
            itemTransform.gameObject.SetActive(false);

            _pool.Enqueue(newItem);
        }
    }
}
