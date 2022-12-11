using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementDisplayer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _text;
    public Achievement Achievement;
    
    public void DisplayData()
    {
        _image.sprite = Achievement.Image;
        _title.text = Achievement.Title;
        _text.text = Achievement.Text;
    }
}
