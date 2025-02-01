using Assets.Src.Code.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Src.Code.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Sprite _redHeart, _blackHearth;
        [SerializeField] private Image[] _hearts;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _mainMenuButton, _restartButton, _openGuideButton, _closeGuideButton;
        [SerializeField] private GameObject _gameOverWindow, _guideMenu;

        private void Start()
        {
            UpdateScore(0);

            GameController.Instance.OnHealthChangeHandler += UpdateHeartsInfo;
            GameController.Instance.OnScoreChangeHandler += UpdateScore;
            _mainMenuButton.onClick.AddListener(MainMenu);
            _restartButton.onClick.AddListener(Restart);
            _openGuideButton.onClick.AddListener(OpenGuideMenu);
            _closeGuideButton.onClick.AddListener(CloseGuideMenu);

            if (PlayerPrefs.GetInt("GuideComplete") == 0)
            {
                _guideMenu.SetActive(true);
                GameController.Instance.SwitchPauseGame(true);
            }
        }

        private void OpenGuideMenu()
        {
            GameAudio.Instance.PlayClickSound();

            _guideMenu.SetActive(true);
            GameController.Instance.SwitchPauseGame(true);
        }

        private void CloseGuideMenu()
        {
            GameAudio.Instance.PlayClickSound();

            _guideMenu.SetActive(false);
            GameController.Instance.SwitchPauseGame(false);
            PlayerPrefs.SetInt("GuideComplete", 1);
        }

        private void MainMenu()
        {
            GameController.Instance.SwitchPauseGame(false, isForced: true);
            GameAudio.Instance.PlayClickSound();
            SceneManager.LoadScene(0);
        }

        private void Restart()
        {
            GameController.Instance.SwitchPauseGame(false, isForced: true);
            GameAudio.Instance.PlayClickSound();
            SceneManager.LoadScene(1);
        }

        private void UpdateHeartsInfo(int hp)
        {
            int currentHP = hp;

            for (int i = 0; i < _hearts.Length; i++)
            {
                if (currentHP > 0)
                {
                    currentHP--;
                    _hearts[i].sprite = _redHeart;
                }
                else
                    _hearts[i].sprite = _blackHearth;
            }

            if (hp <= 0)
            {
                GameOverWindow();
            }
        }

        private void GameOverWindow()
        {
            _gameOverWindow.SetActive(true);
        }

        private void UpdateScore(int score)
        => _score.text = score.ToString();

        private void OnDestroy()
        {
            GameController.Instance.OnHealthChangeHandler -= UpdateHeartsInfo;
            GameController.Instance.OnScoreChangeHandler -= UpdateScore;
            _mainMenuButton.onClick.RemoveListener(MainMenu);
            _restartButton.onClick.RemoveListener(Restart);
            _openGuideButton.onClick.RemoveListener(OpenGuideMenu);
            _closeGuideButton.onClick.RemoveListener(CloseGuideMenu);
        }
    }
}