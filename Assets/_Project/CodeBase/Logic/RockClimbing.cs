using UnityEngine;

public class RockClimbing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            if (other.TryGetComponent(out CharacterController characterController))
                EnableClimbing(characterController);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            if (other.TryGetComponent(out CharacterController characterController))
                DisableClimbing(characterController);
        }
    }

    private void EnableClimbing(CharacterController characterController)
    {
        if (characterController.TryGetComponent(out BotMovement botMovement))
        {
            botMovement.SetClimbing(true);
        }
        else if (characterController.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.SetClimbing(true);
        }
    }

    private void DisableClimbing(CharacterController characterController)
    {
        if (characterController.TryGetComponent(out BotMovement botMovement))
        {
            botMovement.SetClimbing(false);
        }
        else if (characterController.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.SetClimbing(false);
        }
    }
}
