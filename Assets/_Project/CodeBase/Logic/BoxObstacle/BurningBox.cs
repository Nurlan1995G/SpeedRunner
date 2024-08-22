using Assets._Project.CodeBase.Characters.Interface;
using UnityEngine;

public class BurningBox : Interactable
{
    protected override void Interact(Collider other)
    {
        if(other.TryGetComponent(out IRespawned respawned))
        {
            if (respawned is Player)
                SoundHandler.Instance.PlayLose();

            respawned.Respawn();
        }
    }

    protected override void InteractExit(Collider other)
    {
    }
}
