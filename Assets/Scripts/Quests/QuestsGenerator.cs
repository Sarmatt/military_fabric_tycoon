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

    //Довести до нормального вигляду
    private int GetQuestItemsCount(List<CreatingStaff> list)
    {
        if (list.Count > 4)
            return Random.Range(1, 3);
        else if (list.Count > 2)
            return Random.Range(1, 2);      
        else
            return 1;
    }

    private CreatingStaff GetGeneratedStaffByDemand(List<CreatingStaff> allStaff)
    {
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

    private List<CreatingStaff> GetGeneraderStaffList(List<CreatingStaff> allStaff)
    {
        List<CreatingStaff> staff = new List<CreatingStaff>(allStaff);
        List<CreatingStaff> resultList = new List<CreatingStaff>();
        for(int i = 0; i < GetQuestItemsCount(allStaff); i++)
        {
            CreatingStaff resStaff = GetGeneratedStaffByDemand(staff);
            if (resStaff == null) return null;
            resultList.Add(resStaff);
            staff.Remove(resStaff);         
        }
        return resultList;
    }

    //Додати кращу генерацію кількості в залежності від кількості станків і темпу виробництва
    private int GetItemCount()
    {
        return Random.Range(1, 10);
    }

    private int GetQuestMoneyCount(List<QuestItem> items)
    {
        int res = 0;
        foreach(var item in items)
            res += item.Count * item.Staff.Price;
        return res;
    }

    private int GetQuestExperienceCount(List<QuestItem> items)
    {
        int res = 0;
        foreach (var item in items)
            res += item.Count * item.Staff.Experience;
        return res;
    }

    private void GenerateQuest()
    {
        List<CreatingStaff> allStaff = StaffGeneralList.singleton.AllStaffForCreating;
        Quest quest = new Quest();
        List<CreatingStaff> items = GetGeneraderStaffList(allStaff);
        if (items == null) return;
        List<QuestItem> questItems = new List<QuestItem>();
        foreach(var item in items)
        {
            QuestItem questItem = new QuestItem(item, GetItemCount());
            questItems.Add(questItem);
        }
        quest.Items = questItems;
        quest.Experience = GetQuestMoneyCount(questItems);
        quest.Money = GetQuestExperienceCount(questItems);
        QuestsFunctional.singleton.AddQuest(quest);
    }
}
