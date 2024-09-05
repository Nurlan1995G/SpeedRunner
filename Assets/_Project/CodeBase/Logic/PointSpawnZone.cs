using System.Collections.Generic;
using UnityEngine;

public class PointSpawnZone : MonoBehaviour
{
    [Header("Настройки зоны спавна")]
    [SerializeField] private TargetPoint _pointPrefab; 
    [SerializeField] private int _pointsCount = 10; 
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10, 0, 10);

    private List<TargetPoint> _points = new List<TargetPoint>();

    public List<TargetPoint> TargetPoints => _points;

    private void Start()
    {
        SpawnPoints();
    }

    private void SpawnPoints()
    {
        for (int i = 0; i < _pointsCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2),
                0, 
                Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2)
            );

            TargetPoint targetPoint = Instantiate(_pointPrefab, transform.position + spawnPosition, Quaternion.identity, transform);

            _points.Add(targetPoint);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f); 
        Gizmos.DrawCube(transform.position, _spawnAreaSize);
    }
}
