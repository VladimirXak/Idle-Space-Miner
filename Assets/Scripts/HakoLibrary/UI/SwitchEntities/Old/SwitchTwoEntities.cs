using UnityEngine;

namespace HakoLibrary.UI
{
    public abstract class SwitchTwoEntities : MonoBehaviour
    {
        public abstract bool TrySwitch(bool isFirst);
        public abstract void Switch(bool isFirst);
    }
}
