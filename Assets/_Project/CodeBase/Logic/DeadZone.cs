using Assets._Project.CodeBase.Characters.Interface;
using DG.Tweening;
using UnityEngine;

public class DeadZone : InteractableEnter
{
    [SerializeField] private TypeBattersPlatform _typeBatters;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isTwistLeft;

    private float _rotationYLenght = 360;

    public void Start()
    {
        SelectType();
    }

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out IRespawned respawned))
        {
            if (respawned is Player)
                SoundHandler.Instance.PlayLose();

            respawned.Respawn();
        }
    }

    private void SelectType()
    {
        if (_typeBatters == TypeBattersPlatform.MovableBattersPlatform)
            transform.DOMove(_target.position, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        else if (_typeBatters == TypeBattersPlatform.RotateBattersPlatform)
        {
            if (_isTwistLeft)
                _rotationYLenght = -_rotationYLenght;

            transform.DORotate(new Vector3(0, _rotationYLenght, 0), _duration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
        else
            _target = transform;
    }
}

public enum TypeBattersPlatform
{
    RotateBattersPlatform,
    MovableBattersPlatform,
    StandingBattersPlatform
}
