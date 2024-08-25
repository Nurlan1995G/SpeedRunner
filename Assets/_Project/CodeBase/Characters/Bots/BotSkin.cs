using UnityEngine;

public class BotSkin : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; private set; }

    public void Enable() =>
    gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}

