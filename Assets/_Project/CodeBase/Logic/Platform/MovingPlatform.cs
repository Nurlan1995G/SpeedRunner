using DG.Tweening;
using UnityEngine;

public class MovingPlatform : Interactable
{
    [SerializeField] private TypeMovingPlatform _type;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isTwistLeft;

    private float _rotationYLenght = 360;

    private Vector3 _startPosition;
    private CharacterController _characterController;

    private void Start()
    {
        _startPosition = transform.position;

        transform.DOMove(_target.position, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        
        if (_type == TypeMovingPlatform.Rotate)
        {
            if (_isTwistLeft)
                _rotationYLenght = -_rotationYLenght;

            transform.DORotate(new Vector3(0, _rotationYLenght, 0), _duration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }

    private void Update()
    {
        Vector3 platformMovement = _startPosition - transform.position;
        transform.position = _startPosition;

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
    LeftRight,
    Rotate
}
