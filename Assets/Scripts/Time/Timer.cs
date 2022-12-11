using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Timer : MonoBehaviour
{
    [Header("Functional")]
    [Tooltip("Перший елемент temp, щоб відлік був з 1 елемента")]
    [SerializeField] private List<int> _monthsDays = new List<int>();
    [SerializeField] private int _day;
    [SerializeField] private int _month;
    [SerializeField] private int _year;
    [SerializeField] private float _timer;
    private float _tmpTimer;

    [Header("Displayer")]
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        DisplayDate();
        _tmpTimer = _timer;
    }

    private void Update()
    {
        if(_timer > 0)
            _timer -= Time.deltaTime;
        else
        {
            _day++;
            if(_monthsDays[_month] < _day)
            {
                _month++;
                _day = 1;
            }
            if(_month > 12)
            {
                _year++;
                _month = 1;
            }
            DisplayDate();
            _timer = _tmpTimer;
        }
    }

    private void DisplayDate()
    {
        string dateString = _day < 10 ? "0" : "";
        dateString += _day + ".";
        dateString += _month < 10 ? "0" : "";
        dateString += _month + ".";
        dateString += _year;
        _text.text = dateString;
    }

}
