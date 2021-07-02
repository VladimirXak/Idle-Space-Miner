using System;

namespace Game
{
    public interface IHealth<T> : IValue<T>
    {
        event Action<T> Initialized;
        event Action Died;

        void SetHealth(T value);
        void GetDamage(T value);
    }
}
