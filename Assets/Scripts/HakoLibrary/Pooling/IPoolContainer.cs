namespace HakoLibrary.Pooling
{
    public interface IPoolContainer
    {
        T GetItem<T>() where T : PoolObject;
        void Return(PoolObject item);
    }
}
