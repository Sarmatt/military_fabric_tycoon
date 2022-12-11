using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCell : MonoBehaviour
{
    [HideInInspector] public GameObject Panel;
    [HideInInspector] public Equipment Equipment;
    [SerializeField] private PlacingObject _object;
    [Header("Displayer")]
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Image Image;

    private void Start() => _object = Equipment.gameObject.GetComponent<PlacingObject>();

    public void DisplayData()
    {
        Image.sprite = Equipment.Avatar;
        _titleText.text = "" + Equipment.Name;
        _moneyText.text = "" + Equipment.NeededMoney;
    }

    public void PlaceObject()
    {
        PanelsHandler.singleton.ClosePanel(Panel);
        BuildingGrid.singleton.StartPlacing(_object);
    }
}
