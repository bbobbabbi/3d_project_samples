using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float distance = 5f;

    private float _azimuthAngle ;
    private float _polarAngle;

    private void Start()
    {
        var cartesianPosition = GetCameraPosition(distance, _polarAngle, _azimuthAngle);
        transform.position = target.position - cartesianPosition;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Debug.Log($"## X: {mouseX} Y: {mouseY}");

        _polarAngle += -mouseY * rotationSpeed * Time.deltaTime;
        _azimuthAngle -= -mouseX * rotationSpeed * Time.deltaTime;

        _polarAngle = Mathf.Clamp(_polarAngle, -45, 45);

        var cartesianPosition = GetCameraPosition(distance, _polarAngle, _azimuthAngle);
        transform.position = target.position - cartesianPosition;
        transform.LookAt(target);
    }

    Vector3 GetCameraPosition(float r, float polarAngle, float azimuthAngle) { 
        float b = r * Mathf.Cos(polarAngle* Mathf.Deg2Rad);
        float z = b * Mathf.Cos(azimuthAngle * Mathf.Deg2Rad);
        float y = r * Mathf.Sin(polarAngle * Mathf.Deg2Rad) * -1;
        float x = b * Mathf.Sin(azimuthAngle* Mathf.Deg2Rad);
        
        return new Vector3(x, y, z);
    }
}
