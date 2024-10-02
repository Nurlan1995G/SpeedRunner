using UnityEngine;

public class PetMover : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _aproachTime;
    [SerializeField] private float _maxDistance;

    private float _timer;

    private void Start() =>
        transform.position = _targetPoint.position;

    private void Update()
    {
        transform.forward = _targetPoint.forward;

        if (Vector3.Distance(transform.position, _targetPoint.position) >= _maxDistance)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPoint.position, _timer / _aproachTime);
            _timer += Time.deltaTime;
        }
        else
        
            _timer = 0;
    }
}
