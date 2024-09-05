using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFinder
{
    private List<TargetPoint> _points = new List<TargetPoint>();

    public TargetFinder()
    {
    }

    public TargetPoint FindNearestFreePoint(Vector3 position)
    {
        Debug.Log("FindNearestFreePoint - TargetFinder");

        TargetPoint nearestPoint = _points
            .Where(p => !p.IsBusy) 
            .OrderBy(p => Vector3.Distance(position, p.transform.position)) 
            .FirstOrDefault();

        Debug.Log(nearestPoint + " - nearestPoint");

        if (nearestPoint != null)
        {
            nearestPoint.IsBusy = true; 
        }

        return nearestPoint;
    }

    public void Add(TargetPoint targetPoint)
    {
        Debug.Log("Add Point - " + _points.Count);

        _points.Add(targetPoint);
    }
}
