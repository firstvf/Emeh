using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game
{
    public class Hearth : SpawnObject
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                GameController.Instance.AddHealth();
                gameObject.SetActive(false);
            }
        }
    }
}