using Event.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;


public class Item : MonoBehaviour
{
    private Rotateble _rotateObject;
    private void Awake()
    {
        _rotateObject = gameObject.GetComponent<Rotateble>();
    }
    private void OnMouseDown()
    {
        _rotateObject.StartRotation();
    }
    private void OnMouseUp()
    {
        _rotateObject.EndRotation();
    }
}