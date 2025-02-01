using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game.Interactable.Bonuses
{
    public class SpeedDownSphere : Bonus
    {
        protected override void InteractAction(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                GameAudio.Instance.PlayBonusSound();
                GameController.Instance.ChangeSpeed(0.5f);
                gameObject.SetActive(false);
            }
        }
    }
}