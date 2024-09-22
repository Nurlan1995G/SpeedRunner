﻿using Assets._Project.CodeBase.Characters.Interface;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager
{
    private List<IRespawned> _finishers = new List<IRespawned>();
    private PositionStaticData _positionStaticData;

    public RaceManager(PositionStaticData positionStaticData)
    {
        _positionStaticData = positionStaticData;
    }

    public void RegisterFinish(IRespawned finisher)
    {
        if (_finishers.Contains(finisher))
            return;
        Debug.Log(_finishers.Count + " - finishes count");
        _finishers.Add(finisher);

        if (_finishers.Count <= 3)
        {
            Debug.Log("f_finishers.Count <= 3");
            int positionIndex = _finishers.Count - 1;
            AssignFinisherPosition(finisher, positionIndex);
        }
    }

    private void AssignFinisherPosition(IRespawned finisher, int positionIndex)
    {
        Vector3 finishPosition = Vector3.zero;
        Quaternion finishRotation = Quaternion.identity;

        switch (positionIndex)
        {
            case 0: 
                finishPosition = _positionStaticData.OnePosition;
                break;
            case 1: 
                finishPosition = _positionStaticData.TwoPosition;
                break;
            case 2: 
                finishPosition = _positionStaticData.ThreePosition;
                break;
            default:
                return;
        }

        TeleportToPosition(finisher, finishPosition, finishRotation);
    }

    private void TeleportToPosition(IRespawned finisher, Vector3 position, Quaternion rotation)
    {
        if (finisher is Player player)
        {
            Debug.Log("finisher is Player player");
            player.transform.position = position;
            player.transform.rotation = rotation;
        }
        else if (finisher is BotController bot)
        {
            Debug.Log("finisher is BotController bot");
            bot.transform.position = position;
            bot.transform.rotation = rotation;
        }
    }
}
