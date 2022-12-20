using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StaffGeneralListSaves : MonoBehaviour
{
    [SerializeField] private StaffGeneralList _staffGeneralList;
    private string _path;

    private class SavingData
    {
        public List<CreatingStaff> Staff = new List<CreatingStaff>();
    }

    private void OnEnable() => GlobalEvents.StaffWasAdded += SaveObjects;

    private void OnDisable() => GlobalEvents.StaffWasAdded -= SaveObjects;

    private void Start()
    {
        InitializePath();
        TryToLoadData();
    }

    private void InitializePath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "MainData.json");
#else
        _path = Path.Combine(Application.dataPath, "StaffGeneralList.json");
#endif
    }

    private void TryToLoadData()
    {
        if (File.Exists(_path))
        {
            var inputString = File.ReadAllText(_path);
            SavingData obj = JsonUtility.FromJson<SavingData>(inputString);
            foreach (var child in obj.Staff)
            {
                StaffGeneralList.singleton.AddItem(child);
            }
        }
    }

    private void SaveObjects()
    {
        SavingData buildingGridData = new SavingData();
        buildingGridData.Staff = _staffGeneralList.AllStaffForCreating;
        var outputSrt = JsonUtility.ToJson(buildingGridData);
        File.WriteAllText(_path, outputSrt);
    }
}
