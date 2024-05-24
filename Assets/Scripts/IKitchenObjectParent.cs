using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//hen player hem de counter kitchen object'e sahip olabilir so interface

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}
