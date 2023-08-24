using UnityEngine;

public class CheckCurrentClothe : MonoBehaviour
{
    [SerializeField] private ClotheType _clotheType;
    private SpriteRenderer _currentClothe;

    private void Awake()
    {
        _currentClothe = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        EventManager.Instance.Subscribe(NameEvent.OnClotheSold, OnClotheSold);
    }
    
    private void OnClotheSold(params object[] parameter)
    {
        var clothe = (Clothe) parameter[0];
        
        if(clothe.clotheType == _clotheType)
        {
            if(clothe.clotheSprite == _currentClothe.sprite)
            {
                var clotheList = InventoryManager.Instance.GetDefaultClotheList();
                _currentClothe.sprite = clotheList.Find(c => c.clotheType == _clotheType).clotheSprite;
            }
        }
    }
}