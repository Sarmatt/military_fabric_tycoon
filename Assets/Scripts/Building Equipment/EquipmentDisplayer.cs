using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EquipmentDisplayer : MonoBehaviour
{
    public static EquipmentDisplayer singleton;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;
    private Equipment _functional;
    [SerializeField] private ScrollAndPinch _scrollAndPinchScript;

    [Header("Displaying Items")]
    [SerializeField] private Transform _placeForCells; 
    [SerializeField] private GameObject _cellPrefab;

    private void Awake() => singleton = this;

    public void DisplayPanel(Equipment obj)
    {
        _panel.SetActive(true);    
        _functional = obj;
        DisplayItems();
        _text.text = _functional.Name;
        _scrollAndPinchScript.enabled = false;
    }

    public void EnableScroolAndPinch() => _scrollAndPinchScript.enabled = true;

    public void StartPlacing()
    {
        _panel.SetActive(false);
        PlacingObject functional = _functional.GetComponent<PlacingObject>();
        functional.ChangingPlace = true;
        BuildingGrid.singleton.MoveObject(functional);
    }

    private void DisplayItems()
    {
        List<CreatingStaff> items = StaffGeneralList.singleton.GetCreatingStaffListById(_functional.Id);
        if(_placeForCells.childCount != 0)
            foreach (Transform child in _placeForCells)
                Destroy(child.gameObject);
        foreach (var item in items)
        {
            EquipmentDisplayingCell instance = Instantiate(_cellPrefab, _placeForCells).GetComponent< EquipmentDisplayingCell>();
            instance.DisplayText(item.Name);
            instance.DisplayImage(item.Avatar);
            instance.Functional = _functional;
            instance.Displayer = this;
            instance.Staff = item;
        }
        ActiveCells(_functional.SelectedStaff.Name);
    }

    public void CellEquipment()
    {      
        EconomyFunctional.singleton.AddMoney(_functional.NeededMoney / 3);
        BuildingGrid.singleton.GridObjectsList.Remove(_functional.GetComponent<PlacingObject>());
        Destroy(_functional.gameObject);
        GlobalEvents.BuildingGridWasChanged?.Invoke();
    }

    public void ActiveCells(string name)
    {
        foreach(Transform cell in _placeForCells)
        {
            EquipmentDisplayingCell script = cell.GetComponent<EquipmentDisplayingCell>();
            if (script.Staff.Name == name) 
                script.SetFonState(true);
            else
                script.SetFonState(false);
        }
    }
}
