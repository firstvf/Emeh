using Assets.Src.Code.Controllers;
using Assets.Src.Code.Game.Interactable;
using Assets.Src.Code.Game.Interactable.Bonuses;
using System.Collections;
using UnityEngine;

namespace Assets.Src.Code.Spawner
{
    public class GameSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _poolParent;
        [SerializeField] private Hearth _hearth;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private StickySphere _stickySphere;
        [SerializeField] private SpeedDownSphere _speedDown;
        [SerializeField] private SpeedUpSphere _speedUp;
        [SerializeField] private SpeedRopeSphere _speedRope;

        private ObjectPooler<SpawnObject> _enemyPool, _stickySpherePool
            , _hearthPool, _speedUpPool, _speedDownPool, _speedRopePool;

        private void Start()
        {
            StartPool();
            GameController.Instance.OnHealthChangeHandler += DisablePools;
        }

        private void DisablePools(int hp)
        {
            if (hp <= 0)
            {
                _poolParent.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        private void StartPool()
        {
            _enemyPool = new(_enemy, _poolParent, 5);
            _stickySpherePool = new(_stickySphere, _poolParent, 5);
            _hearthPool = new(_hearth, _poolParent, 5);

            _speedUpPool = new(_speedUp, _poolParent, 2);
            _speedDownPool = new(_speedDown, _poolParent, 2);
            _speedRopePool = new(_speedRope, _poolParent, 2);


            var item = _stickySpherePool.GetFreeObjectFromPool();
            item.GetRect().anchoredPosition = new Vector2(0, 500f);

            StartCoroutine(SpawnCoroutine(_enemyPool, new WaitForSeconds(7.65f)));
            StartCoroutine(SpawnCoroutine(_stickySpherePool, new WaitForSeconds(1.7f), true));
            StartCoroutine(SpawnCoroutine(_hearthPool, new WaitForSeconds(8.5f)));

            // bonuses
            StartCoroutine(SpawnCoroutine(_speedUpPool, new WaitForSeconds(10)));
            StartCoroutine(SpawnCoroutine(_speedDownPool, new WaitForSeconds(12)));
            StartCoroutine(SpawnCoroutine(_speedRopePool, new WaitForSeconds(7)));
        }

        private Vector2 GetSpawnPosition(bool isStickySphere)
        {
            switch (isStickySphere)
            {
                case true:
                    int random = Random.Range(0, 3);

                    if (random == 0)
                        return new Vector2(400f, 800f);
                    else if (random == 1)
                        return new Vector2(-400f, 800f);

                    return new Vector2(0, 800f);

                case false:
                    return GetSpawnInteractableObjectPosition();
            }
        }

        private Vector2 GetSpawnInteractableObjectPosition()
        {
            int random = Random.Range(0, 3);

            if (random == 0)
                return new Vector2(Random.Range(380, 250), 800f);
            else if (random == 1)
                return new Vector2(Random.Range(-380, -250), 800f);

            return new Vector2(Random.Range(-100, 100), 800f);
        }

        private IEnumerator SpawnCoroutine<T>(ObjectPooler<T> pool, WaitForSeconds time, bool isStickySphere = false) where T : SpawnObject
        {
            while (true)
            {
                yield return time;
                var item = pool.GetFreeObjectFromPool();
                item.GetRect().anchoredPosition = GetSpawnPosition(isStickySphere);
            }
        }

        private void OnDestroy()
        {
            GameController.Instance.OnHealthChangeHandler -= DisablePools;
        }
    }
}