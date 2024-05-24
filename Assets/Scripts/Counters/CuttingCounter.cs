using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CuttingCounter : BaseCounter, IHasProgress
{





    public static event EventHandler OnAnyCut;


    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    // OnProgressChangedEventArgs class'ý burada da tanýmlýydý ancak farklý classlarda yer
    //aldýklarý için farklý referanslar olarak geçiyorlardý so IHasProgress diye belirtmeliyiz

    public event EventHandler OnCut;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be cut 
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                }

            }
            else
            {
                //Player is not carrying anything 
                //Do nothing 
            }
        }
        else
        {
            //There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate 

                    //eger burger icerisine o malzeme eklenebiliyorsa ekleriz
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
            else
            {
                //Player is not carrying anything 
                //so player'a KitchenObject verilmelidir
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }

    public override void InteractAlternate(Player  player)
    {   //2. koþulu ekleyerek slice'larýn bir daha slice edilmesini engellemiþ oluyoruz
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //There is a KitchenObject here AND it can be cut 
            //so cut them 
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            
            if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO()); //object'i silmeden once slice'ý alýyoruz 
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
           
        }
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        /*foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
        */
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    //Bread ve meat icin false dönmeli. Bu objectlerin slice'larý yok 
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        /*foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;*/
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
        
        
    }







}
