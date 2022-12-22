using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainStatsFunctional : MonoBehaviour
{
    public static MainStatsFunctional singleton;
    [Header("Functional")]
    public int Money;
    public string Name;
    public int Level;
    public float Experience;
    public float MaxExperience;
    [SerializeField] private EconomyFunctional _economyFunctional;
    [Header("Displaying")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _fonExperience;
    [SerializeField] private GameObject _notEnoughtMoneyPanel;

    private void Awake() => singleton = this;

    private void Start() => DisplayData();

    public void DisplayData()
    {
        float fill = Experience / MaxExperience;
        _fonExperience.fillAmount = fill;
        _nameText.text = Name;
        _levelText.text = "" + Level;
        _economyFunctional.DisplayText();
    }

    public void AddExperience(int adding)
    {
        Experience += adding;
        if (Experience >= MaxExperience) LevelUp();
        DisplayData();
        GlobalEvents.MainStatisticWasChanged?.Invoke();
    }

    public void LevelUp()
    {
        Level++;
        Experience  -= MaxExperience;
        MaxExperience += (int)(Experience * 0.25f);
        GlobalEvents.MainStatisticWasChanged?.Invoke();
    }

    public void SetName(string name)
    { 
        Name = name;
        _nameText.text = Name;
        GlobalEvents.MainStatisticWasChanged?.Invoke();
    }

    public bool EnoughtMoney(int value)
    {
        if(Money < value)
        {
            _notEnoughtMoneyPanel.SetActive(true);
            return false;
        }
        return true;
    }
}
