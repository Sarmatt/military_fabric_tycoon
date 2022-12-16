using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private ScrollAndPinch _scroll;

    public void OffScrollAndPinch() => _scroll.enabled = false;

    public void OnScrollAndPinch() => _scroll.enabled = true;
}
