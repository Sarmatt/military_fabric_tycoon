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

    [Range(0, 2)]
    public int Demand;

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
        GetDemand();
    }

    public void GetDemand()
    {
        StaffShopCell shopCell = ShopItemsContainer.singleton.GetCellById(ShapeId);
        if (Rating < 2f)
        {
            if (Price >= shopCell.MinPrice)
                Demand = 0;
            else
                Demand = 1;
        }
        else if (Rating < 4f)
        {
            if (Price <= shopCell.MiddlePrice)
                Demand = 2;
            else if (Price > shopCell.MaxPrice)
                Demand = 0;
            else
                Demand = 1;
        }
        else
        {
            if (Price <= shopCell.MaxPrice)
                Demand = 2;
            else
                Demand = 0;
        }        
    }

    public void ChangePrice(int value)
    {
        Price = value;
        GetDemand();
    }
}
