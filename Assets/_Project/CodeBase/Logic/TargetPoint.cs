using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [field: SerializeField] public bool IsBusy { get; set; } = false;
    [field: SerializeField] public bool Diactivate { get; set; } = false;
}
