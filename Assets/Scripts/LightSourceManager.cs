using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A clas to keep track of the pool of light sources in the scene

public class LightSourceManager : MonoBehaviour
{
    int _lightSourceCount = 0;
    Dictionary<int, int> _dict = new Dictionary<int, int>();

    public int Count
    {
        get { return _lightSourceCount; }
    }

    public int RegisterLightSource(int objectID)
    {
        
        _dict[_lightSourceCount] = objectID;
        _lightSourceCount++;
        return _lightSourceCount - 1;
    }

    public bool RemoveLightSource(int key)
    {
        return _dict.Remove(key);
    }

    public int GetLightSourceObjectID(int key)
    {
        if (_dict.ContainsKey(key))
        {
            return _dict[key];
        }
        else
        {
            return -1;
        }
    }

    public int GetLightSourceIndex(int objectId)
    {

        return -1;
    }
}
