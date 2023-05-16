using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Skin> _skinsList;
    [SerializeField] private GameObject _skinCellExample;
    [SerializeField] private Wallet _wallet;

    [Header("References/UI")]
    [SerializeField] private Transform _skinsContent;
    [SerializeField] private Text _costText;

    [Header("References/UI/BuyEquipButton")]
    [SerializeField] private Button _button;
    [SerializeField] private Text _buttonText;

    [Header("References/Audio")]
    [SerializeField] private Audio_Manager _audioManager;
    [SerializeField] private AudioClip _skinChooseSound;
    
    [Header("References/Cube")]
    [SerializeField] private Material _mainCubeMaterial;
    [SerializeField] private Material _cubeShowcaseMaterial;
    private List<GameObject> _createdSkinCells = new List<GameObject>();
    private Skin _lastChoosedSkin;
    
    private void Start()
    {
        ResetCubeInfo();
        RefreshSkinsList();
    }
    public void RefreshSkinsList()
    {
        if (_skinsList.Count <= 0) return;
        foreach (GameObject cell in _createdSkinCells)
        {
            Destroy(cell);
        }
        foreach (Skin skin in _skinsList)
        {
            GameObject skinCellObj = Instantiate(_skinCellExample, _skinsContent);
            skinCellObj.TryGetComponent<SkinCell>(out var skinCell);
            skinCell.SkinItem = skin;
            skinCell.ShopReference = this;
            skinCell.ButtonSkin.onClick.AddListener(() => { var param = skin; ChooseSkin(param); });
            skinCell.RefreshUI();
            _createdSkinCells.Add(skinCellObj);
        }
    }
    public void ChooseSkin(Skin skin)
    {
        _button.gameObject.SetActive(true);
        if(skin.sprite)
        {
            _cubeShowcaseMaterial.SetTexture("_MainTex", skin.sprite.texture);
        }
        else _cubeShowcaseMaterial.SetTexture("_MainTex", null);
        _costText.text = $"Cost: {skin.cost}";
        _buttonText.text = "Buy";
        if (_skinChooseSound)
        {
            _audioManager.PlaySound(_skinChooseSound);
        }           
        _lastChoosedSkin = skin;
        if(skin.purchased)
        {
            SetPurchased();
        }
        else
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => { var param = _lastChoosedSkin; BuySkin(param); });
        }
    }
    public void SetPurchased()
    {
        if(_lastChoosedSkin)
        {
            _costText.text = "Purchased";
            _buttonText.text = "Equip";
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => { var param = _lastChoosedSkin; EquipSkin(param); });
        }
    }
    public void EquipSkin(Skin skin)
    {
        if(skin.sprite)
        {
            _mainCubeMaterial.SetTexture("_MainTex", skin.sprite.texture);
        }
        else _mainCubeMaterial.SetTexture("_MainTex", null);
    }
    public void BuySkin(Skin skin)
    {
        if(_wallet.CoinsCount >= skin.cost)
        {
            skin.purchased = true;
            _wallet.CoinsCount -= skin.cost;
            SetPurchased();
            _wallet.RefreshUI();
            _wallet.SaveData();
        }
    }
    private void ResetCubeInfo()
    {
        _button.gameObject.SetActive(false);
        _costText.text = "";
        _cubeShowcaseMaterial.SetTexture("_MainTex", null);
    }
}
