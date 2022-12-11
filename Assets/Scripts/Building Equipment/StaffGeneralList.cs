using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaffGeneralList : MonoBehaviour
{
    public static StaffGeneralList singleton;
    public List<CreatingStaff> AllStaffForCreating = new List<CreatingStaff>();
    private void Awake() => singleton = this;

    public List<CreatingStaff> GetCreatingStaffListById(int id)
    {
        IEnumerable<CreatingStaff> result = from staff in AllStaffForCreating
                                            where staff.EquipmentId == id
                                            select staff;
        return result.ToList();
    }

    public string GetStaffName(int id)
    {
        foreach (var staff in AllStaffForCreating)
            if (staff.Id == id) return staff.Name;
        Debug.LogError("Can't find staff name whith if: " + id);
        return null;
    }

    public CreatingStaff GetStaff(int id)
    {
        foreach (var staff in AllStaffForCreating)
            if (staff.Id == id)
                return staff;
        Debug.LogError("Can't find creating staff with id: " + id);
        return null;
    }
}
