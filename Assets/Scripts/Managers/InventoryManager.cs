using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviourSingleton<InventoryManager>
{
    [SerializeField] private ClotheTemplate _clothePrefab;
    private List<Clothe> _clothes = new();
    private List<ClotheTemplate> _currentClothes = new();

    protected void Start()
    {
        EventManager.Instance.Subscribe(NameEvent.OnClotheBought, OnClotheBought);
        EventManager.Instance.Subscribe(NameEvent.OnClotheSold, OnClotheSold);
    }
    
    public bool IsInInventory(Clothe clothe)
    {
        return _clothes.Contains(clothe);
    }

    private void OnClotheBought(params object[] parameters)
    {
        var newClothe = (Clothe) parameters[0];
        CreateTemplate(newClothe);
    }
    
    private void OnClotheSold(params object[] parameters)
    {
        var clothe = (Clothe) parameters[0];
        var index = _clothes.IndexOf(clothe);
        Destroy(_currentClothes[index].gameObject);
        _currentClothes.RemoveAt(index);
        _clothes.Remove(clothe);
    }

    private void CreateTemplate(Clothe newClothe)
    {
        var parent = UIManager.Instance.GetParent(true);
        var template = Instantiate(_clothePrefab, parent);
        _clothes.Add(newClothe);
        _currentClothes.Add(template);
        template.SetClothe(newClothe);
    }
}
