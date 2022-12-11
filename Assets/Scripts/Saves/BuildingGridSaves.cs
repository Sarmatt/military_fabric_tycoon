using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class BuildingGridDataObject
{
    public int PrefId;
    public int PlaceX;
    public int PlaceZ;
    public int RotY;
    public int CreatingStaffId;
    public Quaternion Rotation;

    public BuildingGridDataObject(int PrefId, int PlaceX, int PlaceZ, int RotY, int CreatingStaffId, Quaternion Rotation)
    {
        this.PrefId = PrefId;
        this.PlaceX = PlaceX;
        this.PlaceZ = PlaceZ;
        this.RotY = RotY;
        this.CreatingStaffId = CreatingStaffId;
        this.Rotation = Rotation;
    }
}

[System.Serializable]
public class BuildingGridData
{
    public List<BuildingGridDataObject> Objects = new List<BuildingGridDataObject>();
} 

public class BuildingGridSaves : MonoBehaviour
{
    [SerializeField] private BuildingGrid _buildingGrid;
    private string _path;

    private void OnEnable() => GlobalEvents.BuildingGridWasChanged += SaveObjects;

    private void OnDisable() => GlobalEvents.BuildingGridWasChanged -= SaveObjects;

    private void Start()
    {
        InitializePath();
        TryToLoadData();
    }

    private void InitializePath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "BuildinGridData.json");
#else
        _path = Path.Combine(Application.dataPath, "BuildinGridData.json");
#endif
    }

    private void SaveObjects()
    {
        BuildingGridData buildingGridData = new BuildingGridData();
        foreach (var item in _buildingGrid.GridObjectsList)
        {
            int CreatingStaffId = item.GetComponent<Equipment>().StaffId;
            BuildingGridDataObject obj = new BuildingGridDataObject(item.PrefId, 
                                                                    item.PlaceX, 
                                                                    item.PlaceZ, 
                                                                    item.RotY, 
                                                                    CreatingStaffId, 
                                                                    item.transform.rotation);
            buildingGridData.Objects.Add(obj);           
        }
        var outputSrt = JsonUtility.ToJson(buildingGridData);
        File.WriteAllText(_path, outputSrt);
    }  

    private void ConvertDataToPlacingObject(BuildingGridDataObject data, PlacingObject placingObj)
    {
        placingObj.PlaceX = data.PlaceX;
        placingObj.PlaceZ = data.PlaceZ;
        placingObj.RotY = data.RotY;
        placingObj.PrefId = data.PrefId;
        placingObj.Placed = true;
        placingObj.transform.rotation = data.Rotation;
        placingObj.PlaceObject();
        AddPlacingObjectToBuildingGrid(placingObj);

        void AddPlacingObjectToBuildingGrid(PlacingObject placingObj)
        {
            _buildingGrid.GridObjectsList.Add(placingObj);
        }
    }


    private void TryToLoadData()
    {
        if (File.Exists(_path))
        {
            var inputString = File.ReadAllText(_path);
            BuildingGridData obj = JsonUtility.FromJson<BuildingGridData>(inputString);
            foreach(var child in obj.Objects)
            {
                GameObject instance = Instantiate(_buildingGrid.GetPrefabByID(child.PrefId)).gameObject;
                instance.transform.position = new Vector3(child.PlaceX, 0, child.PlaceZ);
                ConvertDataToPlacingObject(child, instance.GetComponent<PlacingObject>());
                Equipment equipment = instance.GetComponent<Equipment>();
                equipment.StaffId = child.CreatingStaffId;
                equipment.CanMakeStaff = true;
            }      
        }
    }
}
