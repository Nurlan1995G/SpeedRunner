using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private List<LayerMask> _layerMasks;

    [SerializeField, Range(0.01f, 1f)] private float _distanceToCheck;

    public bool IsGrounded { get; private set; }
    public bool IsOnTrampoline { get; private set; }
    public bool IsOnBoostUp { get; private set; }
    public bool IsOnJumpBot { get; private set; }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(transform.position, _distanceToCheck, _layerMasks[0]);
        IsOnTrampoline = Physics.CheckSphere(transform.position, _distanceToCheck, _layerMasks[1]);
        IsOnBoostUp = Physics.CheckSphere(transform.position, _distanceToCheck, _layerMasks[2]);
        IsOnJumpBot = Physics.CheckSphere(transform.position, _distanceToCheck, _layerMasks[3]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;  
        Gizmos.DrawWireSphere(transform.position, _distanceToCheck);

        Gizmos.color = new Color(0, 1, 0, 0.2f); 
        Gizmos.DrawSphere(transform.position, _distanceToCheck);
    }
}
