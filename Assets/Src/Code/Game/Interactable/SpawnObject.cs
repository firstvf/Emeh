using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game.Interactable
{
    public class SpawnObject : MonoBehaviour, IGetRect
    {
        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            transform.position
                += Vector3.down * Time.deltaTime * GameController.Instance.GameSpeed;
        }

        public RectTransform GetRect()
        => _rect;

        virtual protected void InteractAction(Collider2D collision) { }

        private void OnTriggerEnter2D(Collider2D collision)
        => InteractAction(collision);
    }
}