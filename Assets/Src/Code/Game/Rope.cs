using Assets.Src.Code.Controllers;
using UnityEngine;

namespace Assets.Src.Code.Game
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private SpringJoint2D _joint;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _pullingSpeed;
        public StickySphere _target; // Цель (B)        
        private bool _isRopeActive;

        private void Update()
        {
            UpdateRopePosition();
        }

        private void CreateRope()
        {
            _joint.enabled = true;
            _lineRenderer.enabled = true;

            _joint.connectedBody = _target.Rigidbody2D;
            _joint.autoConfigureDistance = false;
            _joint.distance = Vector2.Distance(transform.position, _target.transform.position);
            _joint.dampingRatio = 1f;  // Гашение колебаний
            _joint.frequency = 1f;     // Жёсткость пружины

            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = Color.green;
            _lineRenderer.endColor = Color.green;
            _lineRenderer.positionCount = 2;
        }

        private void UpdateRopePosition()
        {
            if (_isRopeActive)
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _target.transform.position);
                if (_joint.distance > 1f)
                    _joint.distance -= _pullingSpeed;
            }
        }

        public void SetStickySphere(StickySphere stickySphere)
        {
            if (_target != null)
            {
                _target.SwitchJointRope(false);
            }

            GameAudio.Instance.PlayRopeSound();
            _isRopeActive = true;
            _target = stickySphere;
            _target.SwitchJointRope(true);
            CreateRope();
        }

        public void DisableRope()
        {
            if (_target != null)
            {
                _target.SwitchJointRope(false);
            }

            _isRopeActive = false;

            _joint.enabled = false;
            _lineRenderer.enabled = false;
        }
    }
}