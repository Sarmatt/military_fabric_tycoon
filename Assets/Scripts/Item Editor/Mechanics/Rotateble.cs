using Event.Mouse;
using UnityEngine;
public class Rotateble : MonoBehaviour
{
    [SerializeField] private float _lAndRIntensity = 3.2f;
    [SerializeField] private float _upIntensity = 2.3f;
    [SerializeField] private bool _isRotate;
 
    private void OnEnable()
    {
        TouchUpAndDownEvent.onMouse += OnMouseUpAndDownEvent;
        TouchLeftAndRightEvent.onMouse += OnMouseLeftAndRightEvent ;
    }
    private void OnDisable()
    {
        TouchUpAndDownEvent.onMouse -= OnMouseUpAndDownEvent;
        TouchLeftAndRightEvent.onMouse -= OnMouseLeftAndRightEvent ;
    }
    private void OnMouseUpAndDownEvent(int value)
    {
        RotateCube(_upIntensity * value, 0, 0);
    }
    private void OnMouseLeftAndRightEvent(int value)
    {
        RotateCube(0, _lAndRIntensity * value, 0);
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