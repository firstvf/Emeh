using Assets.Src.Code.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Src.Code.Game.Interactable
{
    public class StickySphere : SpawnObject, IPointerClickHandler
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        private bool _isRopeJoint;
        private bool _isInitialized;

        private void Start()
        {
            _isInitialized = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isRopeJoint == false)
                GameController.Instance.Player.Rope.SetStickySphere(this);
            else
            {
                GameController.Instance.Player.Rope.DisableRope();
            }
        }

        public void SwitchJointRope(bool isJoint)
        => _isRopeJoint = isJoint;

        private void OnDisable()
        {
            if (_isRopeJoint)
            {
                GameController.Instance.Player.Rope.DisableRope();
            }

            if (_isInitialized)
            {
                GameController.Instance.AddScore();
            }
        }
    }
}