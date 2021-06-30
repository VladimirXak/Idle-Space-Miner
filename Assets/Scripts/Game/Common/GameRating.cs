using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameRating : MonoBehaviour
    {
        [SerializeField] private Button _rateGameButton;
        [Space(10)]
        [SerializeField] private string _url;

        private void Awake()
        {
            _rateGameButton.onClick.AddListener(RateGame);
        }

        private void RateGame()
        {
            Application.OpenURL(_url);
        }
    }
}
