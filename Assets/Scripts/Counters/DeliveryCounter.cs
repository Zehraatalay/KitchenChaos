using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    //delivery efektleri player'a göre uygulandýgýndan çok yüksek ses çýkmýyordu bunu çözmek için
    //delivery counter nesnesini baz almaya karar verdim
    //bir tane delivery counter olduðundan singleton pattern kullandým
    public static DeliveryCounter Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plate

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

                player.GetKitchenObject().DestroySelf();
            }
        }
    }


}
