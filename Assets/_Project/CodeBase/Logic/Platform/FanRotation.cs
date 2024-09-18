using UnityEngine;

public class FanRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.left * _rotationSpeed * Time.deltaTime);
    }
}