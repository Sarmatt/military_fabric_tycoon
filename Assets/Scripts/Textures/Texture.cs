using UnityEngine;


public class Texture : MonoBehaviour
{
    [SerializeField] private Material _material;
    public Material GetMaterial()
    {
        return _material;
    }
}