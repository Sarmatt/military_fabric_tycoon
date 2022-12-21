using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemsEditorPanel : MonoBehaviour
{
    public static ItemsEditorPanel singleton;

    [Header("Main Parameters")]
    [HideInInspector] public int CurrentStaffId;
    [SerializeField] private List<EditingPref> _editingPrefs;
    [SerializeField] private int _currentEditingPrefId;
    [SerializeField] private EditingPref _editingPref;

    [Header("Name")]
    [SerializeField] private string _itemName;

    [Header("Material Type")]
    [SerializeField] private TMP_Text _clothTypeText;
    private int _currentClothTypeid = 0;

    [Header("Season")]
    [SerializeField] private TMP_Text _seasonText;
    private int _currentSeasonId = 0;

    [Header("Price")]
    [SerializeField] private TMP_Text _priceText;
    private int _price;
    [SerializeField] private Slider _slider;

    [Header("Texture")]
    [SerializeField] private GameObject _textureCell;
    [SerializeField] private Transform _placeForCells;
    [SerializeField] private MaterialTexture _currentCamo;
    [SerializeField] private List<StaffAvatarData> _avatars;

    [Header("Stars")]
    [SerializeField] private Sprite _emptyStar;
    [SerializeField] private Sprite _halfStar;
    [SerializeField] private Sprite _fullStar;
    [SerializeField] private List<Image> _qualityStars = new List<Image>();
    [SerializeField] private List<Image> _visualStars = new List<Image>();
    [SerializeField] private List<Image> _actualityStars = new List<Image>();


    //Rating
    private float _visualRating;
    private float _qualityRating;
    private float _actualityRating;

    private void Awake()
        => singleton = this;

    private void Start()
    {
        InitializeStartValues();
    }

    public void ChangePrice()
    {
        int res = _editingPref.StartPrice + (int)(_editingPref.StartPrice * _slider.value * 4);
        _priceText.text = "" + res;
        _price = res;
    }

    private void PlusListObject(List<ItemParameter> list, ref int id, TMP_Text text)
    {
        id++;
        if (id >= list.Count)
            id = 0;
        text.text = list[id].Name;
    }

    private void MinusListObject(List<ItemParameter> list, ref int id, TMP_Text text)
    {
        id--;
        if (id < 0)
            id = list.Count - 1;
        text.text = list[id].name;
    }

    public void DisplayStars(List<Image> stars, float value)
    {
        foreach(var star in stars)
        {
            if (value >= 1)
                star.sprite = _fullStar;
            else if (value < 1 && value > 0)
                star.sprite = _halfStar;
            else
                star.sprite = _emptyStar;
            value--;
        }
    }

    public void PlusClothType()
    {
        PlusListObject(_editingPref.ClothType, ref _currentClothTypeid, _clothTypeText);
        DisplayStars(_qualityStars, _editingPref.ClothType[_currentClothTypeid].AddingRaiting);
        _qualityRating = _editingPref.ClothType[_currentClothTypeid].AddingRaiting;
    }

    public void MinusClothType()
    {
        MinusListObject(_editingPref.ClothType, ref _currentClothTypeid, _clothTypeText);
        DisplayStars(_qualityStars, _editingPref.ClothType[_currentClothTypeid].AddingRaiting);
        _qualityRating = _editingPref.ClothType[_currentClothTypeid].AddingRaiting;
    }

    public void PlusSeason()
    {
        PlusListObject(_editingPref.Seasons, ref _currentSeasonId, _seasonText);
        DisplayStars(_actualityStars, _editingPref.Seasons[_currentSeasonId].AddingRaiting);
        _actualityRating = _editingPref.Seasons[_currentSeasonId].AddingRaiting;
    }

    public void MinusSeason()
    {
        MinusListObject(_editingPref.Seasons, ref _currentSeasonId, _seasonText);
        DisplayStars(_actualityStars, _editingPref.Seasons[_currentSeasonId].AddingRaiting);
        _actualityRating = _editingPref.Seasons[_currentSeasonId].AddingRaiting;
    }

    public void ChangeTexture(int matid, MaterialTexture texture)
    {
        foreach (var child in _editingPref.Renderers)
            child.material = _editingPref.Materials[matid];
        DisplayStars(_visualStars, texture.AddingRaiting);
        _visualRating = texture.AddingRaiting;
        _currentCamo = texture;
    }

    private void DisplayTextures()
    {
        ChangeTexture(0, _editingPref.Camouflages[0]);
        foreach (var child in _editingPref.Camouflages)
        {
            var instance = Instantiate(_textureCell, _placeForCells)
                .GetComponent<ItemTextureCellDisplayer>();
            instance.InitData(child);
        }
    }

    public void ChangeName(string name)
        => _itemName = name;

    private void InitializeStartValues()
    {
        _slider.value = 0;
        DisplayTextures();
        DisplayStars(_qualityStars, _editingPref.ClothType[0].AddingRaiting);
        DisplayStars(_visualStars, _editingPref.Camouflages[0].AddingRaiting);
        DisplayStars(_actualityStars, _editingPref.Seasons[0].AddingRaiting);
        _priceText.text = "" + _editingPref.StartPrice;
        _clothTypeText.text = _editingPref.ClothType[_currentClothTypeid].Name;
        _seasonText.text = _editingPref.Seasons[_currentSeasonId].Name;
    }

    private void ClearTexturesPlace()
    {
        foreach (Transform child in _placeForCells)
            Destroy(child.gameObject);
    }

    private void ChangeShape()
    {
        ClearTexturesPlace();
        _editingPref.gameObject.SetActive(false);
        _editingPref = _editingPrefs[_currentEditingPrefId];
        _editingPref.gameObject.SetActive(true);
        _currentClothTypeid = 0;
        _currentSeasonId = 0;
        InitializeStartValues();
    }

    public void NextShape()
    {
        _currentEditingPrefId++;
        if (_currentEditingPrefId == _editingPrefs.Count)
            _currentEditingPrefId = 0;
        ChangeShape();
    }

    public void PreviousShape()
    {
        _currentEditingPrefId--;
        if (_currentEditingPrefId < 0)
            _currentEditingPrefId = _editingPrefs.Count - 1;
        ChangeShape();
    }

    private float GetMiddleCalculatedRating()
    {
        float res = (_qualityRating + _visualRating + _actualityRating) / 3;
        if (res % 1 != 0)
            res = (int)res + 0.5f;
        return res;
    }

    //Переписати логіку в майбутньому
    private float GetCalculatedTimeForCreating()
    {
        float res = GetMiddleCalculatedRating() * 2;
        return res;
    }

    private int GetCalculatedExperience()
    {
        int middleCalculatedRating = (int)GetMiddleCalculatedRating();
        if (middleCalculatedRating == 0)
            middleCalculatedRating = 1;
        int res = middleCalculatedRating * 3;
        return res;
    }

    private Sprite GetGeneratedAvatar(int staffId, MaterialTexture camo)
    {
        foreach(var avatar in _avatars)
        {
            if (avatar.Camo.Id == camo.Id && avatar.StaffId == _editingPref.Shape.ShapeId)
                return avatar.Image;
        }
        Debug.LogError("Can't find image by data: staffId = " + _editingPref.Shape.ShapeId + " camoId = " + camo.Id);
        return null;
    }

    public void CreateItem()
    {
        if(_itemName != "")
        {
            CreatingStaff staff = new CreatingStaff(
                CurrentStaffId,
                1,
                _itemName,
                _price,
                GetCalculatedTimeForCreating(),
                GetCalculatedExperience(),
                GetMiddleCalculatedRating(),
                _editingPref.Shape.ShapeId,
                _currentCamo,
                _editingPref.ClothType[_currentClothTypeid],
                _editingPref.Seasons[_currentSeasonId],
                GetGeneratedAvatar(CurrentStaffId, _currentCamo)
                );
            StaffGeneralList.singleton.AddItem(staff);
            CurrentStaffId++;
            GlobalEvents.StaffWasAdded?.Invoke();
            SceneManager.LoadScene(0);
        }
        
    }
}
