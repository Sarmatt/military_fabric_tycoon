using System.Collections.Generic;
using UnityEngine;

public class AchievemrntPanel : MonoBehaviour
{
    [SerializeField] private List<Achievement> _achievements = new List<Achievement>();
    [SerializeField] private GameObject _cellPref;
    [SerializeField] private Transform _place;

    private void OnEnable() => DisplayCells();

    private void ClearPlace()
    {
        if(_place.childCount > 0)
            foreach (Transform child in _place)
                Destroy(child.gameObject);
    }

    private void DisplayCells()
    {
        ClearPlace();
        foreach(var child in _achievements)
        {
            AchievementDisplayer instance = Instantiate(_cellPref, _place).GetComponent<AchievementDisplayer>();
            instance.Achievement = child;
            instance.DisplayData();
        }
    }
}
