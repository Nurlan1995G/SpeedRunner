using UnityEngine;

public class RockClimbing : Interactable
{
    public override void InteractEnter(Collider other)
    {
        /*if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            Debug.Log("Соприкосновение произошло");
            Vector3 normal = (other.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(-normal);
            playerMover.StartClimbing(targetRotation);
        }*/
    }

    public override void InteractExit(Collider other)
    {
        /*if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            Debug.Log("Выход от стены");
            playerMover.StopClimbing();
        }*/
    }
}
