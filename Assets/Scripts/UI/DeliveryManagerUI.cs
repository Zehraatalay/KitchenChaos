using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{


    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplete;

    private void Awake()
    {
        recipeTemplete.gameObject.SetActive(false);
    }


    public void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        UpdateVisual(); //sure that do not show up the previous one
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    //sadece degisiklik oldugunda bu metodu çaðýrmalýyýz(her frame'de güncellemek yerine) 
    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if(child == recipeTemplete) continue;
            Destroy(child.gameObject);
        }
        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplete, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
            //recipeTransform.Find("RecipeNameText");
        }
    }


}
