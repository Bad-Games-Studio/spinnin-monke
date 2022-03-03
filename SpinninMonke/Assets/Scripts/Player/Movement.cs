using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public float acceleration;
        public float maxSpeed;

        private ThirdPersonCamera _camera;
        private Rigidbody _rigidbody;
        
        private Vector2 _movementDirection;

        private void Start()
        {
            _camera = FindCamera();
            _rigidbody = GetComponent<Rigidbody>();

            _movementDirection = Vector2.zero;
        }
        
        private void Update()
        {
            HandleMovementKeys();
        }

        private void HandleMovementKeys()
        {
            UpdateMovementDirection();

            var velocityLeftRight       = _movementDirection.x * _camera.GetRight();
            var velocityForwardBackward = _movementDirection.y * _camera.GetForward();
            var netVelocityDirection = (velocityLeftRight + velocityForwardBackward).normalized;
            _rigidbody.velocity = maxSpeed * netVelocityDirection;
        }

        private void UpdateMovementDirection()
        {
            // Cringe but Input.GetAxis() works poorly due to
            // Smoothing for controllers (even though I have no controllers ;;).
            // Tried but was unable to make it as responsive as the following "masterpiece".
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _movementDirection.x -= 1;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _movementDirection.x += 1;
            }
            
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _movementDirection.x += 1;
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _movementDirection.x -= 1;
            }
            
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _movementDirection.y -= 1;
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                _movementDirection.y += 1;
            }
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _movementDirection.y += 1;
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                _movementDirection.y -= 1;
            }
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
