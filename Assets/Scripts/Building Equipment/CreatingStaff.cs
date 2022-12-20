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
    public int ShapeId;
    public float Rating;
    public MaterialTexture Camouflage;
    public ItemParameter MaterialType;
    public ItemParameter Season;
    public Sprite Avatar;

    public CreatingStaff(
        int id,
        int equipmentId,
        string name,
        int price,
        float timeForCreating,
        int experience,
        float rating,
        int shapeId,
        MaterialTexture camouflage,
        ItemParameter materialType,
        ItemParameter season,
        Sprite avatar) 
    {
        Id = id;
        EquipmentId = equipmentId;
        Name = name;
        Price = price;
        TimeForCreating = timeForCreating;
        Experience = experience;
        Rating = rating;
        ShapeId = shapeId;
        Camouflage = camouflage;
        MaterialType = materialType;
        Season = season;
        Avatar = avatar;
    }
}
