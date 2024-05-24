using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    //bircok tane trash bin'i desteklemek için static olarak event handler oluþturdum
    public static event EventHandler OnAnyObjectTrashed;



    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
       
    }
}
