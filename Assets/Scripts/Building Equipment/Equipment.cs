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
    [SerializeField] private float _timeCoef = 1;
    [HideInInspector] public int StaffId;
    public CreatingStaff SelectedStaff;
    private bool _staffLoaded = false;

    [HideInInspector] public bool CanMakeStaff;
    private bool _creatingItem;

    public float Timer;

    private void Update()
    {
        if (!_staffLoaded && StaffId != 0)
        {
            SelectedStaff = StaffGeneralList.singleton.GetStaff(StaffId);
            _staffLoaded = true;
        }
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

    public float GetTimerValue() => SelectedStaff.TimeForCreating * _timeCoef;
    public void SetTimerValue() => Timer = GetTimerValue();
}
