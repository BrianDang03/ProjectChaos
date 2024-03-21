# Code Name: ProjectChaos
---
## Table of Contents

### 1. Scripts
   - Counters
     - [1. BaseCounter.cs](README.md#1-basecountercs)
     - [2. ClearConter.cs](README.md#2-clearcountercs)
     - [3. ContainerCounter.cs](README.md#3-containercountercs)
       - [3a. ContainerCounterVisual.cs](README.md#3a-containercountervisualcs)
     - [4. CuttingCounter.cs](README.md#4-cuttingcountercs)
       - [4a. CuttingCounterVisual.cs](README.md#4a-cuttingcountervisualcs)
     - [5. DeliveryCounter.cs](README.md#5-deliverycountercs)
     - [6. PlatesCounter.cs](README.md#6-platescountercs)
       - [6a. PlateCounterVisual.cs](README.md#6a-platescountervisualcs)
     - [7. StoveCounter.cs](README.md#7-stovecountercs)
       - [7a. StoveCounterVisual.cs](README.md#7a-stovecountervisualcs) 
       - [7b. StoveCounterSound.cs](README.md#7b-stovecountersoundcs)
     - [8. TrashCounter.cs](README.md#8-trashcountercs)
   - ScriptableObjects
     - [1. AudioClipRefsSO.cs](README.md#1-audiocliprefssocs)
     - [2. BurningRecipeSO.cs](README.md#2-burningrecipesocs)
     - [3. CuttingRecipeSO.cs](README.md#3-cuttingrecipesocs)
     - [4. FryingRecipeSO.cs](README.md#4-fryingrecipesocs)
     - [5. KitchenObjectSO.cs](README.md#5-kitchenobjectsocs)
     - [6. RecipeListSO.cs](README.md#6-recipelistsocs)
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
# Scripts
---
## Counters

### [1. BaseCounter.cs](README.md#1-scripts)

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
### [2. ClearCounter.cs](README.md#1-scripts)

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
### [3. ContainerCounter.cs](README.md#1-scripts)

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
This class is used to define the behavior of a container counter in the game environment. It handles interactions between players and kitchen objects placed on the counter, as well as spawning new kitchen objects when both the player and the counter are empty.

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

### [3a. ContainerCounterVisual.cs](README.md#1-scripts)

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
### [4. CuttingCounter.cs](README.md#1-scripts)

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
This class is used to define the behavior of a cutting counter in the game environment, allowing players to perform cutting actions on kitchen objects placed on the counter.

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

### [4a. CuttingCounterVisual.cs](README.md#1-scripts)

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
### [5. DeliveryCounter.cs](README.md#1-scripts)

#### Description
This class represents a delivery counter object. It inherits functionality from the `BaseCounter` class and implements behavior specific to delivering recipes.

#### Inherits from
- `BaseCounter`

#### Fields
- `public static DeliveryCounter Instance { get; private set; }`: Singleton instance of the `DeliveryCounter` class.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the singleton instance of the `DeliveryCounter`.
- `public override void Interact(Player player)`: Overrides the base class method to implement interaction behavior when a player interacts with the delivery counter.
    - If the player is carrying a kitchen object:
        - If the player is carrying a plate, it delivers the recipe associated with the plate using the `DeliveryManager`, and then destroys the plate.

#### Usage
This class is used to define the behavior of a delivery counter in the game environment, allowing players to deliver recipes by placing plates on the delivery counter.

#### Notes
- This class extends functionality from the `BaseCounter` class, providing behavior specific to recipe delivery in the game.
- It interacts with the `DeliveryManager` to deliver recipes when plates are placed on the delivery counter.
- The singleton pattern is used to ensure that only one instance of the `DeliveryCounter` exists in the game.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //Only Accpets Plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestorySelf();
            }
        }
    }
}

```

---
### [6. PlatesCounter.cs](README.md#1-scripts)

#### Description
This class represents a plates counter object. It inherits functionality from the `BaseCounter` class and implements behavior specific to spawning and removing plates.

#### Inherits from
- `BaseCounter`

#### Events
- `public event EventHandler OnPlateSpawned`: Event triggered when a plate is spawned on the plates counter.
- `public event EventHandler OnPlateRemoved`: Event triggered when a plate is removed from the plates counter.

#### Fields
- `[SerializeField] private KitchenObjectSO plateKitchenObjectSO`: Serialized field representing the scriptable object for the plate kitchen object.
- `[SerializeField] float spawnPlateTimerMax = 4f`: Maximum timer value for spawning plates.
- `[SerializeField] private int platesSpawnedAmountMax = 4`: Maximum number of plates that can be spawned on the plates counter.

#### Methods
- `private void Update()`: Unity lifecycle method called once per frame. Updates the timer for spawning plates and spawns a plate if the timer exceeds the maximum value and the maximum number of plates has not been reached.
- `public override void Interact(Player player)`: Overrides the base class method to implement interaction behavior when a player interacts with the plates counter.
    - If the player is empty-handed and there are plates available on the counter:
        - Removes a plate from the counter, spawns it to the player, and triggers the `OnPlateRemoved` event.

#### Usage
This class is used to define the behavior of a plate counter in the game environment, allowing players to interact with plates by picking them up from the plates counter.

#### Notes
- This class extends functionality from the `BaseCounter` class, providing behavior specific to managing plates in the game.
- It tracks the number of plates spawned on the counter and controls the spawning and removal of plates based on player interactions.
- Events are used to notify other game systems or objects when plates are spawned or removed from the plates counter.

#### Code
```
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

            if (platesSpawnedAmount < platesSpawnedAmountMax)
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
```

### [6a. PlatesCounterVisual.cs](README.md#1-scripts)

#### Description
This class represents the visual component of a plates counter object. It listens to events from a `PlatesCounter` instance and updates the visual representation of plates on the counter accordingly.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `[SerializeField] PlatesCounter platesCounter`: Reference to the `PlatesCounter` instance associated with this visual component.
- `[SerializeField] private Transform counterTopPoint`: Reference to the transform representing the top point of the counter.
- `[SerializeField] private Transform plateVisualPrefab`: Prefab representing the visual representation of a plate.
- `private List<GameObject> plateVisualGameObjectList`: List to store references to instantiated plate visual GameObjects.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the list for plate visual GameObjects.
- `void Start()`: Unity lifecycle method called before the first frame update. Subscribes to the `OnPlateSpawned` and `OnPlateRemoved` events of the associated `PlatesCounter` instance.
- `private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)`: Event handler method triggered when a plate is spawned on the plates counter. Instantiates a plate visual GameObject and adds it to the list.
- `private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)`: Event handler method triggered when a plate is removed from the plates counter. Removes the last plate visual GameObject from the list and destroys it.

#### Usage
This class is used to manage the visual representation of plates on the plates counter in the game environment. It listens to events triggered by the `PlatesCounter` instance and updates the visual representation accordingly.

#### Notes
- This class is responsible for instantiating and destroying plate visual GameObjects based on events triggered by the `PlatesCounter` instance.
- It ensures that the visual representation of plates on the counter remains synchronized with the actual state of the `PlatesCounter`.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffset = .1f;
        plateVisualTransform.localPosition = new UnityEngine.Vector3(0, plateOffset * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}

```

---
### [7. StoveCounter.cs](README.md#1-scripts)

#### Description
This class represents a stove counter object. It inherits functionality from the `BaseCounter` class and implements behavior specific to frying and burning recipes.

#### Inherits from
- `BaseCounter`
- `IHasProgress`

#### Events
- `public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged`: Event triggered when the progress of frying or burning changes.
- `public event EventHandler<OnStateChangedEventArgs> OnStateChanged`: Event triggered when the state of the stove counter changes.
    - `public class OnStateChangedEventArgs : EventArgs`
        - `public State state`: The current state of the stove counter.

#### Fields
- `[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray`: Serialized array of `FryingRecipeSO` instances representing the frying recipes available for this stove counter.
- `[SerializeField] private BuringRecipeSO[] burningRecipeSOArray`: Serialized array of `BuringRecipeSO` instances representing the burning recipes available for this stove counter.
- `private State state`: Enum representing the current state of the stove counter (Idle, Frying, Fried, Burned).
- `private float fryingTimer`: Timer for tracking the frying progress.
- `private FryingRecipeSO fryingRecipeSO`: Reference to the frying recipe currently being processed.
- `private float buringTimer`: Timer for tracking the burning progress.
- `private BuringRecipeSO burningRecipeSO`: Reference to the burning recipe currently being processed.

#### Methods
- `private void Start()`: Unity lifecycle method called before the first frame update. Initializes the stove counter state to Idle.
- `private void Update()`: Unity lifecycle method called once per frame. Updates the progress of frying or burning based on the current state of the stove counter.
- `public override void Interact(Player player)`: Overrides the base class method to implement interaction behavior when a player interacts with the stove counter.
    - Handles interactions such as placing ingredients, frying, and removing cooked items.
- `private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)`: Checks if there is a frying recipe available for the input kitchen object.
- `private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the output kitchen object associated with the input kitchen object from the frying recipe.
- `private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the frying recipe that matches the input kitchen object from the array of frying recipes.
- `private BuringRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the burning recipe that matches the input kitchen object from the array of burning recipes.

#### Usage
This class is used to define the behavior of a stove counter in the game environment, allowing players to fry and burn ingredients to create cooked items.

## Notes
- This class extends functionality from the `BaseCounter` class and implements the `IHasProgress` interface to track progress.
- It manages the state of the stove counter and processes frying and burning recipes based on player interactions.
- Events are used to notify other game systems or objects when the progress or state of the stove counter changes.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BuringRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float buringTimer;
    private BuringRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });

                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        //Fried
                        fryingTimer = 0f;
                        GetKitchenObject().DestorySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        buringTimer = 0f;
                        state = State.Fried;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                    break;
                case State.Fried:
                    buringTimer += Time.deltaTime;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = buringTimer / burningRecipeSO.burningTimerMax });

                    if (buringTimer >= burningRecipeSO.burningTimerMax)
                    {
                        //Fried
                        buringTimer = 0f;
                        GetKitchenObject().DestorySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }
                    break;
                case State.Burned:
                    break;
            }

        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is not Kitchen Object Here
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
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

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }
                }
            }
            else
            {
                //Player is not Carrying Anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private BuringRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BuringRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
```

### [7a. StoveCounterVisual.cs](README.md#1-scripts)

#### Description
This class represents the visual component of a stove counter object. It listens to the `OnStateChanged` event from a `StoveCounter` instance and updates the visual representation of the stove accordingly.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `[SerializeField] private StoveCounter stoveCounter`: Reference to the `StoveCounter` instance associated with this visual component.
- `[SerializeField] private GameObject stoveOnGameObject`: Reference to the GameObject representing the stove's visual when it is turned on.
- `[SerializeField] private GameObject particle`: Reference to the particle GameObject representing the visual effect of the stove when it is active.

#### Methods
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to the `OnStateChanged` event of the associated `StoveCounter` instance.
- `private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)`: Event handler method triggered when the state of the stove counter changes. Updates the visual representation of the stove based on the new state.

#### Usage
This class is used to manage the visual representation of a stove counter object in the game environment. It listens to events triggered by the `StoveCounter` instance and updates the visual representation accordingly.

#### Notes
- This class ensures that the visual representation of the stove counter remains synchronized with the state changes of the associated `StoveCounter`.
- It controls the visibility of the stove's visual and particle effects based on whether the stove is frying or has finished frying.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particle;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particle.SetActive(showVisual);
    }
}
```

### [7b. StoveCounterSound.cs](README.md#1-scripts)

#### Description
This class represents the sound component of a stove counter object. It listens to the `OnStateChanged` event from a `StoveCounter` instance and plays or pauses the associated audio source based on the state of the stove.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `[SerializeField] private StoveCounter stoveCounter`: Reference to the `StoveCounter` instance associated with this sound component.
- `private AudioSource audioSource`: Reference to the AudioSource component attached to this GameObject.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the audio source component.
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to the `OnStateChanged` event of the associated `StoveCounter` instance.
- `void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)`: Event handler method triggered when the state of the stove counter changes. Plays or pauses the audio source based on the new state.

#### Usage
This class is used to manage the sound effects associated with a stove counter object in the game environment. It listens to events triggered by the `StoveCounter` instance and plays or pauses the associated audio source accordingly.

#### Notes
- This class ensures that the sound effects of the stove counter remain synchronized with the state changes of the associated `StoveCounter`.
- It plays the sound effect when the stove is frying or has finished frying, and pauses the sound when the stove is idle or burned.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}

```

---
### [8. TrashCounter.cs](README.md#1-scripts)

#### Description
This class represents a trash counter object. It inherits functionality from the `BaseCounter` class and implements behavior specific to trashing kitchen objects.

#### Inherits from
- `BaseCounter`

#### Events
- `public static EventHandler OnAnyObjectTrashed`: Event triggered when any object is trashed in the trash counter.

#### Methods
- `new public static void ResetStaticData()`: Overrides the base class method to reset static data. Resets the `OnAnyObjectTrashed` event to null.
- `public override void Interact(Player player)`: Overrides the base class method to implement interaction behavior when a player interacts with the trash counter.
    - If there is no kitchen object on the counter and the player is carrying a kitchen object:
        - Destroys the kitchen object carried by the player.
        - Triggers the `OnAnyObjectTrashed` event.

#### Usage
This class is used to define the behavior of a trash counter in the game environment, allowing players to dispose of unwanted kitchen objects.

#### Notes
- This class extends functionality from the `BaseCounter` class, providing behavior specific to trashing kitchen objects.
- It handles player interactions with the trash counter and triggers events when objects are trashed.

#### Code
```
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
```

---
## ScriptableObjects
### [1. AudioClipRefsSO.cs](README.md#1-scripts)

#### Description
This scriptable object represents a collection of audio clips categorized by different types of sounds commonly used in game. It provides references to various audio clips that can be used for different events or actions within the game.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public AudioClip[] chop`: Array of audio clips for chopping sounds.
- `public AudioClip[] deliveryFail`: Array of audio clips for delivery failure sounds.
- `public AudioClip[] deliverySucess`: Array of audio clips for delivery success sounds.
- `public AudioClip[] footstep`: Array of audio clips for footstep sounds.
- `public AudioClip[] objectDrop`: Array of audio clips for object dropping sounds.
- `public AudioClip[] objectPickup`: Array of audio clips for object pickup sounds.
- `public AudioClip stoveSizzle`: Single audio clip for stove sizzle sound.
- `public AudioClip[] trash`: Array of audio clips for trash sounds.
- `public AudioClip[] warning`: Array of audio clips for warning sounds.

#### Methods
- None

#### Usage
This scriptable object is used to store references to audio clips used for various events or actions within the game environment, particularly in a kitchen setting. It allows easy access to different types of sounds to enhance the auditory experience of the game.

#### Notes
- Audio clips are categorized by different types of sounds commonly encountered in a kitchen environment, such as chopping, delivery, footstep, object interaction, stove sizzle, trash, and warning sounds.
- This scriptable object can be easily extended or modified to include additional audio clips or categories as needed.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySucess;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
```

---
### [2. BurningRecipeSO.cs](READEME.md#1-scripts)

#### Description
This scriptable object represents a burning recipe. It defines the input kitchen object, output kitchen object, and the maximum burning timer allowed for the recipe to complete.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public KitchenObjectSO input`: The input kitchen object required for the burning recipe.
- `public KitchenObjectSO output`: The output kitchen object produced after the burning process.
- `public float burningTimerMax`: The maximum duration allowed for the burning process to complete.

#### Methods
- None

#### Usage
This scriptable object is used to define specific burning recipes in the game environment. It provides a structured way to store and manage burning recipes, including their input objects, output objects, and timing constraints.

#### Notes
- Burning recipes typically involve subjecting certain kitchen objects to heat or fire until they reach a desired state or condition.
- The `input` field specifies the kitchen object required for the burning recipe to start, while the `output` field defines the resulting kitchen object after the burning process is completed.
- The `burningTimerMax` field determines the maximum duration allowed for the burning process to complete, ensuring that the gameplay experience remains balanced and consistent.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuringRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}
```

---
### [3. CuttingRecipeSO.cs](README.md#1-scripts)

#### Description
This scriptable object represents a cutting recipe. It defines the input kitchen object, output kitchen object, and the maximum cutting progress required to complete the recipe.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public KitchenObjectSO input`: The input kitchen object required for the cutting recipe.
- `public KitchenObjectSO output`: The output kitchen object produced after the cutting process.
- `public int cuttingProgressMax`: The maximum cutting progress required to complete the cutting recipe.

#### Methods
- None

#### Usage
This scriptable object is used to define specific cutting recipes in the game environment. It provides a structured way to store and manage cutting recipes, including their input objects, output objects, and cutting progress requirements.

#### Notes
- Cutting recipes typically involve slicing or chopping certain kitchen objects into smaller pieces to prepare them for cooking or further processing.
- The `input` field specifies the kitchen object required for the cutting recipe to start, while the `output` field defines the resulting kitchen object after the cutting process is completed.
- The `cuttingProgressMax` field determines the maximum cutting progress required to complete the cutting recipe, ensuring that the gameplay experience remains balanced and consistent.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressMax;
}
```

---
### [4. FryingRecipeSO.cs](README.md#1-scripts)

#### Description
This scriptable object represents a frying recipe. It defines the input kitchen object, output kitchen object, and the maximum frying timer allowed for the recipe to complete.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public KitchenObjectSO input`: The input kitchen object required for the frying recipe.
- `public KitchenObjectSO output`: The output kitchen object produced after the frying process.
- `public float fryingTimerMax`: The maximum duration allowed for the frying process to complete.

#### Methods
- None

#### Usage
This scriptable object is used to define specific frying recipes in the game environment. It provides a structured way to store and manage frying recipes, including their input objects, output objects, and timing constraints.

#### Notes
- Frying recipes typically involve cooking certain kitchen objects in hot oil or on a heated surface until they reach a desired state or condition.
- The `input` field specifies the kitchen object required for the frying recipe to start, while the `output` field defines the resulting kitchen object after the frying process is completed.
- The `fryingTimerMax` field determines the maximum duration allowed for the frying process to complete, ensuring that the gameplay experience remains balanced and consistent.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}
```

---
### [5. KitchenObjectSO.cs](README.md#1-scripts)

#### Description
This scriptable object represents. It contains information such as the prefab, sprite, and name of the object.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public Transform prefab`: The prefab used to instantiate the kitchen object in the game world.
- `public Sprite sprite`: The sprite representing the visual appearance of the kitchen object.
- `public string objectName`: The name of the kitchen object.

#### Methods
- None

#### Usage
This scriptable object is used to define various kitchen objects in the game environment. It provides a convenient way to store and manage information about kitchen objects, including their visual appearance and name.

#### Notes
- Kitchen objects may include items such as ingredients, utensils, appliances, or other items commonly found in a kitchen setting.
- The `prefab` field allows for easy instantiation of kitchen objects in the game world.
- The `sprite` field defines the visual representation of the kitchen object, which may be used for rendering in the game.
- The `objectName` field provides a human-readable name for the kitchen object, which can be displayed to players or used for reference in code.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
```

---
# [6. RecipeListSO.cs](README.md#1-scripts)

## Description
This scriptable object represents a list of recipes in game. It contains a collection of `RecipeSO` scriptable objects, each representing a specific recipe.

## Inherits from
- `ScriptableObject`

## Fields
- `public List<RecipeSO> recipeSOList`: The list of recipe scriptable objects.

## Methods
- None

## Usage
This scriptable object is used to store and manage a collection of recipes in the game environment. It provides a convenient way to organize and access multiple recipes from a centralized source.

## Notes
- Recipes may include instructions for preparing various dishes or items within the game.
- The `recipeSOList` field allows for easy access to individual recipe scriptable objects, which can then be used to retrieve detailed information about each recipe.
- This scriptable object can be used in conjunction with other game systems to implement features such as crafting, cooking, or quest objectives that involve completing specific recipes.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
```

---
