using UnityEngine;

public class TriggerZone : InteractableEnter
{
    private PointSpawnZone _zone;

    public override void InteractEnter(Collider other)
    {
        if(other.TryGetComponent(out BotController bot))
            bot.SetZone(_zone);
    }

    public void SetNextZone(PointSpawnZone pointSpawnZone) =>
        _zone = pointSpawnZone;
}
