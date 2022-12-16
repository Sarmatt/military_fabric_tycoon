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

    public string ConvertIntToMoneyText(int value)
    {
        string res = "";
        if (value > 1000000)
            res += value / 1000000 + "M";
        else if (value > 1000)
            res += value / 1000 + "K";
        else
            res += value;
        return res;
    }

    public void DisplayText() => _moneyText.text = ConvertIntToMoneyText(_mainStats.Money);
}
