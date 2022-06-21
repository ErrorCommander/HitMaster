using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearWay : Way
{
    private int _indexer = 0;

    public override CheckPoint GetNextCheckPoint()
    {
        for (int i = _indexer; i < _checkPoints.Count; i++)
        {
            if (!_checkPoints[i].IsPassed)
            {
                _indexer++;
                return _checkPoints[i];
            }
        }
        
        return null;
    }

    private IEnumerable<CheckPoint> NextCheckPoint()
    {
        foreach (var checkPoint in _checkPoints)
        {
            if (!checkPoint.IsPassed)
            {
                yield return checkPoint;
            }
        }

        yield return null;
    }

    protected override void DrawWayLine()
    {
        for (int i = 1; i < _checkPoints.Count; i++)
        {
            Gizmos.DrawLine(_checkPoints[i - 1].transform.position, _checkPoints[i].transform.position);
        }
    }
}