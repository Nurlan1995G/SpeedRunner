using Assets._Project.CodeBase.Characters.Interface;
using UnityEngine;

public class BurningBox : InteractableEnter
{
    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out IRespawned respawned))
        {
            if (respawned is Player)
                SoundHandler.Instance.PlayLose();

            respawned.Respawn();
        }
    }
}
