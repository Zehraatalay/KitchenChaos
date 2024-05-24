using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class responsible for single templete 
//PlateIconsUI classý ise spawn ve give the icon isleminden sorumlu 
public class PlateIconsSingleUI : MonoBehaviour
{

    [SerializeField] private Image image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
