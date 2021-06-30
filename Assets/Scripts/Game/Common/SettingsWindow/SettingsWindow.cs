using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private LanguageWindow _languageWindow;
        [Space(10)]
        [SerializeField] private Button _selectLanguageButton;

        private void Awake()
        {
            _selectLanguageButton.onClick.AddListener(() => _languageWindow.WindowAnimation.Open());
        }
    }
}
