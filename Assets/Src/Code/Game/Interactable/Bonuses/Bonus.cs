using DG.Tweening;
using UnityEngine;

namespace Assets.Src.Code.Game.Interactable.Bonuses
{
    public class Bonus : SpawnObject
    {
        private Vector3 _startScale;

        private void Start()
        {
            _startScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.DOScale(new Vector3(30, 28, 1), 0.5f)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            transform.localScale = _startScale;
        }
    }
}