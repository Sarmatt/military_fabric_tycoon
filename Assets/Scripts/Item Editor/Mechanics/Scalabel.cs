using Event.Mouse;
using UnityEngine;


public class Scalabel : MonoBehaviour
{
    public float MaxDistance = 25;
    public float MinDistance = 6;
    public Camera Camera;
    private float _itemSquare;
    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
        Camera.transform.position = new Vector3(transform.position.x,transform.position.y-1,-10);
    }
    private void OnEnable()
    {
        DoubleTouchEvent.Handle += DoubleTouchEventHandle;
    }
    private void OnDisable()
    {
        DoubleTouchEvent.Handle -= DoubleTouchEventHandle;
    }
    private void DoubleTouchEventHandle(Touch touch1,Touch touch2)
    {
        var pos1 = touch1.position;
        var pos2 = touch2.position;
        var pos1b = touch1.position - touch1.deltaPosition;
        var pos2b = touch2.position - touch2.deltaPosition;

        //calc zoom
        var zoom = Vector3.Distance(pos1b,pos2b) /
                   Vector3.Distance(pos1,pos2);

        var newPosition = new Vector3(Camera.transform.position.x,Camera.transform.position.y,Camera.transform.position.z * zoom);

        var newDistance = Vector3.Distance(newPosition,transform.position);
        if (MinDistance > newDistance || newDistance > MaxDistance) return;

        Camera.transform.position = newPosition;
    }
}