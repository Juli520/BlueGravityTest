using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    protected void Start()
    {
        _moneyText.text = $"Money: {GameManager.Instance.CurrentMoney}";
        
        EventManager.Instance.Subscribe(NameEvent.OnShopOpened, OnShopOpened);
        EventManager.Instance.Subscribe(NameEvent.OnShopClosed, OnShopClosed);
        EventManager.Instance.Subscribe(NameEvent.OnMoneyUpdated, OnMoneyUpdated);
        EventManager.Instance.Subscribe(NameEvent.OnInventoryOpened, OnInventoryOpened);
    }

    public Transform GetParent(bool isInventory)
    {
        return isInventory ? _inventoryPanel.transform : _shopPanel.transform;
    }

    #region Events Functions
    
    private void OnShopOpened(object[] parameters)
    {
        _shopPanel.SetActive(true);
    }
    
    private void OnShopClosed(object[] parameters)
    {
        _shopPanel.SetActive(false);
    }

    private void OnMoneyUpdated(object[] parameters)
    {
        _moneyText.text = $"Money: {GameManager.Instance.CurrentMoney}";
    }
    
    private void OnInventoryOpened(object[] parameters)
    {
        _inventoryPanel.SetActive(true);
    }
    
    #endregion
}
