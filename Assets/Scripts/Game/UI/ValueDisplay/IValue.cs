using System;

public interface IValue<T>
{
    event Action<T> ValueChanged;

    T Value { get; }
}
