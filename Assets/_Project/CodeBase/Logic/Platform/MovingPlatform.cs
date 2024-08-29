using DG.Tweening;
using UnityEngine;

public class MovingPlatform : Interactable
{
    [SerializeField] private TypeMovingPlatform _type; 
    [SerializeField] private float _speed = 2f; 
    [SerializeField] private float _distance = 3f; 

    private Vector3 _startPosition;
    private Vector3 _direction;
    private CharacterController _characterController;

    private void Start()
    {
        _startPosition = transform.position;

        if (_type == TypeMovingPlatform.UpDown)
            _direction = Vector3.up; 
        else if (_type == TypeMovingPlatform.LeftRight)
            _direction = Vector3.forward; 
    }

    private void Update()
    {
        float movementFactor = Mathf.Sin(Time.time * _speed) * _distance;
        Vector3 newPosition = _startPosition + _direction * movementFactor;
        Vector3 platformMovement = newPosition - transform.position;

        transform.position = newPosition;

        if (_characterController != null)
        {
            _characterController.Move(platformMovement);
        }
    }

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _characterController = player.CharacterController;
        }
    }

    public override void InteractExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _characterController = null;
        }
    }
}

public enum TypeMovingPlatform
{
    UpDown,
    LeftRight
}
