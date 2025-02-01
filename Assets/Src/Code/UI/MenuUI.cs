using Assets.Src.Code.Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Src.Code.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private Button _soundButton, _settingsButton, _startButton;
        [SerializeField] private GameObject _soundOffIcon;
        [SerializeField] private RectTransform _settingsWindow;
   
        private void Start()
        {
            SwitchSound();

            _soundButton.onClick.AddListener(SoundSettings);
            _startButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(Settings);
        }

        private void SoundSettings()
        {
            GameAudio.Instance.PlayClickSound();

            if (PlayerPrefs.GetInt("DisableSound") == 1)
            {
                PlayerPrefs.SetInt("DisableSound", 0);
            }
            else if (PlayerPrefs.GetInt("DisableSound") == 0)
            {
                PlayerPrefs.SetInt("DisableSound", 1);
            }

            SwitchSound();
        }

        private void SwitchSound()
        {
            if (PlayerPrefs.GetInt("DisableSound") == 1)
            {
                _soundOffIcon.SetActive(true);
                GameAudio.Instance.SwitchSound(0);
                PlayerPrefs.SetInt("DisableSound", 1);
            }
            else if (PlayerPrefs.GetInt("DisableSound") == 0)
            {
                _soundOffIcon.SetActive(false);
                GameAudio.Instance.SwitchSound(1);
                PlayerPrefs.SetInt("DisableSound", 0);
            }
        }

        private void Play()
        {
            GameAudio.Instance.PlayClickSound();

            Debug.LogError("Load scene[1], but should be guide");
            SceneManager.LoadScene(1);
        }

        private void Settings()
        {
            GameAudio.Instance.PlayClickSound();

            _settingsButton.interactable = false;

            if (_settingsWindow.gameObject.activeInHierarchy == false)
            {
               _settingsWindow.DOAnchorPos(new Vector2(-150, -500), 0.5f)
                    .OnStart(() => _settingsWindow.gameObject.SetActive(true))
                    .OnComplete(() => _settingsButton.interactable = true);
            }
            else if (_settingsWindow.gameObject.activeInHierarchy)
            {
               _settingsWindow.DOAnchorPos(new Vector2(-150, 0), 0.5f)
                    .OnComplete(() => SettingsCloseWindowTweenAction());
            }
        }

        private void SettingsCloseWindowTweenAction()
        {
            _settingsWindow.gameObject.SetActive(false);
            _settingsButton.interactable = true;
        }

        private void OnDestroy()
        {
            _soundButton.onClick.RemoveListener(SoundSettings);
            _startButton.onClick.RemoveListener(Play);
            _settingsButton.onClick.RemoveListener(Settings);
        }
    }
}