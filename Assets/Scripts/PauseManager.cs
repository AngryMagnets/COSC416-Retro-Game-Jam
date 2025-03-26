using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private InputManager inputManager;
    public static PauseManager instance;

    private bool isSettingsMenuActive;
    public bool IsSettingsMenuActive => isSettingsMenuActive;

    private void Awake()
    {
        instance = this;
        DisableSettingsMenu();
        inputManager.OnPause.AddListener(EnableSettingsMenu);
    }

    public void ToggleSettingsMenu()
    {
        if (isSettingsMenuActive)
        {
            DisableSettingsMenu();
        }
        else
        {
            EnableSettingsMenu();
        }
    }

    public void EnableSettingsMenu()
    {
        Time.timeScale = 0f;
        settingsMenu.SetActive(true);
        isSettingsMenuActive = true;
    }
    public void DisableSettingsMenu()
    {
        Time.timeScale = 1f;
        settingsMenu.SetActive(false);
        isSettingsMenuActive = false;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
