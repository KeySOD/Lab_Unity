using UnityEngine;
using UnityEngine.UI;
public class SkinCell : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    private Skin _skin;
    private Shop _shop;
    public Skin SkinItem { get => _skin; set => _skin = value; }
    public Button ButtonSkin { get => _button; set => _button = value; }
    public Shop ShopReference { get => _shop; set => _shop = value; }

    public void RefreshUI()
    {
        if (_icon.sprite)
        {
            _icon.sprite = _skin.sprite;
        }
        else _icon.sprite = null;
    }
    public void ChooseSkin()
    {
        _shop.ChooseSkin(_skin);
    }
}
