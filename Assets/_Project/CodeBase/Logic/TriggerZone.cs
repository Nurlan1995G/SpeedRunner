using UnityEngine;

public class TriggerZone : InteractableEnter
{
    [SerializeField] private PointSpawnZone _zone;

    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out BotController bot))
        {
            bot.SetZone(_zone);
        }
    }
}
