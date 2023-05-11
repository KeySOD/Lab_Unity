using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private float _coinsCount;
    [Header("References/UI")]
    [SerializeField] private Text _coinsText;
    public float CoinsCount { get => _coinsCount; set => _coinsCount = value; }
    private void Start()
    {
        LoadData();
        RefreshUI();
    }
    public void RefreshUI()
    {
        _coinsText.text = $"Coins: {_coinsCount}";       
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("CoinsData", _coinsCount);
    }
    public void LoadData()
    {
        if(PlayerPrefs.HasKey("CoinsData"))
        {
            _coinsCount = PlayerPrefs.GetFloat("CoinsData");
        }
    }
}
