using UnityEngine;
using TMPro;

public class QuestDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _experience;
    [SerializeField] private GameObject _button;

    [HideInInspector] public Quest CurQuest;

    private void Update()
    {
        if (InventoryFunctional.singleton.ContainsStaff(CurQuest.Item.Id, CurQuest.Count))
            _button.SetActive(true);
        else
            _button.SetActive(false);
    }

    public void DisplayData()
    {
        _title.text = CurQuest.Item.Name + " x" + CurQuest.Count;
        _money.text = CurQuest.Money + "";
        _experience.text = CurQuest.Experience + "";
    }

    public void AcceptQuest()
    {
        EconomyFunctional.singleton.AddMoney(CurQuest.Money);
        MainStatsFunctional.singleton.AddExperience(CurQuest.Experience);
        QuestsFunctional.singleton.RemoveQuest(CurQuest);
        InventoryFunctional.singleton.RemoveItem(CurQuest.Item, CurQuest.Count);
        Destroy(this.gameObject);
    }
}
