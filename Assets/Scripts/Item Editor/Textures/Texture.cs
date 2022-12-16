using Core;
using Item_Editor.Mechanics;
using UnityEngine;


public class Texture : MonoBehaviour
{
    [SerializeField] private Material _material;
    private Droppable _drop;
    private void Awake()
    {
        _drop = gameObject.GetComponent<Droppable>();
        _drop.Tag = Tag.Item;
        _drop.DropAction = DropAction;
    }
    private void DropAction(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material = _material;
    }
}