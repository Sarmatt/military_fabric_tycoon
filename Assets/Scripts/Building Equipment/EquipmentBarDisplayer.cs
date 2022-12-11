using UnityEngine;
using UnityEngine.UI;

public class EquipmentBarDisplayer : MonoBehaviour
{
    [SerializeField] private Equipment _functional;
    [SerializeField] private Image _image;
    private Camera _camera;

    private void OnEnable() => _functional.TimerValueWasChanged += DisplayFill;
    private void OnDisable() => _functional.TimerValueWasChanged -= DisplayFill;

    private void Start() => _camera = Camera.main;

    private void LateUpdate()
    {
        transform.LookAt(_camera.transform.position);
        transform.Rotate(0, 180, 0);
    }

    private void DisplayFill() 
    {
        float value = _functional.Timer / _functional.GetTimerValue();
        _image.fillAmount = value;
    }
}
