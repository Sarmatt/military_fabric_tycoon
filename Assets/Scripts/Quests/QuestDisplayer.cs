using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class QuestDisplayer : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _titles = new List<TMP_Text>();
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _experience;
    [SerializeField] private GameObject _button;

    [HideInInspector] public Quest CurQuest;

    private void Update()
    {
        if (QuestIsReady())
            _button.SetActive(true);
        else
            _button.SetActive(false);
    }

    private bool QuestIsReady()
    {
        foreach (var item in CurQuest.Items)
        {
            if (!InventoryFunctional.singleton.ContainsStaff(item.Staff.Id, item.Count))
                return false;
        }
        return true;
    }

    public void DisplayData()
    {
        int i = 0;
        foreach(var child in CurQuest.Items)
        {
            _titles[i].text = child.Staff.Name + " x" + child.Count;
            i++;
        }
        _money.text = CurQuest.Money + "";
        _experience.text = CurQuest.Experience + "";
    }

    public void AcceptQuest()
    {
        EconomyFunctional.singleton.AddMoney(CurQuest.Money);
        MainStatsFunctional.singleton.AddExperience(CurQuest.Experience);
        QuestsFunctional.singleton.RemoveQuest(CurQuest);
        foreach(var item in CurQuest.Items)
            InventoryFunctional.singleton.RemoveItem(item.Staff, item.Count);
        Destroy(gameObject);
    }
}
