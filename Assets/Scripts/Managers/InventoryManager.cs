using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviourSingleton<InventoryManager>
{
    [SerializeField] private ClotheTemplate _clothePrefab;
    [SerializeField] private List<Clothe> _defaultClothe;
    [SerializeField] private ScrollRect _clotheScrollRect;

    private List<Clothe> _clothes = new();
    private List<ClotheTemplate> _currentClothes = new();
    private ClotheType _currentType;
    private bool _isInventoryOpen;
    public bool IsInventoryOpen => _isInventoryOpen;

    #region MonoBehaviour Functions

    protected void Start()
    {
        EventManager.Instance.Subscribe(NameEvent.OnClotheBought, OnClotheBought);
        EventManager.Instance.Subscribe(NameEvent.OnClotheSold, OnClotheSold);
        EventManager.Instance.Subscribe(NameEvent.OnClotheTypeChanged, OnClotheTypeChanged);
        EventManager.Instance.Subscribe(NameEvent.OnInventoryOpened, OnInventoryOpened);
        EventManager.Instance.Subscribe(NameEvent.OnInventoryClosed, OnInventoryClosed);
        
        _currentType = UIManager.Instance.CurrentType;

        _clothes = _defaultClothe;
        
    }

    #endregion

    #region Private Functions

    private void CreateTemplate(Clothe newClothe)
    {
        if (newClothe.cantTrade)
        {
            return;
        }
        
        var template = Instantiate(_clothePrefab, _clotheScrollRect.content);
        _currentClothes.Add(template);
        template.SetClothe(newClothe);
    }

    private void ClearList()
    {
        foreach (var clothe in _currentClothes)
        {
            Destroy(clothe.gameObject);
        }
        
        _currentClothes.Clear();
    }

    #endregion

    #region Public Functions

    public bool IsInInventory(Clothe clothe)
    {
        return _clothes.Contains(clothe);
    }

    public List<Clothe> GetClotheList()
    {
        return _clothes;
    }
    
    public List<Clothe> GetDefaultClotheList()
    {
        return _defaultClothe;
    }

    #endregion

    #region Events Functions
    
    private void OnClotheBought(params object[] parameters)
    {
        var newClothe = (Clothe) parameters[0];
        _clothes.Add(newClothe);
        CreateTemplate(newClothe);
    }
    
    private void OnClotheSold(params object[] parameters)
    {
        var clothe = (Clothe) parameters[0];
        //This is to get the index of the clothe in the list without taking into account defaults
        var index = _clothes.IndexOf(clothe) - 16;
        Destroy(_currentClothes[index].gameObject);
        _currentClothes.RemoveAt(index);
        _clothes.Remove(clothe);
    }
    
    private void OnClotheTypeChanged(params object[] parameters)
    {
        _currentType = (ClotheType) parameters[0];
        ClearList();
        foreach (var clothe in _clothes)
        {
            if(clothe.clotheType == _currentType)
            {
                CreateTemplate(clothe);
            }
        }
    }
    
    private void OnInventoryOpened(params object[] parameters)
    {
        _isInventoryOpen = true;
    }
    
    private void OnInventoryClosed(params object[] parameters)
    {
        _isInventoryOpen = false;
    }
    
    #endregion
}
