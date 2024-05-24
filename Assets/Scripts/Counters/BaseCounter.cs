using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    //drop islemi sadece counter'larda yapýldýgý için drop ses efekti icin bu classý kullanýyoruz

    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }


    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject; //protected yaptýgýmda subclass'lardan direkt olarak eriþilebilir


    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        //tüm counterlar bu metodu override etmek zorunda olmadýgý icin yorum satýrýna aldým 
        //Debug.Log("BaseCounter.InteractAlternate()");
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
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
