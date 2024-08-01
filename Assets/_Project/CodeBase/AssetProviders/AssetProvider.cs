using UnityEngine;

namespace Assets.Project.AssetProviders
{
    public class AssetProvider
    {
        /*public Food Instantiate(Food fish, Vector3 whereToSpawn, Quaternion identity) =>
            Object.Instantiate(fish, whereToSpawn, identity);*/

        public BotSlimeView Instantiate(string path, Vector3 position, Quaternion rotation) =>
            Object.Instantiate(Resources.Load<BotSlimeView>(path), position, rotation);

        public PlayerView Instantiate<T>(string path, Vector3 position) where T : class =>
            Object.Instantiate(Resources.Load<PlayerView>(path), position, Quaternion.identity);

        public GameObject Instantiate(GameObject gameObject, Transform hatPosition) =>
            Object.Instantiate(gameObject, hatPosition);
    }
}