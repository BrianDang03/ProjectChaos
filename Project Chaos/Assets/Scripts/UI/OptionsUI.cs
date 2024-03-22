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
