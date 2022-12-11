using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Equipment _object;

    public void OnPointerClick(PointerEventData eventData)
    {
        EquipmentDisplayer.singleton.DisplayPanel(_object);
    }
}
