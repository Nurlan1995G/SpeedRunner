using UnityEngine;

public static class StaticClassLogic
{
    public static GameObject FindObject(string objectTag) =>
            GameObject.FindGameObjectWithTag(objectTag);
}
