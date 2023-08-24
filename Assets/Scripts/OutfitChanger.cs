using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OutfitChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _bodyPart;
    [SerializeField] private ClotheType _clotheType;
    private GameObject _container;
    private Image _newClotheImage;
    private List<Sprite> _bodyOptions = new();
    private int _currentOption;

    #region MonoBehaviour Functions

    private void Awake()
    {
        _container = transform.GetChild(0).gameObject;
        _newClotheImage = _container.transform.GetChild(1).GetComponent<Image>();
    }

    private void OnEnable()
    {
        var clotheList = InventoryManager.Instance.GetClotheList();

        foreach (var clothe in clotheList.Where(clothe => clothe.clotheType == _clotheType))
        {
            _bodyOptions.Add(clothe.clotheSprite);
        }

        if (_bodyOptions.Count == 0)
        {
            _container.SetActive(false);
            return;
        }

        _container.SetActive(true);
        
        _newClotheImage.sprite = _bodyOptions[_currentOption];
    }
    
    private void OnDisable()
    {
        _bodyOptions.Clear();
    }
    
    #endregion

    #region Public Functions

    public void NextOption()
    {
        _currentOption++;
        if (_currentOption >= _bodyOptions.Count)
        {
            _currentOption = 0;
        }
        
        _newClotheImage.sprite = _bodyOptions[_currentOption];
        _bodyPart.sprite = _bodyOptions[_currentOption];
    }
    
    public void PreviousOption()
    {
        _currentOption--;
        if (_currentOption < 0)
        {
            _currentOption = _bodyOptions.Count - 1;
        }
        
        _bodyPart.sprite = _bodyOptions[_currentOption];
    }

    #endregion
}