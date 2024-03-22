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
