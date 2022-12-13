using UnityEngine;


public class Draggable : MonoBehaviour
{
    private Vector3 _startPosition;
    private Transform _transform;
    private bool _isDraggable;
    private void Start()
    {
        _startPosition = _transform.position;
        _isDraggable = false;
    }
    private void OnMouseDown()
    {
        _isDraggable = true;
    }
    private void OnMouseDrag()
    {
        Vector3 position = new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.WorldToScreenPoint(_transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        _transform.position = new Vector3(worldPosition.x,worldPosition.y,worldPosition.z);
    }
    private void OnMouseUp()
    {
        if (_isDraggable) return;
        _transform.position = _startPosition;
    }
    public void EndMoving()
    {
        _isDraggable = false;
    }
}