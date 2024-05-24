using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        // Interact_performed fonksiyonunu direkt pass ediyoruz
        // bu action trigger edildigi zaman kontrol edilmeli(butona týklanýldýgý zaman gibi gibi)

        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
    }

    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }



    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /*if ( OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
            //whoever sending this event = this (parameter)
         }
        */
        OnInteractAction?.Invoke(this, EventArgs.Empty);
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); //ReadValue<Vector2> ifadesi Move eyleminin degerini Vector2 olarak return eder 

        //and then move the object 
        inputVector = inputVector.normalized;  //hýzýn her zaman ayný kalmasý için 
        return inputVector;
    }


}

