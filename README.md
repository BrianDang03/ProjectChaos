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
     - [7. RecipeSO.cs](README.md#7-recipesocs)
   -  UI
      - [1. DeliveryManagerSingleUI.cs](README.md#1-deliverymanagersingleuics)
      - [2. DeliveryManagerUI.cs](README.md#2-deliverymanageruics)
      - [3. GameOverUI.cs](README.md#3-gameoveruics)
      - [4. GamePauseUI.cs](README.md#4-gamepauseuics)
      - [5. GamePlayingClockUI.cs](README.md#5-gameplayingclockuics)
      - [6. GameStartCountdownUI.cs](README.md#6-gamestartcountdownuics)
      - [7. MainMenuUI.cs](README.md#7-mainmenuuics)
      - [8. OptionsUI.cs](README.md#8-optionsuics)
      - [9. PlateIconsSingleUI.cs](README.md#9-plateiconssingleuics)
      - [10. PlateIconUI.cs](README.md#10-plateiconuics)
      - [11. ProgressBarUI.cs](README.md#11-progressbaruics)
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

#### Notes
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
### [2. BurningRecipeSO.cs](README.md#1-scripts)

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
### [6. RecipeListSO.cs](README.md#1-scripts)

#### Description
This scriptable object represents a list of recipes in game. It contains a collection of `RecipeSO` scriptable objects, each representing a specific recipe.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public List<RecipeSO> recipeSOList`: The list of recipe scriptable objects.

#### Methods
- None

#### Usage
This scriptable object is used to store and manage a collection of recipes in the game environment. It provides a convenient way to organize and access multiple recipes from a centralized source.

#### Notes
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
### [7. RecipeSO.cs](README.md#1-scripts)

#### Description
This scriptable object represents a recipe in a game. It contains a list of kitchen objects required for the recipe and the name of the recipe.

#### Inherits from
- `ScriptableObject`

#### Fields
- `public List<KitchenObjectSO> kitchenObjectSOList`: The list of kitchen object scriptable objects required for the recipe.
- `public string recipeName`: The name of the recipe.

#### Methods
- None

#### Usage
This scriptable object is used to define specific recipes in the game environment. It provides a structured way to store and manage recipes, including their required kitchen objects and names.

#### Notes
- Recipes define combinations of kitchen objects required to craft or prepare specific items within the game.
- The `kitchenObjectSOList` field specifies the kitchen objects required for the recipe, allowing for complex combinations of ingredients, utensils, or other items.
- The `recipeName` field provides a human-readable name for the recipe, which can be displayed to players or used for reference in code.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;
}
```

---
## UI
### [1. DeliveryManagerSingleUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) for displaying details of a single recipe in the delivery manager. It displays the name of the recipe and icons representing the kitchen objects required for the recipe.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public TextMeshProUGUI recipeNameText`: Text component for displaying the name of the recipe.
- `public Transform iconContainer`: Container for holding icon representations of kitchen objects.
- `public Transform iconTemplate`: Template object for the icon representation of a kitchen object.

#### Methods
- `void Awake()`: Called when the script instance is being loaded.
- `public void SetRecipeSO(RecipeSO recipeSO)`: Sets the recipe information to be displayed in the UI.

#### Usage
This script is attached to a GameObject in the scene representing the delivery manager UI. It is responsible for updating the UI with details of a single recipe when requested.

#### Notes
- The `iconTemplate` serves as a template for creating icon representations of kitchen objects. It is set as inactive in the hierarchy to prevent it from being displayed directly.
- When `SetRecipeSO` is called with a `RecipeSO` object, the UI updates to display the name of the recipe and icons representing the kitchen objects required for the recipe.
- It dynamically instantiates icon representations for each kitchen object in the recipe's `kitchenObjectSOList` and sets their sprites based on the associated `sprite` field in the `KitchenObjectSO`.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }

}
```

---
### [2. DeliveryManagerUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) for the delivery manager in game. It displays a list of waiting recipes and updates the UI when new recipes are spawned or completed.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public Transform container`: Container for holding the list of waiting recipes in the UI.
- `public Transform recipeTemplate`: Template object for displaying individual recipe details in the UI.

#### Methods
- `void Awake()`: Called when the script instance is being loaded.
- `void Start()`: Called before the first frame update.
- `void DelieveryManager_OnRecipeSpawned(object sender, EventArgs e)`: Event handler for the `OnRecipeSpawned` event triggered by the `DeliveryManager`.
- `void DelieveryManager_OnRecipeCompleted(object sender, EventArgs e)`: Event handler for the `OnRecipeCompleted` event triggered by the `DeliveryManager`.
- `void UpdateVisual()`: Updates the visual representation of the delivery manager UI based on the current list of waiting recipes.

#### Usage
This script is attached to a GameObject in the scene representing the delivery manager UI. It listens for events triggered by the `DeliveryManager` and updates the UI accordingly.

#### Notes
- The `recipeTemplate` serves as a template for creating individual recipe entries in the UI. It is set as inactive in the hierarchy to prevent it from being displayed directly.
- When the game starts, the script subscribes to the `OnRecipeSpawned` and `OnRecipeCompleted` events triggered by the `DeliveryManager`. It then updates the UI to display the initial list of waiting recipes.
- The `UpdateVisual` method clears the current UI representation and dynamically instantiates UI elements for each waiting recipe obtained from the `DeliveryManager`.
- Each instantiated recipe UI element is populated using the `SetRecipeSO` method of the `DeliveryManagerSingleUI` component attached to it, which sets the recipe information to be displayed.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DelieveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DelieveryManager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void DelieveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DelieveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
```

---
### [3. GameOverUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) for displaying the game over screen in game. It shows the total number of recipes delivered when the game is over.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public TextMeshProUGUI recipesDeliveredText`: Text component for displaying the total number of recipes delivered.

#### Methods
- `void Start()`: Called before the first frame update.
- `void KitchenGameManager_OnStateChanged(object sender, EventArgs e)`: Event handler for the `OnStateChanged` event triggered by the `KitchenGameManager`.
- `void Show()`: Displays the game over UI.
- `void Hide()`: Hides the game over UI.

#### Usage
This script is attached to a GameObject in the scene representing the game over UI. It listens for changes in the game state triggered by the `KitchenGameManager`. When the game enters the game over state, it displays the game over UI and shows the total number of recipes delivered.

#### Notes
- The `Start` method subscribes to the `OnStateChanged` event triggered by the `KitchenGameManager`. When the game state changes, this event is fired, and the UI is updated accordingly.
- The `KitchenGameManager_OnStateChanged` method checks if the game is over. If it is, the game over UI is displayed, and the total number of successful recipes delivered is updated in the `recipesDeliveredText`.
- The `Show` and `Hide` methods are used to toggle the visibility of the game over UI.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

```

---
### [4. GamePauseUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) for pausing the game. It provides buttons for resuming the game, returning to the main menu, and accessing options.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public Button resumeButton`: Button for resuming the game.
- `public Button mainMenuButton`: Button for returning to the main menu.
- `public Button optionsButton`: Button for accessing options.

#### Methods
- `void Awake()`: Called when the script instance is being loaded.
- `void Start()`: Called before the first frame update.
- `void KitchenGameManager_OnGamePaused(object sender, EventArgs e)`: Event handler for the `OnGamePaused` event triggered by the `KitchenGameManager`.
- `void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)`: Event handler for the `OnGameUnpaused` event triggered by the `KitchenGameManager`.
- `void Show()`: Displays the pause menu UI.
- `void Hide()`: Hides the pause menu UI.

#### Usage
This script is attached to a GameObject in the scene representing the pause menu UI. It listens for events triggered by the `KitchenGameManager` to show or hide the pause menu UI when the game is paused or unpaused, respectively.

#### Notes
- The `Awake` method sets up the button click listeners for the resume, main menu, and options buttons. Clicking these buttons triggers the corresponding actions.
- The `Start` method subscribes to the `OnGamePaused` and `OnGameUnpaused` events triggered by the `KitchenGameManager`. When the game is paused, the pause menu UI is shown, and when the game is unpaused, the UI is hidden.
- The `Show` and `Hide` methods are used to toggle the visibility of the pause menu UI.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        });

        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

```

---
### [5. GamePlayingClockUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) for displaying a clock or timer during gameplay. It updates the fill amount of an image component based on the normalized game playing timer value.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public Image timerImage`: Image component representing the timer visual.

#### Methods
- `void Update()`: Called once per frame. Updates the fill amount of the timer image based on the normalized game playing timer value.

#### Usage
This script is attached to a GameObject in the scene representing the clock or timer UI. It requires an image component to display the timer visual. During gameplay, the `Update` method continuously updates the fill amount of the timer image based on the normalized game playing timer value obtained from the `KitchenGameManager`.

#### Notes
- The `Update` method retrieves the normalized game playing timer value from the `KitchenGameManager` and sets it as the fill amount of the timer image. This allows the timer image to visually represent the remaining time during gameplay.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
```

---
### [6. GameStartCountdownUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) for displaying a countdown timer before the start of the game. It updates the countdown text based on the remaining time until the game starts.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public TextMeshProUGUI countdownText`: TextMeshProUGUI component representing the countdown text.

#### Methods
- `void Start()`: Called before the first frame update. Subscribes to the `OnStateChanged` event triggered by the `KitchenGameManager`.
- `void Update()`: Called once per frame. Updates the countdown text with the remaining time until the game starts.
- `void KitchenGameManager_OnStateChanged(object sender, EventArgs e)`: Event handler for the `OnStateChanged` event triggered by the `KitchenGameManager`.
- `void Show()`: Displays the countdown UI.
- `void Hide()`: Hides the countdown UI.

#### Usage
This script is attached to a GameObject in the scene representing the countdown UI. It requires a TextMeshProUGUI component to display the countdown text. During the countdown to the start of the game, the `Update` method continuously updates the countdown text based on the remaining time obtained from the `KitchenGameManager`. The countdown UI is shown or hidden based on the state of the countdown to start as indicated by the `KitchenGameManager`.

#### Notes
- The `Start` method subscribes to the `OnStateChanged` event triggered by the `KitchenGameManager`. When the game's state changes to the countdown to start active, the countdown UI is shown.
- The `Update` method retrieves the remaining time until the game starts from the `KitchenGameManager` and updates the countdown text accordingly.
- The `KitchenGameManager_OnStateChanged` method toggles the visibility of the countdown UI based on the state of the countdown to start as indicated by the `KitchenGameManager`.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStatTimer()).ToString();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
```

---
### [7. MainMenuUI.cs](README.md#1-scripts)

#### Description
This script manages the user interface (UI) functionality for the main menu scene. It handles button clicks to start the game or quit the application.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public Button playButton`: Button component representing the play button in the main menu UI.
- `public Button quitButton`: Button component representing the quit button in the main menu UI.

#### Methods
- `void Awake()`: Called when the script instance is being loaded. Subscribes to the click events of the play and quit buttons and sets the time scale to 1.

#### Usage
This script is attached to a GameObject in the main menu scene, typically associated with the main menu canvas. It requires Button components for the play and quit buttons. The `Awake` method subscribes to the click events of these buttons and sets the time scale to 1 to ensure normal game speed.

#### Notes
- The `Awake` method subscribes to the click events of the play and quit buttons. When the play button is clicked, it loads the main scene using the `Loader.Load` method. When the quit button is clicked, it quits the application using `Application.Quit`.
- Setting the time scale to 1 ensures that the game runs at normal speed when transitioning from the main menu to the gameplay.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Main);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}

```

---
### [8. OptionsUI.cs](README.md#1-scripts)

#### Description
This script manages the options user interface (UI) functionality. It allows the player to adjust sound effects and music volume settings. Additionally, it provides a button to close the options menu.

#### Inherits from
- `MonoBehaviour`

#### Properties
- `public static OptionsUI Instance`: Singleton instance of the OptionsUI class.

#### Fields
- `public Button soundEffectsButton`: Button component representing the sound effects volume adjustment button.
- `public Button musicButton`: Button component representing the music volume adjustment button.
- `public Button closeButton`: Button component representing the close button for the options menu.
- `public TextMeshProUGUI soundEffectsText`: TextMeshProUGUI component displaying the current sound effects volume.
- `public TextMeshProUGUI musicText`: TextMeshProUGUI component displaying the current music volume.

#### Methods
- `void Awake()`: Called when the script instance is being loaded. Initializes the singleton instance, subscribes to button click events, and updates the visual representation of sound effects and music volume settings.
- `void Start()`: Called before the first frame update. Subscribes to the game unpaused event and initializes the visual representation of volume settings. Hides the options menu.
- `void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)`: Event handler for the game unpaused event. Hides the options menu.
- `void UpdateVisual()`: Updates the visual representation of sound effects and music volume settings.
- `public void Show()`: Displays the options menu.
- `private void Hide()`: Hides the options menu.

#### Usage
This script is attached to a GameObject representing the options menu in the game. It requires Button and TextMeshProUGUI components for sound effects, music, and close buttons, as well as text displays for volume settings. The `Awake` method initializes the singleton instance and subscribes to button click events for adjusting volume settings. The `Start` method initializes the visual representation of volume settings and subscribes to the game unpaused event. The `Show` method displays the options menu, and the `Hide` method hides it.

#### Notes
- The `Awake` method sets up the singleton pattern to ensure that only one instance of the OptionsUI class exists throughout the game.
- Button click event listeners are used to adjust sound effects and music volume settings. These methods also update the visual representation of volume settings.
- The `KitchenGameManager_OnGameUnpaused` method hides the options menu when the game is unpaused.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Math.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Math.Round(MusicManager.Instance.GetVolume() * 10f);

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
```

---
### [9. PlateIconsSingleUI.cs](README.md#1-scripts)

#### Description
This script manages the visual representation of a single plate icon in the user interface (UI). It sets the sprite of the plate icon based on the provided KitchenObjectSO.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public Image image`: Image component representing the plate icon in the UI.

#### Methods
- `public void SetKitchenObjectSO(KitchenObjectSO kitchenOBjectSO)`: Sets the sprite of the plate icon based on the provided KitchenObjectSO.

#### Usage
This script is attached to a GameObject representing a single plate icon in the UI. It requires an Image component for displaying the plate icon. The `SetKitchenObjectSO` method is used to update the sprite of the plate icon based on the provided KitchenObjectSO.

#### Notes
- The `SetKitchenObjectSO` method updates the sprite of the plate icon to match the sprite of the provided KitchenObjectSO, allowing for dynamic changes to the plate icon based on the game state.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenOBjectSO)
    {
        image.sprite = kitchenOBjectSO.sprite;
    }
}
```

---
### [10. PlateIconUI.cs](README.md#1-scripts)

#### Description
This script manages the visual representation of ingredients on a plate icon in the user interface (UI). It dynamically updates the plate icon to display the ingredients added to the plate.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public PlateKitchenObject plateKitchenObject`: Reference to the PlateKitchenObject script, representing the plate containing ingredients.
- `public Transform iconTemplate`: Reference to the template for individual ingredient icons.

#### Methods
- `private void Awake()`: Called when the script instance is being loaded. It deactivates the icon template GameObject to prevent it from being visible in the UI.
- `private void Start()`: Called before the first frame update. Subscribes to the PlateKitchenObject's OnIngredientAdded event to update the UI when ingredients are added to the plate.
- `private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)`: Event handler method called when an ingredient is added to the plate. It triggers an update of the UI.
- `private void UpdateVisual()`: Updates the visual representation of the plate icon based on the ingredients currently on the plate. It destroys existing ingredient icons and creates new ones based on the plate's ingredients.

#### Usage
This script is attached to a GameObject representing a plate icon in the UI. It requires a reference to a PlateKitchenObject representing the plate containing ingredients and a template for individual ingredient icons. The plate icon's visual representation is updated dynamically as ingredients are added to or removed from the plate.

#### Notes
- The `UpdateVisual` method dynamically updates the plate icon to display the ingredients currently on the plate, allowing players to visually track the contents of the plate during gameplay.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
```

---
### [11. ProgressBarUI.cs](README.md#1-scripts)

#### Description
This script handles the visual representation of a progress bar based on an object that implements the `IHasProgress` interface. It dynamically updates the fill amount of the progress bar to reflect changes in the progress of the associated object.

#### Inherits from
- `MonoBehaviour`

#### Fields
- `public GameObject hasProgressGameObject`: Reference to the GameObject containing the component implementing the `IHasProgress` interface.
- `private IHasProgress hasProgress`: Reference to the component implementing the `IHasProgress` interface.
- `public Image barImage`: Reference to the Image component representing the progress bar.

#### Methods
- `private void Start()`: Called before the first frame update. Initializes the progress bar by subscribing to the `OnProgressChanged` event of the `IHasProgress` component and sets the initial fill amount of the progress bar to 0.0. It also checks if the associated GameObject has a component implementing the `IHasProgress` interface.
- `private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)`: Event handler method called when the progress of the associated object changes. Updates the fill amount of the progress bar based on the normalized progress value provided in the event arguments. It also shows or hides the progress bar based on whether the progress is at its minimum or maximum value.
- `private void Show()`: Shows the progress bar by setting its GameObject active.
- `private void Hide()`: Hides the progress bar by setting its GameObject inactive.

#### Usage
This script is attached to a GameObject representing a progress bar in the UI. It requires references to a GameObject containing a component implementing the `IHasProgress` interface and an Image component representing the progress bar. The progress bar's fill amount is dynamically updated based on changes in the progress of the associated object.

#### Notes
- The `ProgressBarUI` script provides a flexible way to visualize the progress of various game elements that implement the `IHasProgress` interface, such as timers, completion rates, or other incremental progress indicators.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private IHasProgress hasProgress;
    [SerializeField] private Image barImage;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("Game Object has no components of IHasProgress");
        }

        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;

        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
```

---
