using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Rope Rope { get; private set; }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ExitBorderTrigger exitBorder))
            {
                GameController.Instance.GameOver();
            }
        }
    }
}