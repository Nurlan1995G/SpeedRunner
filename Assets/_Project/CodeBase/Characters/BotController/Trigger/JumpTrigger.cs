using System;
using System.Collections;
using UnityEngine;

public class JumpTrigger : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out BotController botController))
        {
            StartCoroutine(DelayJumpBot(botController));
        }
    }

    private IEnumerator DelayJumpBot(BotController botController)
    {
        yield return new WaitForSeconds(0.2f);
        botController.JumpTrigger();
    }
}
