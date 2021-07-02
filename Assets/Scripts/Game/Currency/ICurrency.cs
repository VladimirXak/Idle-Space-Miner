using System;

namespace Game
{
    public interface ICurrency<T> : IValue<T>, IAdditive<T>
    {
        event Action<T> FailedSpend;
        bool TrySpend(T value);
    }
}
