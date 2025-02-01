using Assets.Src.Code.Controllers;
using Assets.Src.Code.Game;
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

        private ObjectPooler<SpawnObject> _enemyPool, _stickySpherePool, _hearthPool;

        [Range(0, 10)]
        [SerializeField] private float _stickySphereTime;
        [Range(0, 10)]
        [SerializeField] private float _enemyTime;
        [Range(0, 10)]
        [SerializeField] private float _hearthTime;

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

            var item = _stickySpherePool.GetFreeObjectFromPool();
            item.GetRect().anchoredPosition = new Vector2(0, 500f);

            StartCoroutine(SpawnCoroutine(_enemyPool, new WaitForSeconds(_enemyTime)));
            StartCoroutine(SpawnCoroutine(_stickySpherePool, new WaitForSeconds(_stickySphereTime), true));
            StartCoroutine(SpawnCoroutine(_hearthPool, new WaitForSeconds(_hearthTime)));
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