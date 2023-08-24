using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Fields
    
    [SerializeField] private float _speed = 5;

    #endregion
    
    #region Private Fields
    
    private float _horizontalMovement;
    private float _verticalMovement;
    private Rigidbody2D _rb;
    
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        
        var movementDirection = new Vector2(_horizontalMovement, _verticalMovement);

        _rb.velocity = movementDirection * _speed;
    }

    private void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            Inventory();
        }
        
        if(Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.AddMoney();
        }
    }

    private void Interact()
    {
        const int distance = 5;
        var t = transform;
        var hits = Physics2D.RaycastAll(t.position, t.right, distance);
        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent<ClothesShop>(out var clothesShop))
            {
                EventManager.Instance.Trigger(NameEvent.OnShopOpened);
                break;
            }
        }
    }

    private void Pause()
    {
        
    }

    private void Inventory()
    {
        EventManager.Instance.Trigger(NameEvent.OnInventoryOpened);
    }
}
