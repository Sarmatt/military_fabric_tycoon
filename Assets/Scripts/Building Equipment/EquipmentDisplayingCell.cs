using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentDisplayingCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _selectedFon;
    [HideInInspector] public CreatingStaff Staff;
    private bool _selected;

    public Equipment Functional;
    [HideInInspector] public EquipmentDisplayer Displayer;

    private void Update() => _selectedFon.SetActive(_selected);

    public void DisplayText(string name) => _text.text = name;

    public void DisplayImage(Sprite image) => _image.sprite = image;

    public void SetFonState(bool state) => _selected = state;

    public void SetStaff()
    {
        Functional.SelectedStaff = Staff;
        Functional.SetTimerValue();
        Displayer.ActiveCells(Staff.Name);
        Functional.CanMakeStaff = true;
        Functional.StaffId = Functional.SelectedStaff.Id;
        GlobalEvents.BuildingGridWasChanged?.Invoke();
    }
}
