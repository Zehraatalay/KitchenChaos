using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //if the player interacts and spawns an object and gives it to the player  
    public override void Interact(Player player)
    {
        /*Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            counterTopPoint SetKitchenObjectParent() metodu ile zaten kontrol ediliyor so bu prametreye gerek yok 
        */
        //player-object etkilesimi ilk kez ise spawn 
        //Player'da KitchenObject yoksa KitchenObject ver 
        if (!player.HasKitchenObject())
        {
            //Player is not carrying anything 
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }



    }


}
