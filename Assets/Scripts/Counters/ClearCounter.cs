using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if(player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else
            {
                //Player is not carrying anything 
                //Do nothing 
            }
        }else
        {
            //There is a KitchenObject here
            if(player.HasKitchenObject())
            {
                //Player is carrying something
                //so do nothing hence counter üzerinde 2 object olamaz(player elindeki + counter üzeirndeki)
                //ama plate üzerinde diger objectler olabilir 

                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate 
          
                    //eger burger icerisine o malzeme eklenebiliyorsa ekleriz
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                  
                    /*
                     * BURADA PLATE PLAYER'DA CONTAÝNER ÜZERÝNDE ÝNGREDÝENT VAR EGER ÝNGREDÝENT PLATE'E 
                     * EKLENEBÝLÝYORSA EKLÝYORUZ 
                     */

                }else
                {
                    //player is not carrying plate but something else 
                    if(GetKitchenObject().TryGetPlate(out  plateKitchenObject)) //counter check ediliyor 
                    {
                        //counter is holding a plate 
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }


                     /*
                     * BURADA PLATE CONTAÝNER'DE  PLAYER ÜZERÝNDE ÝNGREDÝENT VAR EGER ÝNGREDÝENT PLATE'E 
                     * EKLENEBÝLÝYORSA EKLÝYORUZ 
                     */
                    }
                }

            }else
            {
                //Player is not carrying anything 
                //so player'a KitchenObject verilmelidir
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
