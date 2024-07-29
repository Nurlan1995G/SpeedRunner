using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Range(0, 500)] private float _mouseRotationSpeed;

    private float _currentRotation = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            _currentRotation -= _mouseRotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
    }

    public void Reset()
    {
        _currentRotation = 0;
        transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
    }
}