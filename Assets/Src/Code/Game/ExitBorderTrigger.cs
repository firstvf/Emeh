using UnityEngine;

namespace Assets.Src.Code.Game
{
    public class ExitBorderTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.SetActive(false);
        }
    }
}