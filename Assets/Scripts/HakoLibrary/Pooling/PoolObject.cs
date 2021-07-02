using UnityEngine;

namespace HakoLibrary.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        public IPoolContainer PoolContainer { get; set; }

        public virtual void Return()
        {
            PoolContainer.Return(this);
        }
    }
}
