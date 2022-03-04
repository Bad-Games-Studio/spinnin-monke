using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public float maxSpeed;

        [Range(0.0f, 0.99f)]
        public float velocitySmoothness;

        public float rotationSpeed;

        private const float MaxVelocitySmoothness = 1.0f;

        private ThirdPersonCamera _camera;
        private Rigidbody _rigidbody;

        private Vector3 _movementDirection;
        private bool ShouldMove => _movementDirection != Vector3.zero;

        private float VelocitySmoothness => MaxVelocitySmoothness - velocitySmoothness;
        private float RotationStep => Time.deltaTime * rotationSpeed;

        private void Start()
        {
            _camera = FindCamera();
            _rigidbody = GetComponent<Rigidbody>();

            _movementDirection = Vector3.zero;
        }
        
        private void Update()
        {
            _movementDirection = GetMovementDirection();
            SmoothMove();
            if (ShouldMove)
            {
                SmoothRotate();
            }
        }

        private Vector3 GetMovementDirection()
        {
            var forward = Input.GetAxis("Vertical")   * _camera.GetForward();
            var right   = Input.GetAxis("Horizontal") * _camera.GetRight();
            
            var movementDirection = forward + right;
            return movementDirection.magnitude > 1 ?
                movementDirection.normalized :
                movementDirection;
        }

        private Vector3 MovementVelocity => maxSpeed * _movementDirection;
        
        private void SmoothMove()
        {
            var targetVelocity = MovementVelocity;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, targetVelocity, VelocitySmoothness);
        }

        private void SmoothRotate()
        {
            var newDirection = Vector3.RotateTowards(
                transform.forward, _movementDirection, 
                RotationStep, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        
        private static ThirdPersonCamera FindCamera()
        {
            var cameras = FindObjectsOfType<ThirdPersonCamera>();
            if (cameras.Length == 1)
            {
                return cameras[0];
            }
            Debug.Log($"There should be only 1 camera (found {cameras.Length}).");
            return null;
        }
    }
}
