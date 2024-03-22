using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] float spawnPlateTimerMax = 4f;
    [SerializeField] private int platesSpawnedAmountMax = 4;

    private int platesSpawnedAmount;
    private float spawnPlateTimer;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax)
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
            //If Player is Empty Handed
            if (platesSpawnedAmount > 0)
            {
                //If there there is at least one plate
                platesSpawnedAmount--;

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            }
        }
    }
}
