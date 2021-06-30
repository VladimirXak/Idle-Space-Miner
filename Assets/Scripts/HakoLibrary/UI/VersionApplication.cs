using UnityEngine;
using TMPro;

namespace HakoLibrary.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class VersionApplication : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<TextMeshProUGUI>().text = $"Version {Application.version}";
        }
    }
}
