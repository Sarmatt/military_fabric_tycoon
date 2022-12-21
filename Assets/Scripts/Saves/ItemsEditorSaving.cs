using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemsEditorSaving : MonoBehaviour
{
    private class ItemsEditorData
    {
        public int LastItemId;

        public ItemsEditorData(int lastItemId)
        {
            LastItemId = lastItemId;
        }
    }

    [SerializeField] private ItemsEditorPanel _functional;
    private string _path;

    private void OnEnable()
        => GlobalEvents.StaffWasAdded += SaveData;
    private void OnDisable()
        => GlobalEvents.StaffWasAdded -= SaveData;

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
        _path = Path.Combine(Application.dataPath, "Saves/ItemsEditor.json");
#endif
    }

    private void TryToLoadData()
    {
        if (File.Exists(_path))
        {
            var inputString = File.ReadAllText(_path);
            ItemsEditorData data = JsonUtility.FromJson<ItemsEditorData>(inputString);
            _functional.CurrentStaffId = data.LastItemId;
        }
    }

    private void SaveData()
    {
        ItemsEditorData data = new ItemsEditorData(_functional.CurrentStaffId);
        var outputSrt = JsonUtility.ToJson(data);
        File.WriteAllText(_path, outputSrt);
    }
}
