using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _outfitChangerPanel;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _missingMoneyText;
    private int _currentTypeIndex;
    private bool _isShopOpen;
    public bool IsShopOpen => _isShopOpen;
    private ClotheType _currentType = ClotheType.Face;
    public ClotheType CurrentType => _currentType;
    
    protected void Start()
    {
        _moneyText.text = $"Money: {GameManager.Instance.CurrentMoney}";
        
        _currentTypeIndex = (int)_currentType;
        
        EventManager.Instance.Subscribe(NameEvent.OnShopOpened, OnShopOpened);
        EventManager.Instance.Subscribe(NameEvent.OnShopClosed, OnShopClosed);
        EventManager.Instance.Subscribe(NameEvent.OnMoneyUpdated, OnMoneyUpdated);
        EventManager.Instance.Subscribe(NameEvent.OnInventoryOpened, OnInventoryOpened);
        EventManager.Instance.Subscribe(NameEvent.OnInventoryClosed, OnInventoryClosed);
    }
    
    public void ChangeMissingMoneyText(bool enable)
    {
        _missingMoneyText.gameObject.SetActive(enable);
    }

    public void NextType()
    {
        _currentTypeIndex++;
        if(_currentTypeIndex >= Enum.GetNames(typeof(ClotheType)).Length)
        {
            _currentTypeIndex = 0;
        }
        
        _currentType = (ClotheType)_currentTypeIndex;
        EventManager.Instance.Trigger(NameEvent.OnClotheTypeChanged, _currentType);
    }
    
    public void PreviousType()
    {
        _currentTypeIndex--;
        if(_currentTypeIndex <= 0)
        {
            _currentTypeIndex = Enum.GetNames(typeof(ClotheType)).Length - 1;
        }
        
        _currentType = (ClotheType)_currentTypeIndex;
        EventManager.Instance.Trigger(NameEvent.OnClotheTypeChanged, _currentType);
    }

    #region Events Functions
    
    private void OnShopOpened(params object[] parameters)
    {
        if(InventoryManager.Instance.IsInventoryOpen)
        {
            EventManager.Instance.Trigger(NameEvent.OnInventoryClosed);
        }
        _shopPanel.SetActive(true);
        _inventoryPanel.SetActive(true);
        _isShopOpen = true;
    }
    
    private void OnShopClosed(params object[] parameters)
    {
        _shopPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        _isShopOpen = false;
    }

    private void OnMoneyUpdated(params object[] parameters)
    {
        _moneyText.text = $"Money: {GameManager.Instance.CurrentMoney}";
    }
    
    private void OnInventoryOpened(params object[] parameters)
    {
        if(_isShopOpen)
        {
            EventManager.Instance.Trigger(NameEvent.OnShopClosed);
        }
        _outfitChangerPanel.SetActive(true);
    }

    private void OnInventoryClosed(params object[] parameters)
    {
        _outfitChangerPanel.SetActive(false);
    }
    
    #endregion
}
