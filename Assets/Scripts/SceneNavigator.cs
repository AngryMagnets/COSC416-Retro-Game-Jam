using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// So long as this exists in as an object in every scene it *should* work just fine
/// Due to an alternate plan of how peg layouts will be placed swapped between, additional functionality needs to bed added.
/// Additionally, Certain methods can be renamed to navigate to a certain scene rather than previous or last, if deemed necessary
/// </summary>
public class SceneNavigator : MonoBehaviour
{
    [SerializeReference]
    public static SceneNavigator Navigator;
    
    [SerializeField]
    public static Scene CurrentScene { get; private set; }
    [SerializeField]
    private static int numLevels;
    private static int levelsCompleted, currentIdx;
    
    /// <summary>
    /// A list for prefabs of a peg layout and their associated background to be stored in.
    /// <code LoadNewPegLayout/> will replace the currentLayout with one from this list
    /// </summary>
    [SerializeField]
    private List<GameObject> layouts = new List<GameObject>();
    private GameObject currentLayout = null;


    private void Start() { CurrentScene = SceneManager.GetActiveScene(); }
    protected void Awake() { Navigator = GetComponent<SceneNavigator>(); }

    private void HandleErrorOnLoad(int sceneId)
    {
        try
        {
            CurrentScene = SceneManager.GetSceneByBuildIndex(sceneId);
            SceneManager.LoadSceneAsync(sceneId);
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.LogError($"Prev scene: {sceneId} does not exist in build profile");
            QuitGame();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"{e.GetType().ToString()} occured while loading scene {sceneId}");
            Debug.LogException(e);
            QuitGame();
        }
    }

    public void LoadNextScene ()
    {
        HandleErrorOnLoad(CurrentScene.buildIndex + 1);
    }
    /// <summary>
    /// Loads a peg layout from the given prefabs
    /// </summary>
    public void LoadNewPegLayout ()
    {
        int idx;
        do { idx = Random.Range(0, layouts.Count); } while (currentIdx == idx);
        levelsCompleted++;
        
        Destroy(currentLayout);
        currentLayout = (GameObject)Instantiate(layouts[idx], CurrentScene);
    }
    public void LoadPrevScene()
    {
        HandleErrorOnLoad(CurrentScene.buildIndex - 1);
    }
    public void LoadMainMenu()
    {
        levelsCompleted = 0;
        HandleErrorOnLoad(0);
    }

    public void LoadScene(int sceneId)
    {
        HandleErrorOnLoad(sceneId);
    }
    public void LoadScene(Scene scene)
    {
        HandleErrorOnLoad(scene.buildIndex);
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

// Code from: https://discussions.unity.com/t/how-to-get-names-of-all-available-levels/9749
// Probably won't need it if only working with scenes in the build profile
//public class ReadSceneNames : MonoBehaviour
//{
//    public string[] scenes;
//#if UNITY_EDITOR
//    private static string[] ReadNames()
//    {
//        List<string> temp = new List<string>();
//        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
//        {
//            if (S.enabled)
//            {
//                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
//                name = name.Substring(0, name.Length - 6);
//                temp.Add(name);
//            }
//        }
//        return temp.ToArray();
//    }
//    [UnityEditor.MenuItem("CONTEXT/ReadSceneNames/Update Scene Names")]
//    private static void UpdateNames(UnityEditor.MenuCommand command)
//    {
//        ReadSceneNames context = (ReadSceneNames)command.context;
//        context.scenes = ReadNames();
//    }

//    private void Reset()
//    {
//        scenes = ReadNames();
//    }
//#endif
//}
