using System.Collections.Generic;
using UnityEngine;

public class QuestsFunctional : MonoBehaviour
{
    public static QuestsFunctional singleton;
    [Header("Functional")]
    public List<Quest> Quests = new List<Quest>();

    [Header("Displaying")]
    public Transform _placeForQuests;
    [SerializeField] private GameObject _oneItemQuestPref;
    [SerializeField] private GameObject _twoItemQuestPref;
    [SerializeField] private GameObject _threeItemQuestPref;

    private void Awake() => singleton = this;

    private void OnEnable() => DisplayQuests();

    public void GenerateQuest(Quest quest)
    {
        QuestDisplayer instance = null;
        if (quest.Items.Count == 1)
            instance = Instantiate(_oneItemQuestPref, _placeForQuests).GetComponent<QuestDisplayer>();
        else if (quest.Items.Count == 2)
            instance = Instantiate(_twoItemQuestPref, _placeForQuests).GetComponent<QuestDisplayer>();
        else if (quest.Items.Count == 3)
            instance = Instantiate(_threeItemQuestPref, _placeForQuests).GetComponent<QuestDisplayer>();
        instance.CurQuest = quest;
        instance.DisplayData();
    }

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
        GenerateQuest(quest);
    }

    public void RemoveQuest(Quest quest) => Quests.Remove(quest);

    private void DisplayQuests()
    {
        foreach (Transform child in _placeForQuests)
            Destroy(child.gameObject);
        foreach(var quest in Quests)
            GenerateQuest(quest);
    }
}
