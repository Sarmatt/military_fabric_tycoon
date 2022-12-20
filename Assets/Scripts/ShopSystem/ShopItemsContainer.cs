using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaffShopCell
{
    public List<int> ShapeIds = new List<int>();
    public int MinPrice;
    public int MiddlePrice;
    public int MaxPrice;
}

public class ShopItemsContainer : MonoBehaviour
{
    public static ShopItemsContainer singleton;
    public List<StaffShopCell> Cells = new List<StaffShopCell>();

    private void Awake()
    {
        if (singleton != null)
            Destroy(gameObject);
        singleton = this;
        DontDestroyOnLoad(this);
       
    }

    public StaffShopCell GetCellById(int id)
    {
        foreach(var cell in Cells)
            foreach(var child in cell.ShapeIds)
                if (child == id)
                    return cell;

        Debug.LogError("Shop cells doesn't contains this id: " + id);
        return null;
    }
}
