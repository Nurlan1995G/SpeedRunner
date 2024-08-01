using Assets.Project.AssetProviders;
using System;
using UnityEngine;

namespace Assets.Project.CodeBase.SharkEnemy.Factory
{
    public class FactoryCharacter
    {
        private readonly AssetProvider _assetProviser;

        public FactoryCharacter(AssetProvider assetProvider)
        {
            _assetProviser = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
        }

        public BotSlimeView CreateSharkEnemy(string sharkEnemy, Vector3 position)
        {
            BotSlimeView shark = _assetProviser.Instantiate(sharkEnemy, position, 
                Quaternion.Euler(0,GetRandomRotation(),0));

            return shark;
        }

        private float GetRandomRotation()
        {
            float rotation = 0;

            rotation = UnityEngine.Random.Range(0, 360);

            return rotation;
        }
    }
}
