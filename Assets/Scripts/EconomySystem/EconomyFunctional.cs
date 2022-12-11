using UnityEngine;
using TMPro;

public class EconomyFunctional : MonoBehaviour
{
    public static EconomyFunctional singleton;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private MainStatsFunctional _mainStats;

    private void Awake() => singleton = this;

    public void AddMoney(int money)
    {
        _mainStats.Money += money;
        DisplayText();
        GlobalEvents.MainStatisticWasChanged?.Invoke();
    }

    public void MinusMoney(int money)
    {
        if (EnoughMoney(money))
        {
            _mainStats.Money -= money;
            DisplayText();
            GlobalEvents.MainStatisticWasChanged?.Invoke();
        }
    }

    public bool EnoughMoney(int money)
    {
        if (_mainStats.Money >= money) return true;
        return false;
    }

    public void DisplayText()
    {
        string res = "";
        if(_mainStats.Money > 1000000)
            res += _mainStats.Money / 1000000 + "M";
        else if(_mainStats.Money > 1000)
            res += _mainStats.Money / 1000 + "K";
        else
            res += _mainStats.Money;
        _moneyText.text = res;
    }
}
