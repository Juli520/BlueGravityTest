using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothesShop : MonoBehaviour
{
    [SerializeField] private List<Clothe> _clothes = new();
    [SerializeField] private ClotheTemplate _clothePrefab;
    [SerializeField] private Transform _shopScrollRect;
    [SerializeField] private GameObject _sign;
    private List<ClotheTemplate> _currentTemplateClothes = new();
    private ClotheType _currentType;

    #region Monobehaviour Functions
    
    private void Start()
    {
        EventManager.Instance.Subscribe(NameEvent.OnShopOpened, OnShopOpened);
        EventManager.Instance.Subscribe(NameEvent.OnShopClosed, OnShopClosed);
        EventManager.Instance.Subscribe(NameEvent.OnClotheBought, OnClothBought);
        EventManager.Instance.Subscribe(NameEvent.OnClotheSold, OnClothSold);
        EventManager.Instance.Subscribe(NameEvent.OnClotheTypeChanged, OnClotheTypeChanged);
        _currentType = UIManager.Instance.CurrentType;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            _sign.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _sign.SetActive(false);
            EventManager.Instance.Trigger(NameEvent.OnShopClosed);
        }
    }

    #endregion

    #region Private Functions

    //This is a coroutine to wait until the inventory removes the clothe from the list
    private IEnumerator CreateTemplate(Clothe newClothe)
    {
        yield return new WaitForSeconds(.1f);

        var template = Instantiate(_clothePrefab, _shopScrollRect);
        template.SetClothe(newClothe);
        _currentTemplateClothes.Add(template);
    }

    private void ClearList()
    {
        foreach (var clothe in _currentTemplateClothes)
        {
            Destroy(clothe.gameObject);
        }
        
        _currentTemplateClothes.Clear();
    }

    #endregion

    #region Events Functions
    
    private void OnShopOpened(params object[] parameters)
    {
        foreach (var clothe in _clothes.Where(clothe => clothe.clotheType == _currentType))
        {
            StartCoroutine(CreateTemplate(clothe));
        }
    }

    private void OnShopClosed(params object[] parameters)
    {
        ClearList();
    }
    
    private void OnClothBought(params object[] parameters)
    {
        var clothe = (Clothe) parameters[0];
        var index = _clothes.IndexOf(clothe);
        var templateIndex = index % _currentTemplateClothes.Count;
        Destroy(_currentTemplateClothes[templateIndex].gameObject);
        _currentTemplateClothes.RemoveAt(templateIndex);
        _clothes.Remove(clothe);
    }
    
    private void OnClothSold(params object[] parameters)
    {
        var clothe = (Clothe) parameters[0];
        _clothes.Add(clothe);
        StartCoroutine(CreateTemplate(clothe));
    }

    private void OnClotheTypeChanged(params object[] parameters)
    {
        _currentType = (ClotheType) parameters[0];
        ClearList();
        foreach (var clothe in _clothes.Where(clothe => clothe.clotheType == _currentType))
        {
            StartCoroutine(CreateTemplate(clothe));
        }
    }
    
    #endregion
}
