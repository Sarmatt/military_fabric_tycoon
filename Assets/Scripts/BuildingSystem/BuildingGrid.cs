using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[System.Serializable]
public struct Building
{
    public int Id;
    public PlacingObject Prefab;
}

public class BuildingGrid : MonoBehaviour
{
    public static BuildingGrid singleton;

    [SerializeField] private List<Building> _buildings = new List<Building>();
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    public List<PlacingObject> GridObjectsList = new List<PlacingObject>();
    private PlacingObject _selectedObject;
    private Camera _mainCamera;
    [SerializeField] private Transform _ground;
    [SerializeField] private ScrollAndPinch _scrollingScript;
    private bool _swithcer = false;
    private bool _startedDragging = false;

    private Vector3 _tmpWorldPos;

    private void Awake()
    {
        singleton = this;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(_selectedObject != null)
        {
            _scrollingScript.enabled = false;
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!_swithcer)
            {
                _startedDragging = true;
                _swithcer = true;
            }

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);

                RotatePlacingObj(x, z);

                _selectedObject.transform.position = new Vector3(x, 0, z);
                _selectedObject.SetTransparent(ObjectCanBePlaced(x, z));

                _tmpWorldPos = new Vector3(x, _selectedObject.transform.position.y, z);

                if (Input.GetMouseButtonDown(0))
                {
                    if (!_startedDragging)
                    {
                        if (ObjectCanBePlaced(x, z))
                            PlaceObject(x, z);
                        else
                            DontPlaceObject();
                        _scrollingScript.gameObject.SetActive(true);
                        _startedDragging = false;
                        _swithcer = false;
                    }
                    else
                        _startedDragging = false;
                }
            }         
        }       
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int z = 0; z < _gridSize.y; z++)
            {
                if ((x + z) % 2 == 0) Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
                else Gizmos.color = new Color(0f, 5f, 5f, 0.4f);
                Gizmos.DrawCube(_ground.position + new Vector3(x, 0, z), new Vector3(1f, 0.1f, 1f));
            }
        }
    }

    private bool PlaceTaken(int placeX, int placeZ)
    {
        foreach(var child in GridObjectsList)
        {
            if (child.PlaceX == placeX && child.PlaceZ == placeZ)
                return true;
        }
        return false;
    }

    private bool ObjectCanBePlaced(int x, int z)
    {
        if (x < 0 || x > _gridSize.x - _selectedObject.Size.x) return false;
        if (z < 0 || z > _gridSize.y - _selectedObject.Size.y) return false;
        if (PlaceTaken(x, z)) return false;
        return true;
    }

    private void DontPlaceObject()
    {
        Destroy(_selectedObject.gameObject);
    }

    private void RotatePlacingObj(int x, int z)
    {
        Transform selectedObjectTransform = _selectedObject.transform;
        if (x > _tmpWorldPos.x)
            _selectedObject.transform.rotation = Quaternion.Euler(selectedObjectTransform.rotation.x, 270, selectedObjectTransform.rotation.z);
        else if (x < _tmpWorldPos.x)
            _selectedObject.transform.rotation = Quaternion.Euler(selectedObjectTransform.rotation.x, 90, selectedObjectTransform.rotation.z);
        else if (z > _tmpWorldPos.z)
            _selectedObject.transform.rotation = Quaternion.Euler(selectedObjectTransform.rotation.x, 180, selectedObjectTransform.rotation.z);
        else if (z < _tmpWorldPos.z)
            _selectedObject.transform.rotation = Quaternion.Euler(selectedObjectTransform.rotation.x, 360, selectedObjectTransform.rotation.z);
    }

    private void PlaceObject(int placeX, int placeZ)
    {
        int neededMoney = _selectedObject.GetComponent<Equipment>().NeededMoney;
        EconomyFunctional.singleton.MinusMoney(neededMoney);
        _selectedObject.PlaceX = placeX;
        _selectedObject.PlaceZ = placeZ;
        _selectedObject.Placed = true;
        _selectedObject.PlaceObject();
        GridObjectsList.Add(_selectedObject);
        GlobalEvents.BuildingGridWasChanged?.Invoke();
        _selectedObject = null;
    }

    public PlacingObject GetPrefabByID(int id)
    {
        foreach(var child in _buildings) 
            if (child.Id == id)
                return child.Prefab;
        Debug.LogError("Can't fing Prefab with id: " + id);
        return null;
    }

    public void StartPlacing(PlacingObject prefab)
    {
        int neededMoney = prefab.GetComponent<Equipment>().NeededMoney;
        if (EconomyFunctional.singleton.EnoughMoney(neededMoney))
        {
            if (_selectedObject != null)
                Destroy(_selectedObject.gameObject);
            _selectedObject = Instantiate(prefab);     
        }     
    }

    public void MoveObject(PlacingObject obj)
    {
        GridObjectsList.Remove(obj);
        _selectedObject = obj;
    }
}
