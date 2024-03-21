# Code Name: ProjectChaos
---
## Table of Contents

### 1. Scripts
   - Counters
     - [BaseCounter.cs](README.md#1-basecountercs)
     - CLearConter.cs
     - ContainerCounter.cs
       - ContainerCounterVisual.cs
     - CuttingCoutner.cs
       - CuttingCounterVisual.cs
     - DeliveryCounter.cs
     - PlateCounter.cs
       - PlateCounterVisual.cs
     - StoveCounter.cs
       - StoveCounterVisual.cs 
       - StoveCounterSound.cs
     - TrashCounter.cs
   - ScriptableObjects
     - AudioClipRefsSO.cs
     - BurningRecipeSO.cs
     - CuttingRecipeSO.cs
     - FryingRecipeSO.cs
     - KitchekObjectSO.cs
     - RecipeListSO.cs
     - RecipeSO.cs
   -  UI
      - DeliveryManagerSingleUI.cs
      - DeliveryMangaerUI.cs
      - GameOverUI.cs
      - GamePauseUI.cs
      - GamePlayingClockUI.cs
      - GameStartCountdownUI.cs
      - MainMenuUI.cs
      - OptionsUI.cs
      - PlateIconsSingleUI.cs
      - PlateIconUI.cs
      - ProgressBarUI.cs
---
## Counters
---
### 1. BaseCounter.cs

#### Description
This class represents a base counter object in a kitchen environment in a game. It implements functionality related to interactions with kitchen objects.

#### Inherits from
- `MonoBehaviour`
- `IKitchenObjectParent`

#### Fields
- `public static event EventHandler OnAnyObjectPlacedHere`: Static event triggered when any object is placed on the counter.
- `[SerializeField] private Transform counterTopPoint`: The transform representing the top point of the counter.

#### Methods
- `public static void ResetStaticData()`: Resets static data, particularly clears the event handler for object placement.
- `public virtual void Interact(Player player)`: Virtual method for interacting with the counter. Currently logs an error message.
- `public virtual void InteractAlternate(Player player)`: Virtual method for alternate interaction with the counter (currently commented out).
- `public Transform GetKitchenObjectFollowTransform()`: Returns the transform representing the counter's top point.
- `public void SetKitchenObject(KitchenObject kitchenObject)`: Sets the kitchen object placed on the counter and invokes the OnAnyObjectPlacedHere event if a kitchen object is placed.
- `public KitchenObject GetKitchenObject()`: Returns the kitchen object placed on the counter.
- `public void ClearKitchenObject()`: Clears the kitchen object placed on the counter.
- `public bool HasKitchenObject()`: Returns a boolean indicating whether the counter has a kitchen object placed on it.

#### Events
- `public static event EventHandler OnAnyObjectPlacedHere`: Static event triggered when any object is placed on the counter.

#### Interfaces
- `IKitchenObjectParent`: This class implements the IKitchenObjectParent interface.

### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate()");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

```
---
### 2. ClearCounter.cs

#### Description
This class represents a specific type of counter object. It inherits functionality from the `BaseCounter` class and implements custom interaction behavior.

#### Inherits from
- `BaseCounter`

#### Fields
- `[SerializeField] private KitchenObjectSO kitchenObjectSO`: Serialized field representing the scriptable object for kitchen objects associated with this counter.

#### Methods
- `public override void Interact(Player player)`: Overrides the base class method to implement custom interaction behavior when a player interacts with the counter.
    - If there is no kitchen object on the counter:
        - If the player is carrying a kitchen object, it sets the counter as the parent for that object.
        - Otherwise, it does nothing.
    - If there is a kitchen object on the counter:
        - If the player is carrying a kitchen object:
            - If the player is carrying a plate, it tries to add ingredients from the counter's kitchen object to the plate. If successful, it destroys the counter's kitchen object.
            - If the counter's kitchen object is a plate, it tries to add ingredients from the player's kitchen object to the plate. If successful, it destroys the player's kitchen object.
        - If the player is not carrying a kitchen object, it sets the player as the parent for the counter's kitchen object.

#### Usage
This class is used to define the behavior of a clear counter. It handles interactions between players and kitchen objects placed on the counter.

#### Notes
- This class extends functionality from the `BaseCounter` class, providing additional behavior specific to clearing counters in the game.
- Interaction logic within the `Interact` method determines how players and kitchen objects interact with the counter based on their current state.

#### Code
```
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
            //There is not Kitchen Object Here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player has Nothing
            }
        }
        else
        {
            //There is a Kitchen Object Here
            if (player.HasKitchenObject())
            {
                //Playaer is Carrying Something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is Holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestorySelf();
                    }
                }
                else if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                {
                    //Counter has Plate
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestorySelf();
                    }
                }
            }
            else
            {
                //Player is not Carrying Anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

```
---
### 3. ContainerCounter.cs

#### Description
This class represents a specific type of counter object in a kitchen environment in a game. It inherits functionality from the `BaseCounter` class and implements custom interaction behavior.

#### Inherits from
- `BaseCounter`

#### Events
- `public event EventHandler OnPlayerGrabObject`: Event triggered when a player grabs an object from the counter.

#### Fields
- `[SerializeField] private KitchenObjectSO kitchenObjectSO`: Serialized field representing the scriptable object for kitchen objects associated with this counter.

#### Methods
- `public override void Interact(Player player)`: Overrides the base class method to implement custom interaction behavior when a player interacts with the counter.
    - If there is no kitchen object on the counter:
        - If the player is carrying a kitchen object, it sets the counter as the parent for that object.
        - Otherwise, it does nothing.
    - If there is a kitchen object on the counter:
        - If the player is carrying a kitchen object:
            - If the player is carrying a plate, it tries to add ingredients from the counter's kitchen object to the plate. If successful, it destroys the counter's kitchen object.
            - If the counter's kitchen object is a plate, it tries to add ingredients from the player's kitchen object to the plate. If successful, it destroys the player's kitchen object.
        - If the player is not carrying a kitchen object, it sets the player as the parent for the counter's kitchen object.
    - If neither the player nor the counter has a kitchen object:
        - Spawns a new kitchen object associated with this counter's `kitchenObjectSO` scriptable object and assigns it to the player.
        - Invokes the `OnPlayerGrabObject` event.

#### Usage
This class is used to define the behavior of a specific type of counter in the game environment. It handles interactions between players and kitchen objects placed on the counter, as well as spawning new kitchen objects when both the player and the counter are empty.

#### Notes
- This class extends functionality from the `BaseCounter` class, providing additional behavior specific to counters that act as containers in the game.
- Interaction logic within the `Interact` method determines how players and kitchen objects interact with the counter based on their current state, and spawns a new kitchen object when both the player and the counter are empty.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is not Kitchen Object Here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player has Nothing
            }
        }
        else
        {
            //There is a Kitchen Object Here
            if (player.HasKitchenObject())
            {
                //Playaer is Carrying Something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is Holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestorySelf();
                    }
                }
                else if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                {
                    //Counter has Plate
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestorySelf();
                    }
                }
            }
            else
            {
                //Player is not Carrying Anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

        if (!player.HasKitchenObject() && !HasKitchenObject())
        {
            //Playaer is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }
}

```





