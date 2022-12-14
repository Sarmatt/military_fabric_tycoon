using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public List<QuestItem> Items = new List<QuestItem>();
    public int Money;
    public int Experience;

    public Quest() { }

    public Quest(List<QuestItem> items, int money, int experience)
    {
        Items = items;
        Money = money;
        Experience = experience;
    }

}
