using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    //plate is a kitchen object but biraz farklýlýklar var so inheritance kullanabiliriz

    private List<KitchenObjectSO> kitchenObjectSOList;//tabagýn üzerinde yer alan objectler 

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    //event icin hangi ingredientlerin eklendigini bilmemiz lazým so bu class'ý oluþturdum
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }



    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //not a valid ingredient
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //already has this type (hamburger icinde ayný object'ten 2 tane olamaz)
            return false; 
        }else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            //ingredient ekleneceðinde event invoke edilir
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }

}
