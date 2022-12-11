using System.Collections.Generic;
using UnityEngine;

public class QuestsGenerator : MonoBehaviour
{

    [SerializeField] private float _timer;
    [SerializeField] private float _tempTimer;

    private void Srtart() => _tempTimer = _timer;

    private void Update()
    {
        if(QuestsFunctional.singleton.Quests.Count < 4)
        {
            if (_tempTimer > 0)
                _tempTimer -= Time.deltaTime;
            else
            {
                GenerateQuest();
                _tempTimer = _timer;
            }
        }
    }

    private void GenerateQuest()
    {
        Quest quest = new Quest();
        List<CreatingStaff> staff = StaffGeneralList.singleton.AllStaffForCreating;
        int rand = Random.Range(0, staff.Count - 1);
        CreatingStaff curStaff = staff[rand];
        quest.Item = curStaff;
        quest.Count = Random.Range(1, 10);
        quest.Experience = curStaff.Experience * quest.Count;
        quest.Money = quest.Count * curStaff.Price;
        QuestsFunctional.singleton.AddQuest(quest);
    }
}
