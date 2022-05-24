using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class to keep track and manage intersection between the colliders of rigid light sources 

public class IntersectionManager2D : MonoBehaviour
{
    [SerializeField]
    private bool[,] _isIntersecting;
    private Dictionary<int, Vector2>[,] _intersections;
    private LightSourceManager _lsm;
    private int numLights;

    void Awake()
    {
        _lsm = FindObjectOfType<LightSourceManager>();
        _isIntersecting = new bool[_lsm.Count, _lsm.Count];
        numLights = _lsm.Count;
        InitDicts();
    }

    public void Update()
    {
        RenderIntersections();
    }

    private void InitDicts()
    {
        _intersections = new Dictionary<int, Vector2>[_lsm.Count, _lsm.Count];
        for (int i = 0; i < numLights; i++)
        {
            for (int j = 0; j < numLights; j++)
            {
                _intersections[i, j] = new Dictionary<int, Vector2>();
            }
        }
    }

    // For every lightSource_i and lightSource_j we hold a dictionary of intersections where the key is the number
    // of the ray that created the intersection and the value is an IntersectionPoint
    public void ReportIntersection(int rayNum, IntersectionPoint pt)
    {
        for (int i = 0; i < pt.Targets.Length; i++)
        {
            if (pt.Targets[i])
            {
                Debug.Log("Intersection reported: " + pt.Source + ", " + i + ", Raynum = " + rayNum);
                _isIntersecting[pt.Source, i] = true;
                _intersections[pt.Source, i][rayNum] = pt.Position;
            }
            else
            {
                _isIntersecting[pt.Source, i] = false;
                _intersections[pt.Source, i].Remove(rayNum);
            }
        }
    }

    // We render every point that intersects 2 or more rigidlights
    public void RenderIntersections()
    {
        for (int i = 0; i < numLights; i++)
        {
            for (int j = 0; j < numLights; j++)
            {
                if (_isIntersecting[i, j] || _isIntersecting[i, j])
                {
                    foreach (KeyValuePair<int, Vector2> entry in _intersections[i, j])
                    {
                        MarkIntersection(entry.Value);
                    }
                    foreach (KeyValuePair<int, Vector2> entry in _intersections[j, i])
                    {
                        MarkIntersection(entry.Value);
                    }

                }
            }
        }
    }

    public void MarkIntersection(Vector2 position)
    {
        Debug.DrawRay(position, 1 * Vector2.down, Color.blue);
    }
}
