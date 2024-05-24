using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;


    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    } 


    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent; 
    } 

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null) //eski parent'i sil 
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent; //new clearCounter'ý olustur
        
        if(kitchenObjectParent.HasKitchenObject())
        {
            //Bu satýra girmemesi lazým 
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }
        
        kitchenObjectParent.SetKitchenObject(this); //new clearCounter'ý assign et 

        //and update the visual
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        //parent object clear edilir 
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); //instantiate slices 
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>(); 
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }else
        {
            plateKitchenObject = null;
            return false;
        }

    }

}
