using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that holds information on the intersections of some point on a source with all other colliders

public class IntersectionPoint
{
    private int _intersectionSource;
    private bool[] _intersectionTargets;
    private Vector2 _position;

    public int Source
    {
        get { return _intersectionSource; }
    }

    public bool[] Targets
    {
        get { return _intersectionTargets; }
    }

    public Vector2 Position
    {
        get { return _position; }
    }

    public IntersectionPoint(int source, bool[] targets, Vector2 position)
    {
        this._intersectionSource = source;
        this._intersectionTargets = targets;
        this._position = position;

    }
}