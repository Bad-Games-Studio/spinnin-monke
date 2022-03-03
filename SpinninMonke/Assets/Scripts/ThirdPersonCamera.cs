using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    
    public Vector2 offset;


    private float _pitch;
    private float _yaw;
    
    private void Start()
    {
        _pitch = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        _yaw = 0;
    }


    private void LateUpdate()
    {
        UpdateRotation();
        UpdatePosition();
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
        Debug.Log(transform.forward);
        
        return direction.normalized;
    }

    private void UpdateRotation()
    {
        _yaw += 0.1f;

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
}