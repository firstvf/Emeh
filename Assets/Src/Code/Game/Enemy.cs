using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game
{
    public class Enemy : SpawnObject
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                GameController.Instance.RemoveHealth();
                gameObject.SetActive(false);
            }
        }
    }
}