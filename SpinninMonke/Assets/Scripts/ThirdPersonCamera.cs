using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    
    public Vector2 offset;

    public float mouseSensitivity;


    private float _pitch;
    private float _yaw;

    private bool _canRotate;
    
    public Vector3 GetForward()
    {
        var forward = transform.forward;
        return new Vector3(forward.x, 0, forward.z).normalized;
    }

    public Vector3 GetRight()
    {
        var right = transform.right;
        return new Vector3(right.x, 0, right.z).normalized;
    }
    
    private void Start()
    {
        _pitch = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        _yaw = 0;
        _canRotate = false;
    }


    private void LateUpdate()
    {
        HandleFire2Button();
        UpdateRotation();
        UpdatePosition();
    }

    private void HandleFire2Button()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            EnableRotation();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            DisableRotation();
        }
    }
    
    private void EnableRotation()
    {
        _canRotate = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void DisableRotation()
    {
        _canRotate = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    private void UpdateRotation()
    {
        if (_canRotate)
        {
            var rotationAngle = Input.GetAxis("Mouse X") * mouseSensitivity;
            _yaw += rotationAngle;
        }

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
    
    private void UpdatePosition()
    {
        var direction = GetHorizontalDirection();

        var offset3d = -offset.x * direction;
        offset3d.y = offset.y;
        
        transform.position = target.transform.position + offset3d;
    }

    private Vector3 GetHorizontalDirection()
    {
        var angle = _yaw * Mathf.Deg2Rad;
        var direction = new Vector3(
            Mathf.Sin(angle),
            0,
            Mathf.Cos(angle));

        return direction.normalized;
    }
}