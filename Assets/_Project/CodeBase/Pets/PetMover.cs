using UnityEngine;

public class PetMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _approachTime;
    [SerializeField] private float _maxDistance;

    private float _timer;

    private void Start()
    {
        transform.position = _target.position;
    }

    private void Update()
    {
        transform.forward = _target.forward;

        if (Vector3.Distance(transform.position, _target.position) >= _maxDistance)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _timer / _approachTime);

            _timer += Time.deltaTime;
        }
        else
            _timer = 0;
    }
}
