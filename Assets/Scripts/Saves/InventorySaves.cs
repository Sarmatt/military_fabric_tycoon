using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingData
{
    public List<InventoryCell> Cells;
}

public class InventorySaves : MonoBehaviour
{
    private string _path;

    private void Start()
    {
        InitializePath();
        TryToLoadData();
    }

    private void InitializePath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "InventoryData.json");
#else
        _path = Path.Combine(Application.dataPath, "Saves/InventoryData.json");
#endif
    }

    public void SaveAllData()
    {
        SavingData data = new SavingData();
        data.Cells = InventoryFunctional.singleton.Cells;
        var outputSrt = JsonUtility.ToJson(data);
        File.WriteAllText(_path, outputSrt);
    }

    private void TryToLoadData()
    {
        if (File.Exists(_path))
        {
            var inputString = File.ReadAllText(_path);
            SavingData data = JsonUtility.FromJson<SavingData>(inputString);
            foreach (var child in data.Cells)
                InventoryFunctional.singleton.Cells.Add(child);

        }
    }
}
