using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class to render the outline of a polygon colider

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class PolyColliderOutlineRenderer : MonoBehaviour
{
    PolygonCollider2D _polyCo2D;
    private LineRenderer _lr;
    private Vector2[] _points;

    void Awake()
    {
        _polyCo2D = gameObject.GetComponent<PolygonCollider2D>();
        _lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector2[] worldPoints = new Vector2[_polyCo2D.points.Length];
        for (int i = 0; i < worldPoints.Length; i++)
        {
            worldPoints[i] = transform.TransformPoint(_polyCo2D.points[i]);
        }
        SetUpLine(worldPoints);
        RenderLine();
    }

    void SetUpLine(Vector2[] points)
    {
        if (_lr != null)
        {
            _points = points;
            _lr.positionCount = points.Length;
        }
    }

    void RenderLine()
    {
        if (_points != null)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                _lr.SetPosition(i, _points[i]);
            }
        }
    }
}
