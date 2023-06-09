#nullable enable
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player? Instance { get; private set; }
    public event Action<ClearCounter?> OnSelectedCounterChanged;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactionLayerMask;

    private bool _isWalking;
    private Vector3 _lastInteractDirection;
    private ClearCounter? _selectedCounter;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More then one Player instance");
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnOnInteractAction;
    }

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if(_selectedCounter != null)_selectedCounter.Interact();
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            _lastInteractDirection = moveDirection;
        }

        bool isRaycastHit = Physics.Raycast(transform.position, _lastInteractDirection, 
            out RaycastHit raycastHit, interactDistance,interactionLayerMask);
        
        if (isRaycastHit && raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        {
            if (clearCounter != _selectedCounter)
            {
                SetSelectedCounter(clearCounter);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(ClearCounter? selectedCounter)
    {
        _selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(_selectedCounter);
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        _isWalking = inputVector != Vector2.zero;

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        var canMove = TryMoveInDirection(playerHeight, playerRadius, moveDistance, ref moveDirection);

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }


        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    private bool TryMoveInDirection(float playerHeight, float playerRadius, float moveDistance,
        ref Vector3 moveDirection)
    {
        bool canMove = !Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDirection,
            moveDistance);

        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            if (!Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirectionX,
                    moveDistance))
            {
                moveDirection = moveDirectionX;
                return true;
            }

            Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;

            if (!Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirectionZ,
                    moveDistance))
            {
                moveDirection = moveDirectionZ;
                return true;
            }
        }

        return canMove;
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}