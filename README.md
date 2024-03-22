# Code Name: ProjectChaos
---
## Table of Contents

### 1. Scripts
   - Camera
     - [1. LookAtCamera.cs](README.md#1-lookatcameracs) 
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
   - GameInput
      - [1. GameInput.cs](README.md#1-gameinputcs)
   - Interfaces
     - [1. IHasProgress.cs](README.md#1-ihasprogresscs)
     - [2. IKitchenObjectParent.cs](README.md#2-ikitchenobjectparentcs)
   - KitchenObject
      - [1. KitchenObject.cs](README.md#1-kitchenobjectcs)
   - LoadScreen
     - [1. Loader.cs](README.md#1-loadercs)
     - [2. LoaderCallback.cs](README.md#1-loadercallbackcs)
   - Managers
     - [1. DeliveryManager.cs](README.md#1-deliverymanagercs)
     - [2. KitchenGameManager.cs](README.md#2-kitchengamemanagercs)
     - [3. MusicManager.cs](README.md#3-musicmanagercs)
     - [4. ResetStaticDataManager.cs](README.md#4-resetstaticdatamanagercs)
     - [5. SoundManager.cs](README.md#5-soundmanagercs)
   - Player
     - [1. Player.cs](README.md#1-playercs)
     - [2. PlayerAnimator.cs](README.md#2-playeranimatorcs)
     - [3. PlayerSounds.cs](README.md#3-playersoundscs)
     - [4. SelectedCounterVisual.cs](README.md#4-selectedcountervisualcs)
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
      - [12. StoveBurnWarningUI.cs](README.md#12-stoveburnwarninguics)
      - [13. TutorialUI.cs](README.md#13-tutorialuics)
---
# Scripts
---
## Camera

### [1. LookAtCamera.cs](README.md#1-scripts)

#### Description
This script provides functionality to make an object look at the camera or align with the camera's forward direction. It offers different modes to achieve this behavior, including looking directly at the camera, looking at the camera with inverted orientation, aligning with the camera's forward direction, and aligning with the inverted camera's forward direction.

#### Fields
- `[SerializeField] private Mode mode`: Enum specifying the mode of operation for the LookAtCamera script.

#### Methods
- `private void LateUpdate()`: Unity lifecycle method called once per frame after all Update methods have been called. Switches between different modes to make the object look at or align with the camera based on the selected mode.

#### Usage
This script is attached to a GameObject in the scene that needs to dynamically adjust its orientation relative to the camera. By specifying different modes, developers can achieve various effects such as having objects always face the camera or aligning with the camera's direction.

#### Notes
- The LookAtCamera script is useful for creating dynamic and immersive experiences where objects need to maintain visual contact with the camera or align with its orientation.
- Different modes offer flexibility in achieving desired visual effects, such as providing options to invert the orientation or align with the camera's forward direction.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamer = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamer);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
```

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
    - If the player is empty-handed and the game is playing, and there are plates available on the counter:
        - Increases the count of plates spawned.
        - Triggers the `OnPlateSpawned` event.
- `private void OnPlateSpawned()`: Event handler method called when a plate is spawned on the plates counter. Increments the count of plates spawned.

#### Usage
This class is used to manage the spawning and removal of plates on a plates counter in the game environment.

#### Notes
- The `PlatesCounter` class provides functionality to handle interactions with plates, including spawning and removing plates from the counter.
- It tracks the number of plates spawned and ensures that plates are spawned within the specified limits.
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
This class represents a stove counter object. It inherits functionality from the `BaseCounter` class and implements the `IHasProgress` interface to track the progress of frying and burning operations. It manages the frying and burning states of kitchen objects placed on the stove counter.

#### Inherits from
- `BaseCounter`
- `IHasProgress`

#### Events
- `public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged`: Event triggered when the progress of frying or burning changes.
- `public event EventHandler<OnStateChangedEventArgs> OnStateChanged`: Event triggered when the state of the stove counter changes.

#### Nested Classes
- `public class OnStateChangedEventArgs : EventArgs`: Nested class representing event arguments for state change events.

#### Enums
- `public enum State`: Enumeration representing the possible states of the stove counter.
    - `Idle`: The stove counter is idle.
    - `Frying`: The stove counter is frying a kitchen object.
    - `Fried`: The kitchen object on the stove counter is fried.
    - `Burned`: The kitchen object on the stove counter is burned.

#### Fields
- `[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray`: Array of frying recipe scriptable objects.
- `[SerializeField] private BuringRecipeSO[] burningRecipeSOArray`: Array of burning recipe scriptable objects.
- `private State state`: Current state of the stove counter.
- `private float fryingTimer`: Timer for frying operation.
- `private FryingRecipeSO fryingRecipeSO`: Frying recipe scriptable object for the current frying operation.
- `private float buringTimer`: Timer for burning operation.
- `private BuringRecipeSO burningRecipeSO`: Burning recipe scriptable object for the current burning operation.

#### Methods
- `private void Start()`: Unity lifecycle method called before the first frame update. Initializes the stove counter with the idle state.
- `private void Update()`: Unity lifecycle method called once per frame. Updates the frying and burning operations based on the current state of the stove counter.
- `public override void Interact(Player player)`: Overrides the base class method to implement interaction behavior when a player interacts with the stove counter. Handles placing kitchen objects on the stove counter and managing frying and burning operations.
- `private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)`: Checks if there is a frying recipe available for the given input kitchen object.
- `private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)`: Gets the output kitchen object for the given input kitchen object based on the frying recipe.
- `private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the frying recipe scriptable object for the given input kitchen object.
- `private BuringRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)`: Retrieves the burning recipe scriptable object for the given input kitchen object.
- `public bool IsFried()`: Checks if the kitchen object on the stove counter is fried.

#### Usage
This class is used to manage frying and burning operations on a stove counter in the game environment. It allows players to interact with the stove counter by placing kitchen objects for frying and burning.

#### Notes
- The `StoveCounter` class provides functionality to simulate frying and burning operations on kitchen objects placed on a stove counter.
- It utilizes frying and burning recipe scriptable objects to determine the outcome of frying and burning operations based on the input kitchen object.
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

    public bool IsFried()
    {
        return state == State.Fried;
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
This class manages the sound effects for a stove counter object. It plays audio feedback based on the state and progress of the stove counter, such as when frying starts or when the cooking progress reaches a critical level.

#### Fields
- `[SerializeField] private StoveCounter stoveCounter`: Reference to the stove counter script.
- `private AudioSource audioSource`: Reference to the audio source component for playing sound effects.
- `private float warningSoundTimer`: Timer for triggering warning sounds.
- `private bool playWarningSound`: Flag indicating whether to play warning sounds based on the cooking progress.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the audio source component.
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to events triggered by the stove counter.
- `private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)`: Event handler method called when the state of the stove counter changes. Plays or pauses sound effects based on the stove counter's state.
- `private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)`: Event handler method called when the progress of the stove counter changes. Sets the flag to play warning sounds when the cooking progress reaches a critical level.
- `private void Update()`: Unity lifecycle method called once per frame. Updates the warning sound timer and plays warning sounds when triggered.

#### Usage
This class is used to provide audio feedback for the stove counter in the game environment. It plays sound effects to indicate when frying starts and when the cooking progress reaches a critical level, signaling that the food is close to being burned.

#### Notes
- The `StoveCounterSound` class enhances the immersion of the game by providing audio feedback for stove counter actions.
- It utilizes the audio source component to play sound effects based on the state and progress of the stove counter.
- Warning sounds are played when the cooking progress reaches a critical level, alerting the player to take action to prevent burning the food.


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
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = (e.state == StoveCounter.State.Frying) || (e.state == StoveCounter.State.Fried);

        if (playSound)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
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
## Game Input

### [1. GameInput.cs](README.md#1-scripts)

#### Description
The `GameInput` script manages player input actions and key bindings. It provides functionality to handle various input actions such as movement, interaction, and pausing the game. Additionally, it supports rebinding key bindings dynamically during runtime.

#### Fields
- `public static GameInput Instance`: Static reference to the singleton instance of the `GameInput` class.
- `public event EventHandler OnInteractAction`: Event triggered when the interact action is performed.
- `public event EventHandler OnInteractAlternateAction`: Event triggered when the alternate interact action is performed.
- `public event EventHandler OnPauseAction`: Event triggered when the pause action is performed.

#### Methods
- `public Vector2 GetMovementVectorNormalized()`: Returns the normalized movement vector input by the player.
- `public string GetBindingText(Binding binding)`: Returns the display string of the specified input binding.
- `public void RebindBinding(Binding binding, Action onActionRebound)`: Initiates the rebinding process for the specified input binding.

#### Usage
The `GameInput` script is responsible for managing player input and key bindings within the game. It allows developers to define custom input actions and key bindings, as well as handle events triggered by these actions.

#### Notes
- The `GameInput` class uses the Unity Input System to handle player input actions.
- It provides a flexible way to define and rebind input actions dynamically, enhancing player accessibility and customization options.
- Key bindings are saved and loaded using PlayerPrefs, allowing them to persist across game sessions.


#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindigs";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    public enum Binding 
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Gamepad_Interact,
        Gamepad_InteractAlternate
    }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Gamepad_Interact:
                return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;


        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => 
            {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }
}
```

---
## Interfaces

### [1. IHasProgress.cs](README.md#1-scripts)

#### Description
The `IHasProgress` interface defines functionality for objects that have progress, allowing them to notify listeners when their progress changes. It includes an event for handling progress changes, which provides information about the current progress normalized between 0 and 1.

#### Events
- `public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged`: Event triggered when the progress of an object implementing the interface changes. The event handler receives an argument containing the normalized progress value.

#### Usage
Implementing the `IHasProgress` interface in a class allows that class to notify other systems or objects when its progress changes. This is useful for various game elements such as loading bars, timers, or progress indicators.

#### Notes
- The `OnProgressChanged` event uses the `OnProgressChangedEventArgs` class to pass information about the progress change.
- By subscribing to the `OnProgressChanged` event, other classes can react to changes in the progress of objects implementing the `IHasProgress` interface.


#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
```

---
### [2. IKitchenObjectParent.cs](README.md#1-scripts)

#### Description
The `IKitchenObjectParent` interface defines methods for objects that can act as parents for kitchen objects in the game environment. It provides functionality for setting, retrieving, and managing kitchen objects within a parent object.

#### Methods
- `public Transform GetKitchenObjectFollowTransform()`: Retrieves the transform where kitchen objects should follow when placed within the parent.
- `public void SetKitchenObject(KitchenObject kitchenObject)`: Sets the specified kitchen object as a child of the parent object.
- `public KitchenObject GetKitchenObject()`: Retrieves the currently assigned kitchen object from the parent.
- `public void ClearKitchenObject()`: Clears the currently assigned kitchen object from the parent.
- `public bool HasKitchenObject()`: Checks whether the parent object currently has a kitchen object assigned to it.

#### Usage
Implementing the `IKitchenObjectParent` interface in a class allows that class to serve as a container or holder for kitchen objects. It provides methods for managing the interaction between kitchen objects and their parent objects, such as placing, retrieving, or clearing kitchen objects.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
```

---
## KitchenObject
### [1. KitchenObject.cs](README.md#1-scripts)

#### Description
The `KitchenObject` class represents an object in the kitchen environment. It provides functionality for setting a parent object, destroying itself, and spawning kitchen objects.

#### Fields
- `[SerializeField] private KitchenObjectSO kitchenObjectSO`: Serialized field representing the scriptable object for the kitchen object.

#### Methods
- `public KitchenObjectSO GetKitchenObjectSO()`: Retrieves the scriptable object associated with the kitchen object.
- `public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)`: Sets the specified object as the parent of the kitchen object, positioning it accordingly.
- `public IKitchenObjectParent GetKitchenObjectParent()`: Retrieves the parent object associated with the kitchen object.
- `public void DestroySelf()`: Destroys the kitchen object and clears its association with the parent object.
- `public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)`: Attempts to retrieve a plate kitchen object from the kitchen object.
- `public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)`: Instantiates a new kitchen object based on the provided scriptable object and sets its parent.

#### Usage
The `KitchenObject` class serves as the base class for various objects within the kitchen environment. It provides essential functionality for interacting with and managing kitchen objects, such as setting parent objects, destroying objects, and spawning new kitchen objects.

#### Notes
- The `KitchenObject` class is designed to be extended by specific kitchen objects, such as plates, ingredients, or utensils.
- It facilitates the management of kitchen objects within the game environment, including their instantiation, positioning, and destruction.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestorySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
```

---
## LoadScreen

### [1. Loader.cs](README.md#1-scripts)

#### Description
The `Loader` class provides functionality for loading different scenes within the game. It includes methods for loading specific scenes and handling scene loading callbacks.

#### Fields
- `public enum Scene`: Enumeration defining the available scenes, including the main menu, main game scene, and loading scene.
- `public static Scene targetScene`: Static field to store the target scene to be loaded.

#### Methods
- `public static void Load(Scene targetScene)`: Loads the specified target scene, setting it as the target scene to be loaded.
- `public static void LoaderCallback()`: Callback method called after the loading scene finishes loading. It loads the target scene stored in `targetScene`.

#### Usage
The `Loader` class simplifies the process of loading different scenes within the game. By using the provided `Load` method, developers can specify which scene to load, and the `LoaderCallback` method ensures that the target scene is loaded after the loading scene finishes loading.

#### Notes
- This class follows a static design pattern, providing a centralized way to manage scene loading throughout the game.
- It helps maintain cleaner and more organized scene management code by encapsulating loading logic in a separate class.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu,
        Main,
        LoadingScene
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
```

---
### [2. LoaderCallback.cs](README.md#1-scripts)

#### Description
The `LoaderCallback` class ensures that the target scene specified by the `Loader` class is loaded after the loading scene finishes loading. It executes the `Loader.LoaderCallback()` method once, immediately after the first update frame.

#### Fields
- `private bool isFirstUpdate`: Flag to track if the first update frame has occurred.

#### Methods
- `private void Update()`: Unity lifecycle method called once per frame. It checks if it's the first update frame and executes the `Loader.LoaderCallback()` method once to load the target scene.

#### Usage
This class is typically attached to a GameObject in the loading scene. It ensures that the target scene specified by the `Loader` class is loaded automatically after the loading scene finishes loading.

#### Notes
- By executing the callback method after the first update frame, this class ensures that any initialization or setup required for the target scene can be performed before the scene becomes active.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
```

---
## Managers

### [1. DeliveryManager.cs](README.md#1-scripts)

#### Description
The `DeliveryManager` class manages the delivery of recipes in the game. It spawns recipes at regular intervals and handles the delivery of recipes to plates. It tracks successful and failed recipe deliveries.

#### Fields
- `public static DeliveryManager Instance`: Singleton instance of the `DeliveryManager`.
- `[SerializeField] private RecipeListSO recipeListSO`: ScriptableObject containing a list of recipes.
- `[SerializeField] private float spawnRecipeTimerMax`: Maximum time interval between spawning recipes.
- `[SerializeField] int waitingRecipeMax`: Maximum number of waiting recipes allowed.

#### Events
- `public event EventHandler OnRecipeSpawned`: Event invoked when a new recipe is spawned.
- `public event EventHandler OnRecipeCompleted`: Event invoked when a recipe is successfully completed.
- `public event EventHandler OnRecipeSuccess`: Event invoked when a recipe is successfully delivered.
- `public event EventHandler OnRecipeFailed`: Event invoked when a recipe delivery fails.

#### Methods
- `private void Update()`: Updates the timer for spawning recipes and spawns a new recipe if conditions are met.
- `public void DeliverRecipe(PlateKitchenObject plateKitchenObject)`: Checks if the delivered plate matches any waiting recipe. Invokes events accordingly.
- `public List<RecipeSO> GetWaitingRecipeSOList()`: Returns the list of waiting recipes.
- `public int GetSuccessfulRecipesAmount()`: Returns the number of successful recipe deliveries.

#### Usage
This class is responsible for managing the delivery of recipes in the game. It spawns recipes at regular intervals and handles the delivery process. Other scripts can subscribe to its events to react to recipe spawning, successful completion, or failure.

#### Notes
- The `RecipeListSO` contains a list of recipes available for delivery.
- The `spawnRecipeTimerMax` determines the interval between recipe spawns.
- The `waitingRecipeMax` limits the number of waiting recipes at any given time.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float spawnRecipeTimerMax = 8f;
    [SerializeField] int waitingRecipeMax = 4;

    private float spawnRecipeTimer;
    private List<RecipeSO> waitingRecipeSOList;
    private int successfulRecipesAmount = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                //Has same number of ingredents
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    //Cycling through all the ingredients
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredients Match
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //This Recipe ingredient not found on plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //Player Delivered the correct recipe
                    successfulRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        //NoMatches Found
        OnRecipeFailed.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
```

---
### [2. KitchenGameManager.cs](README.md#1-scripts)

#### Description
The `KitchenGameManager` class manages the overall state of the kitchen game. It handles the game's lifecycle, including waiting for the game to start, starting a countdown, playing the game, and ending it when conditions are met. It also manages the game's pause state.

#### Fields
- `public static KitchenGameManager Instance`: Singleton instance of the `KitchenGameManager`.
- `public event EventHandler OnStateChanged`: Event invoked when the game state changes.
- `public event EventHandler OnGamePaused`: Event invoked when the game is paused.
- `public event EventHandler OnGameUnpaused`: Event invoked when the game is unpaused.
- `[SerializeField] private float countdownToStartTImer = 3f`: Duration of the countdown before the game starts.
- `[SerializeField] private float gamePlayingTimerMax = 10`: Maximum duration of the game playing phase.
- `private float gamePlayingTimer`: Current remaining time in the game playing phase.
- `private bool isGamePaused = false`: Flag indicating whether the game is currently paused.
- `private enum State`: Enumeration representing different states of the game.

#### Methods
- `private void Awake()`: Initializes the singleton instance and sets the initial game state.
- `private void Start()`: Subscribes to input events for pausing and starting the game.
- `private void GameInput_OnPauseAction(object sender, EventArgs e)`: Handles the pause action input event by toggling the game's pause state.
- `private void GameInput_OnInteractAction(object sender, EventArgs e)`: Handles the interact action input event by transitioning the game state from waiting to start to countdown to start.
- `private void Update()`: Updates the game state based on the current state.
- `public bool IsGamePlaying()`: Returns true if the game is currently in the playing state.
- `public bool IsCountdownToStartActive()`: Returns true if the game is currently in the countdown to start state.
- `public float GetCountdownToStatTimer()`: Returns the remaining time in the countdown to start.
- `public bool IsGameOver()`: Returns true if the game is over.
- `public float GetGamePlayingTimerNormalized()`: Returns the normalized value of the remaining time in the game playing phase.
- `public void TogglePauseGame()`: Toggles the pause state of the game and invokes corresponding events.

#### Usage
This class manages the overall state of the kitchen game. It controls the game lifecycle, including waiting for the game to start, starting a countdown, playing the game, and ending it when conditions are met. It also handles pausing and unpausing the game.

#### Notes
- Ensure that this script is attached to a GameObject that persists throughout the game session, such as a GameManager object.
- The countdownToStartTimer and gamePlayingTimerMax values can be adjusted in the Unity Editor to change the duration of the countdown and the maximum duration of the game playing phase, respectively.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    [SerializeField] private float countdownToStartTImer = 3f;
    [SerializeField] private float gamePlayingTimerMax = 10;

    private float gamePlayingTimer;
    private bool isGamePaused = false;


    private State state;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        gamePlayingTimer = gamePlayingTimerMax;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTImer -= Time.deltaTime;
                if (countdownToStartTImer < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStatTimer()
    {
        return countdownToStartTImer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimerMax;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
```

---
### [3. MusicManager.cs](README.md#1-scripts)

#### Description
The `MusicManager` class manages the music volume in the game. It allows changing the volume level and saves the user's preference using PlayerPrefs.

#### Fields
- `public static MusicManager Instance`: Singleton instance of the `MusicManager`.
- `private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume"`: Key for storing and retrieving the music volume from PlayerPrefs.
- `private AudioSource audioSource`: Reference to the AudioSource component attached to the GameObject.
- `private float volume = .5f`: Current volume level, initialized to 0.5f.

#### Methods
- `private void Awake()`: Initializes the singleton instance and retrieves the saved music volume from PlayerPrefs.
- `public void ChangeVolume()`: Increases the volume level by 0.1f and wraps around to 0f if it exceeds 1f. Updates the audio source volume and saves the new volume to PlayerPrefs.
- `public float GetVolume()`: Returns the current volume level.

#### Usage
This class can be attached to a GameObject in the scene to manage music volume. It provides methods to adjust the volume level dynamically during gameplay.

#### Notes
- Ensure that an AudioSource component is attached to the same GameObject as this script for music playback.
- The music volume level is persisted across game sessions using PlayerPrefs, allowing the user's preference to be remembered. Adjustments to the volume level can be made in runtime and are reflected immediately.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = .5f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        audioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
```

---
### [4. ResetStaticDataManager.cs](README.md#1-scripts)

#### Description
The `ResetStaticDataManager` class is responsible for resetting the static data of various counters in the game when the GameObject it is attached to is awakened.

#### Methods
- `private void Awake()`: Resets the static data of the CuttingCounter, BaseCounter, and TrashCounter by calling their respective ResetStaticData methods.

#### Usage
Attach this script to a GameObject in the scene. When the GameObject is awakened, it will reset the static data of the specified counters.

#### Notes
- This script assumes that the counters (CuttingCounter, BaseCounter, TrashCounter) have static ResetStaticData methods implemented to reset their static data.
- Ensure that this script is attached to a GameObject that is present in the scene at all times and will be awakened when required.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
```

---
### [5. SoundManager.cs](README.md#1-scripts)

#### Description
The `SoundManager` class manages sound effects in the game. It handles playing different audio clips for various events such as recipe success, recipe failure, chopping, object pickup, object placement, object trashing, and more.

#### Fields
- `public static SoundManager Instance`: Singleton instance of the `SoundManager`.
- `[SerializeField] private AudioClipRefsSO audioClipRefsSO`: ScriptableObject containing references to audio clips.
- `private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume"`: Key for storing sound effects volume in player preferences.
- `private float volume = 0.5f`: Current volume level for sound effects.

#### Methods
- `private void Awake()`: Initializes the singleton instance and retrieves the sound effects volume from player preferences.
- `private void Start()`: Subscribes to various events such as recipe success, recipe failure, chopping, object pickup, object placement, and object trashing.
- `private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)`: Plays a sound effect when a recipe is successfully delivered.
- `private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)`: Plays a sound effect when a recipe delivery fails.
- `private void CuttingCounter_OnAnyCut(object sender, EventArgs e)`: Plays a sound effect when cutting occurs.
- `private void Player_OnPickedSomething(object sender, EventArgs e)`: Plays a sound effect when the player picks up an object.
- `private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)`: Plays a sound effect when an object is placed on a base counter.
- `private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)`: Plays a sound effect when an object is trashed.
- `private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)`: Plays a single audio clip at a specified position with a specified volume.
- `private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)`: Plays a random audio clip from an array at a specified position with a specified volume multiplier.
- `public void PlayFooststepsSound(Vector3 position, float volume)`: Plays a footstep sound at a specified position with a specified volume.
- `public void PlayCountdownSound()`: Plays a countdown sound effect.
- `public void PlayWarningSound(Vector3 position)`: Plays a warning sound effect at a specified position.
- `public void ChangeVolume()`: Increases the volume of sound effects and saves the new volume to player preferences.
- `public float GetVolume()`: Returns the current volume level for sound effects.

#### Usage
Attach this script to a GameObject in the scene to manage sound effects. Assign appropriate audio clips to the `audioClipRefsSO` field in the inspector.

#### Notes
- Ensure that audio clips are properly assigned to the `audioClipRefsSO` ScriptableObject to ensure proper functionality.
- Adjust the volume levels of individual sound effects by adjusting the volume property in the Unity Editor or by calling the `ChangeVolume` method during runtime.

#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 0.5f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomehing += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySucess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFooststepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
```

---
## Player
### [1. Player.cs](README.md#1-scripts)

#### Description
The `Player` class represents the player character in the game. It handles player movement, interactions with kitchen objects, and selection of kitchen counters.

#### Fields
- `public static Player Instance`: Singleton instance of the `Player`.
- `public event EventHandler OnPickedSomehing`: Event invoked when the player picks up something.
- `public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged`: Event invoked when the selected kitchen counter changes.
- `[SerializeField] private float movementSpeed = 7f`: Speed of the player's movement.
- `[SerializeField] private float rotationSpeed = 10f`: Speed of the player's rotation.
- `[SerializeField] private GameInput gameInput`: Reference to the `GameInput` component for player input.
- `[SerializeField] private LayerMask countersLayerMask`: Layer mask for detecting kitchen counters.
- `[SerializeField] private Transform kitchenObjectHoldPoint`: Hold point for kitchen objects picked up by the player.
- `private bool isWalking`: Flag indicating whether the player is currently walking.
- `private Vector3 lastInteractDir`: Last direction of interaction.
- `private BaseCounter selectedCounter`: Currently selected kitchen counter.
- `private KitchenObject kitchenObject`: Kitchen object currently held by the player.

#### Methods
- `private void Awake()`: Initializes the singleton instance of the player.
- `private void Start()`: Subscribes to input events for player interactions.
- `private void GameInput_OnInteractAction(object sender, EventArgs e)`: Handles the primary interact action input event.
- `private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)`: Handles the alternate interact action input event.
- `private void Update()`: Updates player movement and interactions.
- `private void PlayerMovementInput()`: Handles player movement based on input.
- `private void PlayerInteractions()`: Handles player interactions with kitchen counters.
- `private void SetSelectedCounter(BaseCounter selectedCounter)`: Sets the currently selected kitchen counter.
- `public Transform GetKitchenObjectFollowTransform()`: Returns the hold point transform for kitchen objects.
- `public void SetKitchenObject(KitchenObject kitchenObject)`: Sets the kitchen object currently held by the player.
- `public KitchenObject GetKitchenObject()`: Returns the kitchen object held by the player.
- `public void ClearKitchenObject()`: Clears the kitchen object held by the player.
- `public bool HasKitchenObject()`: Returns true if the player is currently holding a kitchen object.

#### Usage
Attach this script to the player GameObject in the scene to control player movement and interactions with kitchen objects. Assign the necessary references in the inspector, such as the `GameInput` component and the kitchen object hold point.

#### Notes
- Ensure that the player GameObject has a collider to detect collisions with kitchen counters.
- Adjust movement and rotation speeds in the inspector to achieve the desired player behavior.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    private static Player instance;
    public static Player Instance { get { return instance; } private set { instance = value; } }

    public event EventHandler OnPickedSomehing;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    public bool IsWalking { get { return isWalking; } }
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update()
    {
        PlayerMovementInput();
        PlayerInteractions();
    }

    private void PlayerMovementInput()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = movementSpeed * Time.deltaTime;
        float playerRadius = .65f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //Don't Move
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void PlayerInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomehing?.Invoke(this, EventArgs.Empty);
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
### [2. PlayerAnimator.cs](README.md#1-scripts)

#### Description
The `PlayerAnimator` class controls the animation of the player character based on the player's movement state.

#### Fields
- `[SerializeField] private Player player`: Reference to the `Player` component for accessing player movement state.
- `private Animator animator`: Reference to the Animator component attached to the GameObject.

#### Methods
- `private void Awake()`: Initializes the Animator component reference.
- `private void Update()`: Updates the "IsWalking" parameter of the Animator based on the player's movement state.

#### Usage
Attach this script to the GameObject representing the player character in the scene. Assign the `Player` component reference in the inspector to link it with the player's movement state.

#### Notes
- Ensure that the Animator component attached to the player GameObject has a boolean parameter named "IsWalking" to control the walking animation.
- This script updates the "IsWalking" parameter of the Animator based on the player's movement state, enabling or disabling the walking animation accordingly.

#### Code 
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
}
```

---
### [3. PlayerSounds.cs](README.md#1-scripts)

#### Description
The `PlayerSounds` class manages player-related sounds, such as footsteps, based on the player's movement state.

#### Fields
- `[SerializeField] private float footstepTimerMax = .1f`: Maximum duration between footstep sounds.
- `[SerializeField] private float footstepVolume = 1f`: Volume of the footstep sounds.
- `private Player player`: Reference to the `Player` component to access the player's movement state.
- `private float footstepTimer`: Timer to control the interval between footstep sounds.

#### Methods
- `private void Awake()`: Initializes the `Player` component reference.
- `private void Update()`: Updates the footstep sounds based on the player's movement state.

#### Usage
Attach this script to the GameObject representing the player character in the scene. Adjust the `footstepTimerMax` and `footstepVolume` parameters in the inspector to control the frequency and volume of footstep sounds.

#### Notes
- This script assumes that there is a `Player` component attached to the same GameObject.
- Ensure that the `SoundManager` singleton instance is properly set up to handle footstep sounds.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    [SerializeField] private float footstepTimerMax = .1f;
    [SerializeField] private float footstepVolume = 1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking)
            {
                SoundManager.Instance.PlayFooststepsSound(player.transform.position, footstepVolume);
            }
        }
    }


}
```

---
### [4. SelectedCounterVisual.cs](README.md#1-scripts)

#### Description
The `SelectedCounterVisual` class controls the visibility of visual elements associated with a specific base counter when it is selected by the player.

#### Fields
- `[SerializeField] private BaseCounter baseCounter`: Reference to the base counter associated with this visual component.
- `[SerializeField] private GameObject[] visualGameObjectArray`: Array of visual game objects to control visibility.

#### Methods
- `private void Start()`: Subscribes to the `OnSelectedCounterChanged` event of the `Player` instance.
- `private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)`: Event handler method invoked when the selected counter changes. Shows or hides the visual elements based on whether the selected counter matches the associated base counter.
- `private void Show()`: Sets the `active` property of each visual game object in the array to true, making them visible.
- `private void Hide()`: Sets the `active` property of each visual game object in the array to false, hiding them.

#### Usage
Attach this script to the GameObject containing the visual elements associated with a specific base counter. Assign the corresponding base counter and visual game objects to the `baseCounter` and `visualGameObjectArray` fields in the inspector.

#### Notes
- This script assumes that there is a `Player` singleton instance in the scene.
- Visual game objects should be properly configured in the scene and linked to this script.

#### Code 
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
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
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        Instance = this;

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
            Hide();
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

    public void Show()
    {
        gameObject.SetActive(true);

        //Controller
        //resumeButton.Select();
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
This script manages the countdown UI displayed at the start of the game. It updates the countdown text based on the countdown timer from the `KitchenGameManager` and triggers animations for each countdown number change. Additionally, it plays a countdown sound effect when the countdown number changes.

#### Fields
- `private const string NUMBER_POPUP = "NumberPopup"`: Constant string representing the trigger name for the number popup animation in the animator.
- `[SerializeField] private TextMeshProUGUI countdownText`: Reference to the TextMeshProUGUI component displaying the countdown text.
- `private Animator animator`: Reference to the Animator component for triggering countdown animations.
- `private int previousCountDownNumeber`: Variable to store the previous countdown number.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the animator reference.
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to game state change events and hides the countdown UI initially.
- `private void Update()`: Unity lifecycle method called once per frame. Updates the countdown text based on the countdown timer from `KitchenGameManager` and triggers animations for countdown number changes.
- `private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)`: Event handler method called when the game state changes. Shows or hides the countdown UI based on the game state.
- `private void Show()`: Shows the countdown UI by setting its GameObject active.
- `private void Hide()`: Hides the countdown UI by setting its GameObject inactive.

#### Usage
This script is attached to a GameObject representing the countdown UI in the game scene. It dynamically updates the countdown text based on the countdown timer provided by `KitchenGameManager` and triggers animations for countdown number changes. The countdown UI is displayed at the start of the game and hidden when the game starts playing.

#### Notes
- The countdown UI enhances the game's visual presentation by providing players with a clear indication of when the game will start.
- It utilizes animations and sound effects to create an engaging countdown experience for players.


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
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private int previousCountDownNumeber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStatTimer());
        countdownText.text = countDownNumber.ToString();

        if (previousCountDownNumeber != countDownNumber)
        {
            previousCountDownNumeber = countDownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
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
This script manages the options menu UI, allowing players to adjust game settings such as sound effects volume, music volume, and key bindings. It provides functionality to rebind key bindings interactively and updates the UI to reflect changes in settings and key bindings.

#### Fields
- `public static OptionsUI Instance { get; private set; }`: Static reference to the OptionsUI instance.
- `[SerializeField] private Button soundEffectsButton`: Reference to the button for adjusting sound effects volume.
- `[SerializeField] private Button musicButton`: Reference to the button for adjusting music volume.
- `[SerializeField] private Button closeButton`: Reference to the button for closing the options menu.
- `[SerializeField] private TextMeshProUGUI soundEffectsText`: Reference to the TextMeshProUGUI for displaying sound effects volume.
- `[SerializeField] private TextMeshProUGUI musicText`: Reference to the TextMeshProUGUI for displaying music volume.
- `[SerializeField] private Button moveUpButton`: Reference to the button for rebinding the move up key.
- `[SerializeField] private Button moveDownButton`: Reference to the button for rebinding the move down key.
- `[SerializeField] private Button moveLeftButton`: Reference to the button for rebinding the move left key.
- `[SerializeField] private Button moveRightButton`: Reference to the button for rebinding the move right key.
- `[SerializeField] private Button interactButton`: Reference to the button for rebinding the interact key.
- `[SerializeField] private Button interactAlternateButton`: Reference to the button for rebinding the alternate interact key.
- `[SerializeField] private TextMeshProUGUI moveUpText`: Reference to the TextMeshProUGUI for displaying the move up key binding.
- `[SerializeField] private TextMeshProUGUI moveDownText`: Reference to the TextMeshProUGUI for displaying the move down key binding.
- `[SerializeField] private TextMeshProUGUI moveLeftText`: Reference to the TextMeshProUGUI for displaying the move left key binding.
- `[SerializeField] private TextMeshProUGUI moveRightText`: Reference to the TextMeshProUGUI for displaying the move right key binding.
- `[SerializeField] private TextMeshProUGUI interactText`: Reference to the TextMeshProUGUI for displaying the interact key binding.
- `[SerializeField] private TextMeshProUGUI interactAlternateText`: Reference to the TextMeshProUGUI for displaying the alternate interact key binding.
- `[SerializeField] private Transform pressToRebindKeyTransform`: Reference to the transform for showing the press to rebind key UI.

#### Methods
- `private void Awake()`: Unity lifecycle method called when the script instance is being loaded. Initializes the static reference to the OptionsUI instance.
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to game unpaused event and updates the visual elements of the options menu.
- `private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)`: Event handler method called when the game is unpaused. Hides the options menu.
- `private void UpdateVisual()`: Updates the visual elements of the options menu to reflect changes in settings and key bindings.
- `public void Show()`: Shows the options menu by setting its GameObject active.
- `private void Hide()`: Hides the options menu by setting its GameObject inactive.
- `private void ShowPressToRebindKey()`: Shows the press to rebind key UI by setting its GameObject active.
- `private void HidePressToRebindKey()`: Hides the press to rebind key UI by setting its GameObject inactive.
- `private void RebindBinding(GameInput.Binding binding)`: Initiates the process of rebinding a key binding. Shows the press to rebind key UI and calls the `RebindBinding` method of the `GameInput` class to rebind the specified binding.

#### Usage
This script is attached to a GameObject representing the options menu in the game scene. It provides players with options to adjust sound effects volume, music volume, and key bindings. Players can interactively rebind key bindings by clicking on the respective buttons and pressing the desired key. The options menu can be opened and closed during gameplay.

#### Notes
- The OptionsUI enhances the player experience by providing customizable settings and key bindings.
- It includes functionality for interactive key binding reassignment, improving accessibility and customization options for players.


#### Code
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    //[SerializeField] private Button gamepadInteractButton;
    //[SerializeField] private Button gamepadInteractAlternateButton;

    //[SerializeField] private TextMeshProUGUI gamepadinteractText;
    //[SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;

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
            GamePauseUI.Instance.Show();
        });

        moveUpButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAlternateButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
        /*
        gamepadInteractButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        gamepadInteractAlternateButton.onClick.AddListener(() => 
        {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        */
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();

        HidePressToRebindKey();

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

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        //gamepadInteractText = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        //gamepadInteractAlternateText = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        //Controller
        //soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
        
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => 
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
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
### [12. StoveBurnWarningUI.cs](README.md#1-scripts)

#### Description
This script manages the warning UI for the stove counter, displaying a warning when the progress of frying a plate approaches the burning threshold. It listens to the progress changed event of the stove counter and shows or hides the warning UI based on the progress value.

#### Fields
- `[SerializeField] private StoveCounter stoveCounter`: Reference to the stove counter component.

#### Methods
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to the progress changed event of the stove counter and hides the warning UI.
- `private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)`: Event handler method called when the progress of the stove counter changes. Determines whether to show or hide the warning UI based on the progress value.
- `private void Show()`: Shows the warning UI by setting its GameObject active.
- `private void Hide()`: Hides the warning UI by setting its GameObject inactive.

#### Usage
This script is attached to a GameObject representing the warning UI for the stove counter in the game scene. It dynamically displays a warning when the progress of frying a plate approaches the burning threshold, providing feedback to the player about the state of the cooking process.

#### Notes
- The StoveBurnWarningUI enhances player awareness by visually indicating when a plate is close to burning during the cooking process.
- It complements the stove counter functionality by providing timely warnings to prevent burning and maintain gameplay balance.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
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
### [13. TutorialUI.cs](README.md#1-scripts)

#### Description
This script manages the tutorial UI, displaying key bindings for player actions to assist players in learning the game controls. It listens to events related to game state changes and binding rebinds and updates the UI accordingly.

#### Fields
- `[SerializeField] private TextMeshProUGUI moveUpKeyText`: Text element displaying the key binding for moving up.
- `[SerializeField] private TextMeshProUGUI moveDownKeyText`: Text element displaying the key binding for moving down.
- `[SerializeField] private TextMeshProUGUI moveLeftKeyText`: Text element displaying the key binding for moving left.
- `[SerializeField] private TextMeshProUGUI moveRightKeyText`: Text element displaying the key binding for moving right.
- `[SerializeField] private TextMeshProUGUI interactKeyText`: Text element displaying the key binding for interacting.
- `[SerializeField] private TextMeshProUGUI interactAlternateKeyText`: Text element displaying the key binding for alternate interaction.
- `[SerializeField] private TextMeshProUGUI gamepadInteactKeyText`: Text element displaying the key binding for gamepad interaction.
- `[SerializeField] private TextMeshProUGUI gamepadInteractAlternateKeyText`: Text element displaying the key binding for alternate gamepad interaction.

#### Methods
- `private void Start()`: Unity lifecycle method called before the first frame update. Subscribes to events related to binding rebinds and game state changes, updates the UI, and shows the tutorial UI.
- `private void GameInput_OnBindingRebind(object sender, System.EventArgs e)`: Event handler method called when a binding is rebound. Updates the visual representation of the key bindings.
- `private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)`: Event handler method called when the game state changes. Hides the tutorial UI when the countdown to start is active.
- `private void UpdateVisual()`: Updates the visual representation of the key bindings based on the current bindings.
- `private void Show()`: Shows the tutorial UI by setting its GameObject active.
- `private void Hide()`: Hides the tutorial UI by setting its GameObject inactive.

#### Usage
This script is attached to a GameObject representing the tutorial UI in the game scene. It dynamically displays key bindings for player actions to help players learn the game controls.

#### Notes
- The TutorialUI provides a helpful guide for players to learn and remember the game controls, improving the overall user experience.
- It updates in real-time to reflect any changes made to the key bindings, ensuring accuracy and relevance to the current configuration.

#### Code
```
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUpKeyText;
    [SerializeField] private TextMeshProUGUI moveDownKeyText;
    [SerializeField] private TextMeshProUGUI moveLeftKeyText;
    [SerializeField] private TextMeshProUGUI moveRightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI interactAlternateKeyText;
    [SerializeField] private TextMeshProUGUI gamepadInteactKeyText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateKeyText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void UpdateVisual()
    {
        moveUpKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        gamepadInteactKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);

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



