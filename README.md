# Code Name: ProjectChaos
---
## Table of Contents

### 1. Scripts
   - Counters
     - [BaseCounter.cs](README.md#1-basecountercs)
     - [ClearConter.cs](README.md#2-clearcountercs)
     - [ContainerCounter.cs](README.md#3-containercountercs)
       - [ContainerCounterVisual.cs](README.md#3a-containercountervisualcs)
     - [CuttingCounter.cs](README.md#4-cuttingcounter.cs)
       - [CuttingCounterVisual.cs](README.md#4a-cuttingcountervisual.cs)
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
This class represents a base counter object. It implements functionality related to interactions with kitchen objects.

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
This class represents a specific type of counter object. It inherits functionality from the `BaseCounter` class and implements custom interaction behavior. It is an empty counter for placing kitchen objects on.

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
This class is used to define the behavior of a clear counter in the game environment. It handles interactions between players and kitchen objects placed on the counter.

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
This class represents a specific type of counter object. It inherits functionality from the `BaseCounter` class and implements custom interaction behavior. It is responsible for spawning in kitchen objects when the player interacts with it.

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

### 3a. ContainerCounterVisual.cs

#### Description
This class represents the visual component of a container counter object. It listens to events from a `ContainerCounter` instance and triggers animations accordingly.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `private const string OPEN_CLOSE`: Constant string representing the trigger name for the open-close animation.
- `[SerializeField] ContainerCounter containerCounter`: Reference to the `ContainerCounter` instance associated with this visual component.
- `private Animator animator`: Reference to the Animator component attached to the GameObject.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the `animator` field by getting the Animator component attached to the GameObject.
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to the `OnPlayerGrabObject` event of the associated `ContainerCounter` instance.
- `private void ContainerCounter_OnPlayerGrabObject(object sender, EventArgs e)`: Event handler method triggered when a player grabs an object from the associated `ContainerCounter`. Sets the trigger for the open-close animation on the Animator component.

#### Usage
This class is used to manage the visual representation of a container counter object in the game environment. It listens to events triggered by interactions with the associated `ContainerCounter` instance and triggers animations accordingly.

#### Notes
- This class serves as a bridge between the logic of the `ContainerCounter` and its visual representation, allowing for synchronized animations based on gameplay events.
- The `OPEN_CLOSE` constant string represents the name of the trigger parameter in the Animator controller used to control the open-close animation.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
    }
    private void ContainerCounter_OnPlayerGrabObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
```

---
### 4. CuttingCounter.cs

#### Description
This class represents a specific type of counter object. It inherits functionality from the `BaseCounter` class and implements cutting behavior for kitchen objects placed on the counter.

#### Inherits from
- `BaseCounter`

#### Events
- `public static EventHandler OnAnyCut`: Static event triggered when any cutting action occurs.

#### Fields
- `[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray`: Serialized array of CuttingRecipeSO instances representing the cutting recipes available for this counter.
- `private int cuttingProgress`: Integer representing the progress of the cutting action.

#### Methods
- `new public static void ResetStaticData()`: Hides the static method `ResetStaticData()` of the base class and resets static data specific to this class.
- `public override void Interact(Player player)`: Overrides the base class method to implement interaction behavior when a player interacts with the counter.
    - If there is no kitchen object on the counter:
        - If the player is carrying a kitchen object, it sets the counter as the parent for that object and resets the cutting progress.
        - Otherwise, it does nothing.
    - If there is a kitchen object on the counter:
        - If the player is carrying a kitchen object:
            - If the player is carrying a plate, it tries to add ingredients from the counter's kitchen object to the plate. If successful, it destroys the counter's kitchen object.
            - If the counter's kitchen object is a plate, it tries to add ingredients from the player's kitchen object to the plate. If successful, it destroys the player's kitchen object.
        - If the player is not carrying a kitchen object, it sets the player as the parent for the counter's kitchen object and triggers an event to notify progress change.
- `public override void InteractAlternate(Player player)`: Overrides the base class method to implement alternate interaction behavior when a player interacts with the counter.
    - If there is a kitchen object on the counter and there is a cutting recipe matching the input kitchen object:
        - Increments the cutting progress.
        - Triggers events related to cutting actions and progress change.
        - If the cutting progress reaches the maximum, spawns a new kitchen object based on the cutting recipe.
- `private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)`: Checks if there is a cutting recipe available for the input kitchen object.
- `private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the output kitchen object associated with the input kitchen object from the cutting recipe.
- `private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the cutting recipe that matches the input kitchen object from the array of cutting recipes.

#### Usage
This class is used to define the behavior of a specific type of counter in the game environment, allowing players to perform cutting actions on kitchen objects placed on the counter.

#### Notes
- This class extends functionality from the `BaseCounter` class, providing additional behavior specific to cutting actions in the game.
- It manages cutting progress, triggers events related to cutting actions, and handles the application of cutting recipes to kitchen objects.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public static EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is not Kitchen Object Here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
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
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //There is KitchenObject here
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestorySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
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
```

### 4a. CuttingCounterVisual.cs

#### Description
This class represents the visual component of a cutting counter object. It listens to the `OnCut` event from a `CuttingCounter` instance and triggers animations accordingly.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `private const string CUT`: Constant string representing the trigger name for the cutting animation.
- `[SerializeField] CuttingCounter cuttingCounter`: Reference to the `CuttingCounter` instance associated with this visual component.
- `private Animator animator`: Reference to the Animator component attached to the GameObject.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the `animator` field by getting the Animator component attached to the GameObject.
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to the `OnCut` event of the associated `CuttingCounter` instance.
- `private void CuttingCounter_OnCut(object sender, EventArgs e)`: Event handler method triggered when a cutting action occurs on the associated `CuttingCounter`. Sets the trigger for the cutting animation on the Animator component.

#### Usage
This class is used to manage the visual representation of a cutting counter object in the game environment. It listens to events triggered by cutting actions on the associated `CuttingCounter` instance and triggers animations accordingly.

#### Notes
- This class serves as a bridge between the logic of the `CuttingCounter` and its visual representation, allowing for synchronized animations based on gameplay events.
- The `CUT` constant string represents the name of the trigger parameter in the Animator controller used to control the cutting animation.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] CuttingCounter cuttingCounter;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}

```

---









