using Event.Mouse;
using UnityEngine;


[RequireComponent(typeof(MouseUpAndDownEvent),typeof(MouseLeftAndRightEvent))]
public class RotateObject : MonoBehaviour
{
    [SerializeField] private int intensity = 2;
    [SerializeField] private bool _isRotate;
    private Transform _transform;
    private void OnEnable()
    {
        GlobalEvents.MouseUpAndDownEvent.onMouse +=onMouseUpAndDownEvent;
        GlobalEvents.MouseUpAndDownEvent.Value =intensity;
        GlobalEvents.MouseLeftAndRightEvent.onMouse +=onMouseLeftAndRightEvent ;
        GlobalEvents.MouseLeftAndRightEvent.Value =intensity ;
    }
    private void onMouseUpAndDownEvent(float value)
    {
        RotateCube(value,0,0);
    }
    private void onMouseLeftAndRightEvent(float value)
    {
        RotateCube(0,value,0);
    }
    private void RotateCube(float x, float y, float z)
    {
        if (!_isRotate) return;
        _transform.Rotate(-x, -y, -z);
    }
    public void StartRotation()
    {
        _isRotate = true;
    }
    public void EndRotation()
    {
        _isRotate = false;
    }
    private void OnDestroy()
    {
        GlobalEvents.MouseLeftAndRightEvent.onMouse -=onMouseUpAndDownEvent;
        GlobalEvents.MouseLeftAndRightEvent.onMouse -=onMouseLeftAndRightEvent ;
    }
}