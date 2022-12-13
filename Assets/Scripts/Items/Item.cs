using ObjectAction;
using UnityEngine;
using UnityEngine.EventSystems;


public class Cube : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Renderer _renderer;
    private RotateObject _rotateObject;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Texture texture))
        {
            _renderer.material = texture.GetMaterial();
            if (other.TryGetComponent(out Draggable movingObject))
            {
                movingObject.EndMoving();
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_rotateObject == null) return;

        if (eventData.clickCount >= 2)
        {
            _rotateObject.EndRotation();
        }
        else
        {
            _rotateObject.StartRotation();
        }

    }
}