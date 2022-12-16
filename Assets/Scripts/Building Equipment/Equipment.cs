using System;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Tooltip("1 - Sewing machine, 2 -...")]
    public int Id;
    public Action TimerValueWasChanged;
    public string Name;
    public int NeededMoney;
    public Sprite Avatar;
    public float TimeCoef = 1;
    [HideInInspector] public int StaffId;
    public CreatingStaff SelectedStaff;

    [HideInInspector] public bool CanMakeStaff;
    public float Timer;

    private void Update()
    {
        if (CanMakeStaff)
        {
            if (Timer > 0)
            {
                TimerValueWasChanged?.Invoke();
                Timer -= Time.deltaTime;
            }
            else
            {
                InventoryFunctional.singleton.AddItem(SelectedStaff);
                SetTimerValue();
            }
        }
    }

    public float GetTimerValue() => SelectedStaff.TimeForCreating * TimeCoef;
    public void SetTimerValue()
    {
        if(StaffId != 0)
            SelectedStaff = StaffGeneralList.singleton.GetStaff(StaffId);
        Timer = GetTimerValue();
    }
}
