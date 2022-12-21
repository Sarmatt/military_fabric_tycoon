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

    private CreatingStaff GetRandomGeneratedStaff(List<CreatingStaff> staff)
    {
        List<CreatingStaff> allStaff = staff;
        if (allStaff.Count == 0) return null;
        return allStaff[Random.Range(0, allStaff.Count - 1)];
    }

    private CreatingStaff GetGeneratedStaffByDemand()
    {
        List<CreatingStaff> allStaff = new List<CreatingStaff>(StaffGeneralList.singleton.AllStaffForCreating);
        CreatingStaff resStaff = GetRandomGeneratedStaff(allStaff);
        if (resStaff == null) return null;
        int rand = 0;
        for(int i = 0; i < 1000; i++)
        {
            if (resStaff.Demand == 0)
            {
                rand = Random.Range(1, 10);//10% на замовлення, якщо воно не актуальне
                if (rand == 1)
                    return resStaff;
                else
                {
                    allStaff.Remove(resStaff);
                    resStaff = GetRandomGeneratedStaff(allStaff);
                    continue;
                }
                   
            }
            else if(resStaff.Demand == 1)
            {
                rand = Random.Range(1, 2);//50% на замовлення, якщо середня статистика
                if (rand == 1)
                    return resStaff;
                else
                {
                    allStaff.Remove(resStaff);
                    resStaff = GetRandomGeneratedStaff(allStaff);
                    continue;
                }
            }
            else if(resStaff.Demand == 2)//100% на замовлення, якщо максимальна статистика
                return resStaff;
        }
        return resStaff;
    }

    private void GenerateQuest()
    {
        Quest quest = new Quest();
        CreatingStaff curStaff = GetGeneratedStaffByDemand();
        if (curStaff == null) return;
        quest.Item = curStaff;
        quest.Experience = curStaff.Experience * quest.Count;
        quest.Money = quest.Count * curStaff.Price;
        
        //Додати кращу генерацію кількості в залежності від кількості станків і темпу виробництва
        quest.Count = Random.Range(1, 10);
        //---------------------------------------------------------------------------------------

        QuestsFunctional.singleton.AddQuest(quest);
    }
}
