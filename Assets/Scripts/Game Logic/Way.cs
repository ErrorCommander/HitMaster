using UnityEngine;
using System.Collections.Generic;

public abstract class Way : MonoBehaviour
{
    public CheckPoint CurrentCheckPoint { get; protected set; }

    [SerializeField] private bool _drawWayLine;
    [SerializeField] protected List<CheckPoint> _checkPoints;

    public abstract CheckPoint GetNextCheckPoint();
    protected abstract void DrawWayLine();

    public CheckPoint GetStartCheckPoint()
    {
        if(_checkPoints != null || _checkPoints.Count > 0)
            return _checkPoints[0];

        GameplayEventSystem.SendGameOver();
        return null;
    }

    private void OnDrawGizmos()
    {
        if (_drawWayLine)
        {
            DrawWayLine();
        }
    }
}
