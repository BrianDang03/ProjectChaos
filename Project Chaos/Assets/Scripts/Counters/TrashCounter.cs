using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is not Kitchen Object Here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestorySelf();

                OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                //Player has Nothing
            }
        }
    }
}

