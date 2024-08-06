using UnityEngine;

namespace Assets.ProjectLesson2.Scripts.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterView : MonoBehaviour
    {
        private const string IsIdling = "IsIdling";
        private const string IsRunning = "IsRunning";
        private const string IsAirborn = "IsAirborn";
        private const string IsGrounded = "IsGrounded";
        private const string IsMovement = "IsMovement";
        private const string IsJumping = "IsJumping";
        private const string IsFalling = "IsFalling";

        private Animator _animator;

        public void Initialize() =>
            _animator = GetComponent<Animator>();

        public void StartIdle() => _animator.SetBool(IsIdling, true);
        public void StopIdle() => _animator.SetBool(IsIdling, false);

        public void StartRunning() => _animator.SetBool(IsRunning, true);
        public void StopRunning() => _animator.SetBool(IsRunning, false);

        public void StartAirborn() => _animator.SetBool(IsAirborn, true);
        public void StopAirborn() => _animator.SetBool(IsAirborn, false);

        public void StartGrounded() => _animator.SetBool(IsGrounded, true);
        public void StopGrounded() => _animator.SetBool(IsGrounded, false);

        public void StartMovement() => _animator.SetBool(IsMovement, true);
        public void StopMovement() => _animator.SetBool(IsMovement, false);

        public void StartJumping() => _animator.SetBool(IsJumping, true);
        public void StopJumping() => _animator.SetBool(IsJumping, false);

        public void StartFalling() => _animator.SetBool(IsFalling, true);
        public void StopFalling() => _animator.SetBool(IsFalling, false);
    }
}
