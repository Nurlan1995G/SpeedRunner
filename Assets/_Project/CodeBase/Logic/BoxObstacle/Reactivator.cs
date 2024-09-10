using System.Collections;
using UnityEngine;

public class Reactivator : MonoBehaviour
{
    public void ReactivateObject(DisappearingBox disappearingBox, float delay)
    {
        StartCoroutine(ReactivateAfterDelay(disappearingBox, delay));
    }

    private IEnumerator ReactivateAfterDelay(DisappearingBox disappearingBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        disappearingBox.gameObject.SetActive(true);
        disappearingBox.StartCoroutine(disappearingBox.Reactivator());
    }
}
