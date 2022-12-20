using UnityEngine;
using TMPro;

public class MainStatsPanel : MonoBehaviour
{
    [Header("Items Panel")]
    [SerializeField] private MainPanelCellItem _cellPref;
    [SerializeField] private Transform _placeForTransforms;

    private void OnEnable()
    {
        DisplayItems();
    }

    private void ClearPlace()
    {
        foreach (Transform child in _placeForTransforms)
            Destroy(child.gameObject);
    }

    private void DisplayItems()
    {
        ClearPlace();
        foreach (var child in StaffGeneralList.singleton.AllStaffForCreating)
        {
            MainPanelCellItem instance = Instantiate(_cellPref, _placeForTransforms);
            instance.DisplayData(child.Name, child.Rating, child.Price, child.Avatar, InventoryFunctional.singleton.GetCountOfItem(child.Id));
        }
    }
}
