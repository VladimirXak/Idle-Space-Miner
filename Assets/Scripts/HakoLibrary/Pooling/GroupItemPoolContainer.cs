using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.Pooling
{
    public class GroupItemPoolContainer : MonoBehaviour, IPoolContainer
    {
        [SerializeField] private Transform _parentItems;
        [SerializeField] private bool _isReturnToParent;
        [SerializeField] private string _pathItemsEnemy;

        private List<PoolObject> _pool;

        private PoolObject[] _prefabsCollection;

        private void Awake()
        {
            _pool = new List<PoolObject>();

            if (_parentItems == null)
                _parentItems = transform;

            _prefabsCollection = Resources.LoadAll<PoolObject>(_pathItemsEnemy);

            foreach (var prefab in _prefabsCollection)
                AddItem(prefab);
        }

        public T GetItem<T>() where T : PoolObject
        {
            if (_pool.Count == 0)
                AddRandomItem();

            int rnd = Random.Range(0, _pool.Count);

            PoolObject item = _pool[rnd];
            _pool.RemoveAt(rnd);

            return (T)item;
        }

        public void Return(PoolObject item)
        {
            item.transform.gameObject.SetActive(false);

            if (_isReturnToParent)
                item.transform.SetParent(_parentItems);

            _pool.Add(item);
        }

        private void AddRandomItem()
        {
            AddItem(_prefabsCollection[Random.Range(0, _prefabsCollection.Length)]);
        }

        private void AddItem(PoolObject item)
        {
            var newItem = Instantiate(item, _parentItems);
            newItem.PoolContainer = this;

            newItem.transform.gameObject.SetActive(false);

            _pool.Add(newItem);
        }
    }
}
