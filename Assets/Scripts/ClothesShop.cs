using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesShop : MonoBehaviour
{
    [SerializeField] private List<Clothe> _clothes = new();
    [SerializeField] private ClotheTemplate _clothePrefab;
    private List<ClotheTemplate> _currentClothes = new();

    private void Start()
    {
        EventManager.Instance.Subscribe(NameEvent.OnShopOpened, OnShopOpened);
        EventManager.Instance.Subscribe(NameEvent.OnShopClosed, OnShopClosed);
        EventManager.Instance.Subscribe(NameEvent.OnClotheBought, OnClothBought);
        EventManager.Instance.Subscribe(NameEvent.OnClotheSold, OnClothSold);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.Trigger(NameEvent.OnShopClosed);
        }
    }
    
    //This is a coroutine to wait until the inventory removes the clothe from the list
    private IEnumerator CreateTemplate(Clothe newClothe)
    {
        yield return new WaitForSeconds(.1f);
        
        var parent = UIManager.Instance.GetParent(false);
        var template = Instantiate(_clothePrefab, parent);
        template.SetClothe(newClothe);
        _currentClothes.Add(template);
        
    }

    #region Events Functions
    
    private void OnShopOpened(object[] parameters)
    {
        foreach (var clothe in _clothes)
        {
            StartCoroutine(CreateTemplate(clothe));
        }
    }

    private void OnShopClosed(object[] parameters)
    {
        foreach (var clothe in _currentClothes)
        {
            Destroy(clothe.gameObject);
        }
        
        _currentClothes.Clear();
    }
    
    private void OnClothBought(object[] parameters)
    {
        var clothe = (Clothe) parameters[0];
        var index = _clothes.IndexOf(clothe);
        Destroy(_currentClothes[index].gameObject);
        _currentClothes.RemoveAt(index);
        _clothes.Remove(clothe);
    }
    
    private void OnClothSold(object[] parameters)
    {
        var clothe = (Clothe) parameters[0];
        _clothes.Add(clothe);
        StartCoroutine(CreateTemplate(clothe));
    }
    
    #endregion
}
