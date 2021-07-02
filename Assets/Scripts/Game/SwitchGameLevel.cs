using HakoLibrary.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class SwitchGameLevel : MonoBehaviour
    {
        [SerializeField] private Switch _previousLevelSwitch;
        [SerializeField] private Switch _nextLevelSwitch;
        [Space(10)]
        [SerializeField] private Button _previousLevelButton;
        [SerializeField] private Button _nextLevelButton;

        private GameLevel _level;

        [Inject]
        private void Construct(GameLevel level)
        {
            _level = level;
        }

        private void Awake()
        {
            _previousLevelButton.onClick.AddListener(GoToPreviousLevel);
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
        }

        private void GoToNextLevel()
        {
            _level.GoToNextLevel();
        }

        private void GoToPreviousLevel()
        {
            _level.GoToPreviousLevel();
        }

        private void SwitchActivityButton(int value)
        {
            _previousLevelSwitch.TrySetState(value % 25 == 0 && _level.Value > _level.MaxPassedValue);
            _nextLevelSwitch.TrySetState(value <= _level.MaxPassedValue);
        }

        private void OnEnable()
        {
            SwitchActivityButton(_level.Value);

            _level.ValueChanged += SwitchActivityButton;
        }

        private void OnDisable()
        {
            _level.ValueChanged -= SwitchActivityButton;
        }
    }
}
