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
    private Animator _playerAnimator;
    
    #endregion

    #region Monobehaviour Functions

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Private Functions

    private void Move()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        
        var movementDirection = new Vector2(_horizontalMovement, _verticalMovement);

        if(movementDirection != Vector2.zero)
        {
            _playerAnimator.SetBool("isWalking", true);
            transform.eulerAngles = new Vector3 (0, _horizontalMovement < 0 ? 180 : 0, 0);
        }
        else
        {
            _playerAnimator.SetBool("isWalking", false);
        }
        
        _rb.velocity = movementDirection * _speed;
    }

    private void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
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
                if(!UIManager.Instance.IsShopOpen)
                {
                    EventManager.Instance.Trigger(NameEvent.OnShopOpened);
                }
                break;
            }
        }
    }

    private void Inventory()
    {
        EventManager.Instance.Trigger(!InventoryManager.Instance.IsInventoryOpen
            ? NameEvent.OnInventoryOpened
            : NameEvent.OnInventoryClosed);
    }

    #endregion
}
