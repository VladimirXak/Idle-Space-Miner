using Game.Data;

namespace Game
{
    public interface ISaveLoadSavedGame
    {
        void Save(SavedData savedData);
        SavedData Load();
        SavedData CreateStartSavedData();
    }
}
