using UnityEngine;

public class RotateScoreToCamera : MonoBehaviour
{
    [SerializeField] private float _speedRotate;

    private Transform _transformCamera;

    private void Start()
    {
        _transformCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (_transformCamera == null)
            return;

        transform.LookAt(transform.position + _transformCamera.transform.rotation * Vector3.forward
            , _transformCamera.transform.rotation * Vector3.up);
    }
}
