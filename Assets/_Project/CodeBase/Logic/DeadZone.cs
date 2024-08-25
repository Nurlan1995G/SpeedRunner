using Assets._Project.CodeBase.Characters.Interface;
using UnityEngine;

public class DeadZone : Interactable
{
    protected override void Interact(Collider other)
    {
        if(other.TryGetComponent(out IRespawned respawn))
        {
            if (respawn is Player)
                SoundHandler.Instance.PlayLose();

            respawn.Respawn();
        }
    }

    protected override void InteractExit(Collider other)
    {
    }
}
