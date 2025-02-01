using UnityEngine;

namespace Assets.Src.Code.Controllers
{
    public class GameAudio : MonoBehaviour
    {
        public static GameAudio Instance { get; private set; }

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _addHealth, _removeHealth, _bonus, _rope, _click, _endBonus;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        public void SwitchSound(int volume)
        => _audioSource.volume = volume;

        public void PlayAddHealthSound() => _audioSource.PlayOneShot(_addHealth, 0.5f);
        public void PlayRemoveHealthSound() => _audioSource.PlayOneShot(_removeHealth, 0.5f);
        public void PlayBonusSound() => _audioSource.PlayOneShot(_bonus);
        public void PlayEndBonusSound() => _audioSource.PlayOneShot(_endBonus);
        public void PlayRopeSound() => _audioSource.PlayOneShot(_rope);
        public void PlayClickSound() => _audioSource.PlayOneShot(_click);
    }
}