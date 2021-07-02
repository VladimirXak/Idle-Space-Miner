using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using HakoLibrary.Pooling;

namespace Game
{
    public class Enemy : PoolObject
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [Space(10)]
        [SerializeField] private List<Material> _listMaterials;

        public void Init(Transform parent)
        {
            //transform.SetParent(parent, true);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);

            _meshRenderer.material = _listMaterials[Random.Range(0, _listMaterials.Count)];

            transform.localRotation = Quaternion.Euler(GetRandomRotate());

            ShowAnimation(0.25f);
        }

        public void ShowAnimation(float time)
        {
            transform.DOScale(1, time);
        }

        public override void Return()
        {
            transform.DOScale(0, 0.25f).OnComplete(delegate
            {
                Return();
            });
        }

        private Vector3 GetRandomRotate()
        {
            float x = Random.Range(0, 360);
            float y = Random.Range(0, 360);
            float z = Random.Range(0, 360);

            return new Vector3(x, y, z);
        }

        private void OnEnable()
        {
            transform.DOLocalRotate(new Vector3(720, 720, 720), 20f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(delegate
            {
                OnEnable();
            });
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}
