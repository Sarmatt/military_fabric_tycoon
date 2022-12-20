using UnityEngine;
using UnityEngine.UI;

public class ItemTextureCellDisplayer : MonoBehaviour
{
    private MaterialTexture _materialTexture;
    [SerializeField] private Image _image;

    public void InitData(MaterialTexture parameter)
    {
        _materialTexture = parameter;
        _image.sprite = parameter.Image;
    }

    public void ChooseCell()
        => ItemsEditorPanel.singleton.ChangeTexture(_materialTexture.Id, _materialTexture);
}