using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestSaves : MonoBehaviour
{
    [System.Serializable]
    private class QuestSavingDataItem
    {
        public int StaffId;
        public int StaffCount;

        public QuestSavingDataItem(int staffId, int staffCount)
        {
            StaffId = staffId;
            StaffCount = staffCount;
        }
    }

    [System.Serializable]
    private class QuestsData
    {
        public int Money;
        public int Experience;
        public List<QuestSavingDataItem> QuestItems = new List<QuestSavingDataItem>();

        public QuestsData(List<QuestSavingDataItem> quests, int money, int experience)
        {
            Money = money;
            Experience = experience;
            QuestItems = quests;
        }
    }

    [System.Serializable]
    private class QuestSavingData
    {
        public List<QuestsData> Quests = new List<QuestsData>();

        public QuestSavingData(List<QuestsData> quests)
        {
            Quests = quests;
        } 
    }

    [SerializeField] private QuestsFunctional _functional;
    private string _path;

    private void OnEnable()
    => GlobalEvents.QuestWasGenerated += SaveAllData;
    private void OnDisable()
        => GlobalEvents.QuestWasGenerated -= SaveAllData;

    private void Start()
    {
        InitializePath();

        StartCoroutine(TryToLoadData());
    }

    private void InitializePath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "InventoryData.json");
#else
        _path = Path.Combine(Application.dataPath, "Saves/Quests.json");
#endif
    }

    public void SaveAllData()
    {
        List<QuestsData> savingQuests = new List<QuestsData>();
        foreach(var quest in _functional.Quests)
        {
            List<QuestSavingDataItem> questSavingItems = new List<QuestSavingDataItem>();
            foreach(var questItem in quest.Items)
            {
                QuestSavingDataItem item = new QuestSavingDataItem(questItem.Staff.Id, questItem.Count);
                questSavingItems.Add(item);
            }
            QuestsData data = new QuestsData(questSavingItems, quest.Money, quest.Experience);
            savingQuests.Add(data);
        }
        QuestSavingData resultData = new QuestSavingData(savingQuests);
        var outputSrt = JsonUtility.ToJson(resultData);
        File.WriteAllText(_path, outputSrt);
    }

    private IEnumerator TryToLoadData()
    {   
        if (File.Exists(_path))
        {
            //Штучна затримка
            yield return new WaitForSeconds(0.1f);
            var inputString = File.ReadAllText(_path);
            QuestSavingData data = JsonUtility.FromJson<QuestSavingData>(inputString);

            foreach(var quest in data.Quests)
            {
                List<QuestItem> questItems = new List<QuestItem>();
                foreach(var questItem in quest.QuestItems)
                {
                    QuestItem item = new QuestItem(
                        StaffGeneralList.singleton.GetStaff(questItem.StaffId),
                        questItem.StaffCount);
                    questItems.Add(item);
                }
                Quest resultQuest = new Quest(questItems, quest.Money, quest.Experience);
                _functional.AddQuest(resultQuest);
            }
        }
    }
}
