using UnityEngine;

public class FanRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private RotateType _rotateType;

    private void Update() => 
        RotateFan();

    private void RotateFan()
    {
        switch (_rotateType)
        {
            case RotateType.Horizontal:
                transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
                break;
            case RotateType.Vertical:
                transform.Rotate(Vector3.left * _rotationSpeed * Time.deltaTime);
                break;
        }
    }
}

public enum RotateType
{
    Vertical,
    Horizontal
}