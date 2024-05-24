using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
    //Player normalde 1 tane kitchen object taþýyabiliyordu ancak bircok tane plate taþýyabilmeli 

    public event EventHandler OnPlateSpawned; //tabak sayýsýný artýrmak için kullanýlan event
    public event EventHandler OnPlateRemoved; //tabak sayýsýný azaltmak icin kullanýlan event 


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if(platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
              
            }

        }


    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty handed
            if(platesSpawnedAmount > 0)
            {
                //there is at least one plate here 
                platesSpawnedAmount--;


                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                    
            }
        }
    }



}
