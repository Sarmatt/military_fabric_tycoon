using Event.Mouse;
using UnityEngine;
public class Rotateble : MonoBehaviour
{
    [SerializeField] private int intensity = 2;
    [SerializeField] private bool _isRotate;
    private void OnEnable()
    {
        TouchUpAndDownEvent.onMouse +=onMouseUpAndDownEvent;
        TouchLeftAndRightEvent.onMouse +=onMouseLeftAndRightEvent ;
    }
    private void OnDisable()
    {
        TouchUpAndDownEvent.onMouse -=onMouseUpAndDownEvent;
        TouchLeftAndRightEvent.onMouse -=onMouseLeftAndRightEvent ;
    }
    private void onMouseUpAndDownEvent(int value)
    {
        RotateCube(intensity*value,0,0);
    }
    private void onMouseLeftAndRightEvent(int value)
    {
        RotateCube(0,intensity*value,0);
    }
    private void RotateCube(float x, float y, float z)
    {
        if (!_isRotate) return;
        transform.Rotate(-x, -y, -z);
    }
    public void StartRotation()
    {
        _isRotate = true;
    }
    public void EndRotation()
    {
        _isRotate = false;
    }
}