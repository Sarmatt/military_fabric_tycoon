[System.Serializable]
public class QuestItem
{
    public CreatingStaff Staff;
    public int Count;

    public QuestItem(CreatingStaff staff, int count)
    {
        Staff = staff;
        Count = count;
    }
}
