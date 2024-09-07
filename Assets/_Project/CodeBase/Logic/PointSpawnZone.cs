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

    private void Awake()
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
        Gizmos.color = new Color(200, 3, 230, 30f); 
        Gizmos.DrawCube(transform.position, _spawnAreaSize);
    }
}
