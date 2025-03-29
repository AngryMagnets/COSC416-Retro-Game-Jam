using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    [SerializeReference]
    public static SceneNavigator Navigator; 
    public Scene CurrentScene { get; private set; }
    
    [SerializeField]


    private void Start () { CurrentScene = SceneManager.GetActiveScene(); }
    protected void Awake() { Navigator = GetComponent<SceneNavigator>(); }
    void LoadNextScene ()
    {
        int sceneId = CurrentScene.buildIndex + 1;
        try
        {
            SceneManager.LoadSceneAsync(sceneId);
        }
        catch (System.Exception)
        {
            Debug.LogError("Next scene does not exist in build profile");
        }
    }
    void LoadPrevScene()
    {
        int sceneId = CurrentScene.buildIndex - 1;
        try
        {
            SceneManager.LoadSceneAsync(sceneId);
        }
        catch (System.Exception)
        {
            Debug.LogError("Prev scene does not exist in build profile");
        }
    }

    void LoadScene(int sceneId)
    {
        CurrentScene = SceneManager.GetSceneAt(sceneId);
        SceneManager.LoadSceneAsync(CurrentScene.buildIndex);
    }
    void LoadScene(Scene scene)
    {
        CurrentScene = scene;
        SceneManager.LoadSceneAsync(CurrentScene.buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
