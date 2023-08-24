using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClotheTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _clotheImage;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _buyButton;
    private Clothe _clothe;

    private void Start()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
        _sellButton.onClick.AddListener(OnSellButtonClicked);
    }

    public void SetClothe(Clothe newClothe)
    {
        _clothe = newClothe;
        
        if(InventoryManager.Instance.IsInInventory(_clothe))
        {
            _sellButton.gameObject.SetActive(true);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
        }
        
        SetUI();
    }

    private void SetUI()
    {
        if(_clothe.name != "")
        {
            _nameText.text = _clothe.name;
        }
        
        if(_clothe.cost != 0)
        {
            _costText.text = InventoryManager.Instance.IsInInventory(_clothe)
                ? (_clothe.cost / 2).ToString()
                : _clothe.cost.ToString();
        }
        
        if(_clothe.clotheSprite != null)
        {
            _clotheImage.sprite = _clothe.clotheSprite;
        }
    }
    
    private void OnBuyButtonClicked()
    {
        var playerMoney = GameManager.Instance.CurrentMoney;
        if (playerMoney >= _clothe.cost)
        {
            GameManager.Instance.RemoveMoney(_clothe.cost);
            EventManager.Instance.Trigger(NameEvent.OnClotheBought, _clothe);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    
    private void OnSellButtonClicked()
    {
        GameManager.Instance.AddMoney(_clothe.cost / 2);
        EventManager.Instance.Trigger(NameEvent.OnClotheSold, _clothe);
    }
}