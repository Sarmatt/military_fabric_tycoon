using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFunctional : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private List<GameObject> _equipment = new List<GameObject>();
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _placeForCells;

    private void OnEnable()
    {
        DisplayItems();
    }

    private void DisplayItems()
    {
        foreach (Transform child in _placeForCells)
            Destroy(child.gameObject);

        foreach(var equipment in _equipment)
        {
            ShopCell instance = Instantiate(_prefab, _placeForCells).GetComponent<ShopCell>();
            instance.Equipment = equipment.GetComponent<Equipment>();
            instance.Panel = _panel;
            instance.DisplayData();
        }
    }
}
