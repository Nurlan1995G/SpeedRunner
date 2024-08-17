using System.Collections;
using UnityEngine;

public class BoostBoxUp : MonoBehaviour
{
    public void SetPowerJump(Player player, PlayerMover playerMover)
    {
        StartCoroutine(WaitForJumpInput(player, playerMover));
    }

    private IEnumerator WaitForJumpInput(Player player, PlayerMover playerMover)
    {
        float elapsedTime = 0f;

        while (elapsedTime < player.CharacterData.BoostWaitTime)
        {
            if (player.PlayerInputs.JumpTriggered)
            {
                playerMover.TakeJumpDirection(player.CharacterData.BoostHeightUp * player.CharacterData.JumpStep);
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerMover.TakeJumpDirection(player.CharacterData.BoostHeightUp);
    }
}
