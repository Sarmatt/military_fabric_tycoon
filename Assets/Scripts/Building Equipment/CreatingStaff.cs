using UnityEngine;

[System.Serializable]
public class CreatingStaff
{
    public int Id;
    public int EquipmentId;
    public string Name;
    public float TimeForCreating;
    public int Experience;
    public int Price;
    [Range(0, 5f)]
    public float Rating;
    public Sprite Avatar;
}
