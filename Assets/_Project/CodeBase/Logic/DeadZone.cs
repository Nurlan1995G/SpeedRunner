using Assets._Project.CodeBase.Characters.Interface;
using UnityEngine;

public class DeadZone : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out IRespawned respawn))
        {
            if (respawn is Player)
                SoundHandler.Instance.PlayLose();

            respawn.Respawn();
        }
    }
}
