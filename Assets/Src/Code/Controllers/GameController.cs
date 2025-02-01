using Assets.Src.Code.Game;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Src.Code.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public Action<int> OnHealthChangeHandler { get; set; }
        public Action<int> OnScoreChangeHandler { get; set; }
        [field: SerializeField] public Player Player { get; private set; }
        [field: SerializeField] public float GameSpeed { get; private set; }

        private int _health;
        private int _score;
        private CancellationTokenSource _cancellationToken;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            _health = 3;
        }

        public void ChangeSpeed(float speed)
        {
            _cancellationToken?.Cancel();

            _cancellationToken = new CancellationTokenSource();
            ChangeSpeedTask(_cancellationToken, speed).Forget();
        }

        private async UniTaskVoid ChangeSpeedTask(CancellationTokenSource cancellationToken, float gameSpeed)
        {
            GameSpeed = gameSpeed;

            await UniTask.Delay(15000);
            if (cancellationToken.IsCancellationRequested == false)
                GameSpeed = 1;
        }

        public void AddScore()
        {
            _score++;
            OnScoreChangeHandler?.Invoke(_score);
        }

        public void AddHealth()
        {
            GameAudio.Instance.PlayAddHealthSound();

            if (_health < 3)
            {
                _health++;
                OnHealthChangeHandler?.Invoke(_health);
            }
        }

        public void RemoveHealth(bool isPlayerDie = false)
        {
            GameAudio.Instance.PlayRemoveHealthSound();

            if (isPlayerDie)
            {
                _health = 0;
                OnHealthChangeHandler?.Invoke(_health);
            }

            if (_health > 0)
            {
                _health--;
                OnHealthChangeHandler?.Invoke(_health);
            }
        }

        public void SwitchPauseGame(bool isPause)
        {
            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        public void GameOver()
        {
            RemoveHealth(true);
        }
    }
}