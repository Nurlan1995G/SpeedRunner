using UnityEngine;

public class GroundSphereChecker : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private LayerMask groundLayer; 

    private void OnTriggerEnter(Collider other)
    {
        if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("Touched the ground!");
        }
    }


}
