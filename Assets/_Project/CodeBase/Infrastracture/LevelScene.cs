using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.CodeBase.Infrastracture
{
    public class LevelScene : MonoBehaviour
    {
        private bool _isBusy;

        [field: SerializeField] public List<PointSpawnZone> PointSpawnZones { get; private set; }
        [field: SerializeField] public List<TriggerZone> TriggerZones { get; private set; }

        public void SetBusy(bool busy) =>
            _isBusy = busy;
    }
}
