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
        Pause();
        settingsMenu.SetActive(true);
    }
    public void DisableSettingsMenu()
    {
        UnPause();
        settingsMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isSettingsMenuActive = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        isSettingsMenuActive = false;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
