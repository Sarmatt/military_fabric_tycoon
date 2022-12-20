using UnityEngine;

[CreateAssetMenu(menuName = "Item parameter/Create texture")]
public class MaterialTexture : ScriptableObject
{
    public int Id;
    public Sprite Image;
    [Range(0, 5)]
    public float AddingRaiting;
}
