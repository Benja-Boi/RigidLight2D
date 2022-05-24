using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource2D : MonoBehaviour
{
    [SerializeField]
    private int _lightIndex;
    private LightSourceManager _lsm;


    public int Index
    {
        get { return _lightIndex; }
    }

    void Awake()
    {
        _lsm = FindObjectOfType<LightSourceManager>();
        _lightIndex = _lsm.RegisterLightSource(transform.gameObject.GetInstanceID());
    }

    void Update()
    {
        
    }
}
