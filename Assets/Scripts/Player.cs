using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public event EventHandler OnPickedSomething;

    //property oldugundan pascal case kullanilmistir 
    public static Player Instance { get; private set; }

    //singleton pattern'de 1 tane instance olabilir so if ile birden fazla instance varsa error verilmesini saglıyoruz
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }


    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction; // listener ekledik 
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        //eğer oyun gamePlaying state'inde değilse pickup drop vb. yasak
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        //eğer oyun gamePlaying state'inde değilse pickup drop vb. yasak
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter !=  null)
        {
            selectedCounter.Interact(this);
        }
    }


    private  void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //objeye çarpmışsa ve o şekilde kalıyorsa move olmadıgından dolayı çarpma olarak algılanmaz, bunu düzenlemek için 
        //last intercat move direction'u tutmamız gerekir 
        //player objeye çarpmışsa ve hareket etmiyorsa lastInteract kullanılarak bu durum algılanır
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir; 
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if(raycastHit.collider.TryGetComponent(out BaseCounter baseCounter)) 
            {
                
                //Has clear counter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }else
            {
                SetSelectedCounter(null); // player bir gameobject önündedir ancak clearcounter degildir(hit)
            }

        }else
        {
            SetSelectedCounter(null); // player gameobject önünde degildir(not hit)
        }

        Debug.Log(selectedCounter);

    }

    //CapsuleCast diagonal movement icin kullanıyoruz 
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance); //Raycast fonksiyonu  3 boyutlu uzayda bir ışın (ray) göndererek bu ışının bir nesneyle çarpışıp çarpışmadığını kontrol etmek için kullanılır
        // bool canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance); rotation islemi icin 
        if (!canMove)
        {
            //can not move towards moveDir
            //attempt only X movement 
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            // bool canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance); rotation islemi icin
            if (canMove)
            {
                //can move only on the X 
                moveDir = moveDirX;
            }
            else
            {
                //can not move only on the X
                //attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move only on the Z 
                    moveDir = moveDirZ;
                }
                else
                {
                    //can not move in any direction
                }
            }
        }


        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed; //frame rate'e gore hızın degismemesi icin 
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }
  
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }


    //player pickup yaptıgında devreye girecek olan ses efekti için kullanılabilir
    //player pickup yaptığında bu fonksiyon devreye giriyor
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }


    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }










}

