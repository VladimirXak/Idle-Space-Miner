using System;

namespace Game.UI
{
    public interface IWindowAnimation
    {
        event Action Opening;
        event Action Opened;
        event Action Closing;
        event Action Closed;

        void Open();
        void Close();
    }
}
