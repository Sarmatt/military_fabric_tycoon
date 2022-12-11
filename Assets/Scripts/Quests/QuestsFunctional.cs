using System.Collections.Generic;
using UnityEngine;

public class QuestsFunctional : MonoBehaviour
{
    public static QuestsFunctional singleton;
    [Header("Functional")]
    public List<Quest> Quests = new List<Quest>();

    [Header("Displaying")]
    public Transform _placeForQuests;
    [SerializeField] private GameObject _questPref;

    private void Awake() => singleton = this;

    private void OnEnable() => DisplayQuests();

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
        QuestDisplayer instance = Instantiate(_questPref, _placeForQuests).GetComponent<QuestDisplayer>();
        instance.CurQuest = quest;
        instance.DisplayData();
    }

    public void RemoveQuest(Quest quest) => Quests.Remove(quest);

    private void DisplayQuests()
    {
        foreach (Transform child in _placeForQuests)
            Destroy(child.gameObject);

        foreach(var quest in Quests)
        {
            QuestDisplayer instance = Instantiate(_questPref, _placeForQuests).GetComponent<QuestDisplayer>();
            instance.CurQuest = quest;
            instance.DisplayData();
        }
    }
}
