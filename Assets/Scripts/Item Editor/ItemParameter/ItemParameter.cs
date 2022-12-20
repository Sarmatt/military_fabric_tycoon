using UnityEngine;

[CreateAssetMenu(menuName = "Item parameter/Create parameter")]
public class ItemParameter : ScriptableObject
{
    public string Name;
    [Range(0, 5)]
    public float AddingRaiting;
}
