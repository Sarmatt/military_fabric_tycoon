using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPanelCellItem : MonoBehaviour
{
    [Header("Rating")]
    [SerializeField] private List<Image> _stars = new List<Image>();
    [SerializeField] private Sprite _fullStar;
    [SerializeField] private Sprite _halfStar;
    [SerializeField] private Sprite _nullStar;
    [Header("Displaying")]
    [SerializeField] private TMP_InputField _priceText;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

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

    public void DisplayData(float rating, int price, Sprite image, int count)
    {
        DisplayStars(rating);
        DisplayText(price);
        DisplayImage(image);
        DisplayCount(count);
    }
}
