using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private TypeMovingPlatform _type;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;

        if (_type == TypeMovingPlatform.UpDown)
            transform.DOMove(_target.position, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        else if(_type == TypeMovingPlatform.LeftRight)
        {
            Vector3 endPos = new Vector3(_target.position.x, _startPosition.y, _startPosition.z);
            transform.DOMove(endPos, _duration).SetLoops(1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}

public enum TypeMovingPlatform
{
    LeftRight,
    UpDown
}
