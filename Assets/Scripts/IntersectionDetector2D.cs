using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A helper class for the rigidlight2D in charge of reporting intersection of the lightsource with other lightsources

[RequireComponent(typeof(Rigidlight2D))]
public class IntersectionDetector2D : MonoBehaviour
{

    [SerializeField]
    [Range(1, 12)]
    int _sampleRate = 1;    // The rate of samples out of all rays sent by the lightsources (one every _sampleRate)

    private Rigidlight2D _rl2d;
    private PolygonCollider2D _polyCo;
    private IntersectionManager2D _im;
    private LightSourceManager _lsm;
    private int _lightSourceIndex;

    void Awake()
    {
        _rl2d = transform.parent.GetComponentInChildren<Rigidlight2D>();
        _polyCo = transform.parent.GetComponentInChildren<PolygonCollider2D>();
        _im = FindObjectOfType<IntersectionManager2D>();
        _lsm = FindObjectOfType<LightSourceManager>();
        _lightSourceIndex = GetComponentInParent<LightSource2D>().Index;
    }

    void Update()
    {
        findIntersectinPoints();
    }

    private void findIntersectinPoints()
    {
        Collider2D[] colliders = new Collider2D[_rl2d.HitPoints.Length];
        for (int i = 0; i < _rl2d.HitPoints.Length; i++)
        {
            if (i % _sampleRate == 0)
            {
                bool[] isIntersecting = new bool[_lsm.Count];
                int numCollides = Physics2D.OverlapCircleNonAlloc(transform.TransformPoint(_rl2d.HitPoints[i]), 0f, colliders);
                for (int c = 0; c < numCollides; c++)
                {
                    if (colliders[c] != _polyCo && colliders[c] != null)
                    {
                        LightSource2D ls2d = colliders[c].GetComponentInParent<LightSource2D>();
                        if (ls2d != null)
                        {
                            int target = ls2d.Index;
                            isIntersecting[target] = true;
                            Debug.DrawLine(transform.position, transform.TransformPoint(_rl2d.HitPoints[i]));
                        }
                    }
                }
                _im.ReportIntersection(i, new IntersectionPoint(_lightSourceIndex, isIntersecting, transform.TransformPoint(_rl2d.HitPoints[i])));
            }
        }
    }
}
