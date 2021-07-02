namespace Game
{
    public interface IValueDisplay<T>
    {
        void Init(IValue<T> valueObserver);
    }
}
