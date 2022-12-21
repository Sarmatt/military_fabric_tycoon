using System.IO;
using UnityEngine;

public class MainData
{
    public string Name;
    public int Level;
    public float Experience;
    public float MaxExperience;
    public int Money;

    public MainData(string Name, int Level, float Experience, float MaxExperience, int Money)
    {
        this.Name = Name;
        this.Level = Level;
        this.Experience = Experience;
        this.MaxExperience = MaxExperience;
        this.Money = Money;
    }
}

public class MainStatsSaves : MonoBehaviour
{
    [SerializeField] private MainStatsFunctional _functional;
    private string _path;

    private void OnEnable() => GlobalEvents.MainStatisticWasChanged += SaveData;

    private void OnDisable() => GlobalEvents.MainStatisticWasChanged -= SaveData;

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
        _path = Path.Combine(Application.dataPath, "Saves/MainData.json");
#endif
    }

    private void TryToLoadData()
    {
        if (File.Exists(_path))
        {
            var inputString = File.ReadAllText(_path);
            MainData obj = JsonUtility.FromJson<MainData>(inputString);
            _functional.Name = obj.Name;
            _functional.Level = obj.Level;
            _functional.Experience = obj.Experience;
            _functional.MaxExperience = obj.MaxExperience;
            _functional.Money = obj.Money;
            _functional.DisplayData();
        }
    }

    private void SaveData()
    {
        MainData data = new MainData(_functional.Name, _functional.Level, _functional.Experience, _functional.MaxExperience, _functional.Money);
        var outputSrt = JsonUtility.ToJson(data);
        File.WriteAllText(_path, outputSrt);
    }
}
