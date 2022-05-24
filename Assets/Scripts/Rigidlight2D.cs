using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class serves as a 2D circle collider that dynamically
// changes shape as a response to other 2D colliders, created by raycasting and a 2D polygon collider


[RequireComponent(typeof(PolygonCollider2D))]
public class Rigidlight2D : MonoBehaviour
{
    [SerializeField]
    [Range(3,720)]
    int _rayCount;  // Count of rays sent by the rigidlight to define its boundries
    [SerializeField]
    [Range(0, 360)]
    float _angle;   // The angle of the resulting collider (360 == full circle, 0 nothing)
    [SerializeField]
    [Range(0, 10)]
    float _size;    // The radius of the resulting collider
    [SerializeField]
    [Range(-Mathf.PI, Mathf.PI)]
    float _direction;   // The dicrection of the collider given by radians 
    [SerializeField]
    LayerMask _ignoreLayer; // Layers to ignore when sending raycasts

    private PolygonCollider2D polyCo;
    private Vector2[] _hitPts;

    public Vector2[] HitPoints
    {
        get { return _hitPts; }
    }
    public LayerMask IgnoreLayer
    {
        get { return _ignoreLayer; }
    }

    private void Awake()
    {
        polyCo = GetComponent<PolygonCollider2D>();
        Ray2D[] rays = ConstructRays();
        _hitPts = FindHitPoints(rays);
    }

    void Update()
    {
        Ray2D[] rays = ConstructRays(); // Construct rays
        _hitPts = FindHitPoints(rays);  // Find intersections
        polyCo.SetPath(0, _hitPts);     // Create path from intersection
    }

    // Construct _rayCount evenly spread rays around the center
    private Ray2D[] ConstructRays()
    {
        float theta = _angle * Mathf.Deg2Rad;       // 0 < theta <= 2*Pi
        float firstRayAngle = ModAngle(_direction + (theta / 2));  // -Pi < firstRayAngle < Pi
        float thetaIncrement = theta / (_rayCount - 1);
        Ray2D[] o_rays = new Ray2D[_rayCount];
        for (int i = 0; i < _rayCount; i++)
        {
            float rayAngle = firstRayAngle - (i * thetaIncrement);
            Vector2 rayDir = Vector3.Normalize(new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle)));
            o_rays[i] = new Ray2D(transform.position, rayDir);
        }

        return o_rays;
    }

    // Find the intersection of the rays
    private Vector2[] FindHitPoints(Ray2D[] rays)
    {
        Vector2[] o_hitPts = new Vector2[_rayCount + 1];
        for (int i = 0; i < _rayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rays[i].origin, rays[i].direction, _size, ~_ignoreLayer);
            if (hit.collider == null)
            {
                o_hitPts[i] = new Vector2(transform.position.x, transform.position.y) + _size * rays[i].direction;
                o_hitPts[i] = transform.InverseTransformPoint(o_hitPts[i]);
                if (hit.normal == -rays[i].direction)
                {
                    o_hitPts[i] = transform.position;
                }
            }
            else
            {
                Debug.Log("Ray hit");
                if (true)
                {
                    //if (hit.collider != null)
                    //{
                    //    Debug.Log("No hit");
                    //}
                    //if (hit.collider == polyCo)
                    //{
                    //    Debug.Log("Hit self");
                    //}
                    //if (hit.collider == lightSourceCo)
                    //{
                    //    Debug.Log("Hit parent");
                    //}
                    //if (hit.normal == -rays[i].direction)
                    //{
                    //    Debug.Log("Reverse direction");
                    //}
                }
                o_hitPts[i] = transform.InverseTransformPoint(hit.point);
            }
            
            //Debug.DrawLine(transform.position, transform.TransformPoint(hitPts[i]));
        }

        if (_angle != 360)  // Full circle should not include center as a vertex.
        {
            o_hitPts[_rayCount] = transform.position;
        }
        else
        {
            o_hitPts[_rayCount] = o_hitPts[0];
        }

        return o_hitPts;
    }

    // Find return the equivalent angle in the range -PI <= x <= PI
    float ModAngle(float x)
    {
        if (x > Mathf.PI)
        {
            return x - 2 * Mathf.PI;
        }
        else
        {
            if (x <= -Mathf.PI)
            {
                return x + 2 * Mathf.PI;
            }
            return x;
        }
    }
}
