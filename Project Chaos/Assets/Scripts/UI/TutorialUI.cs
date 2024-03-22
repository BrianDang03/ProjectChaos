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
