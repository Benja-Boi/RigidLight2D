using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simple class to implement a Drag and Drop for a 2d object
public class DragAndDrop : MonoBehaviour
{
    private Vector2 _dragOffset;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    void OnMouseDown()
    {
        _dragOffset = new Vector2(transform.position.x, transform.position.y) - GetMousePos();
    }
    
    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + _dragOffset;
    }

    Vector2 GetMousePos()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
