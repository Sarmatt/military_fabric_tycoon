using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPanelCellItem : MonoBehaviour
{
    public CreatingStaff Staff;
    [Header("Rating")]
    [SerializeField] private List<Image> _stars = new List<Image>();
    [SerializeField] private Sprite _fullStar;
    [SerializeField] private Sprite _halfStar;
    [SerializeField] private Sprite _nullStar;
    [Header("Displaying")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_InputField _priceText;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

    [Header("Arrows")]
    [SerializeField] private Image _arrowImage;
    [SerializeField] private Sprite _upArrow;
    [SerializeField] private Sprite _downArrow;

    private void DisplayStars(float rating) 
    {
        float tmp = rating;
        int i = 0;
        while(tmp > 0)
        {
            if (tmp == 0.5f)
                _stars[i].sprite = _halfStar;
            else
                _stars[i].sprite = _fullStar;
            i++;
            tmp--;
        }
    }

    private void DisplayText(int price) => _priceText.text = "" + price;

    private void DisplayImage(Sprite image) => _image.sprite = image;

    private void DisplayCount(int count) => _countText.text = count + "";

    public void DisplayData(string name, float rating, int price, Sprite image, int count)
    {
        _nameText.text = name;
        DisplayStars(rating);
        DisplayText(price);
        DisplayImage(image);
        DisplayCount(count);
        DisplayArrow();
    }

    private void DisplayArrow()
    {
        _arrowImage.gameObject.SetActive(true);
        int demand = Staff.Demand;
        if (demand == 0)
            _arrowImage.sprite = _downArrow;
        else if (demand == 1)
            _arrowImage.gameObject.SetActive(false);
        else
            _arrowImage.sprite = _upArrow;
    }

    public void SetPrice(string value)
    {
        Staff.ChangePrice(int.Parse(value));
        DisplayArrow();
    }

}
