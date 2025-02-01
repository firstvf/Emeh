using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game.Interactable
{
    public class Enemy : SpawnObject
    {
        protected override void InteractAction(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                GameController.Instance.RemoveHealth();
                gameObject.SetActive(false);
            }
        }
    }
}