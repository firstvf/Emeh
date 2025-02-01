using Assets.Src.Code.Controllers;
using Assets.Src.Code.Game.Interactable;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Src.Code.Game
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private SpringJoint2D _joint;
        [SerializeField] private LineRenderer _lineRenderer;
        private float _pullingSpeed = 0.1f;
        public StickySphere _target;
        private bool _isRopeActive;
        private CancellationTokenSource _cancellationToken;

        private void Start()
        {
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.startColor = Color.green;
            _lineRenderer.endColor = Color.green;
        }

        private void Update()
        {
            UpdateRopePosition();
        }

        public void ChangeRopeSpeed()
        {
            _cancellationToken?.Cancel();

            _cancellationToken = new CancellationTokenSource();
            ChangeRopeSpeedTask(_cancellationToken).Forget();
        }

        private async UniTaskVoid ChangeRopeSpeedTask(CancellationTokenSource cancelToken)
        {
            _pullingSpeed = 1;
            await UniTask.Delay(15000);
            if (cancelToken.IsCancellationRequested == false)
            {
                _pullingSpeed = 0.1f;
                GameAudio.Instance.PlayEndBonusSound();
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

        private void CreateRope()
        {
            _joint.enabled = true;
            _lineRenderer.enabled = true;
            _joint.connectedBody = _target.Rigidbody2D;
            _joint.distance = Vector2.Distance(transform.position, _target.transform.position);
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
    }
}