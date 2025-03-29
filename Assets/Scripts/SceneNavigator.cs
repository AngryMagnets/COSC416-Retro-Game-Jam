using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// So long as this exists in as an object in every scene it *should* work just fine
/// </summary>
public class SceneNavigator : MonoBehaviour
{
    [SerializeReference]
    public static SceneNavigator Navigator;
    
    [SerializeField]
    public Scene CurrentScene { get; private set; }

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
        }
        catch (System.Exception e)
        {
            Debug.LogError($"{e.GetType().ToString()} occured while loading scene {sceneId}");
            Debug.LogException(e);
        }
        finally
        {
            QuitGame();
        }
    }

    public void LoadNextScene ()
    {
        HandleErrorOnLoad(CurrentScene.buildIndex + 1);
    }
    public void LoadPrevScene()
    {
        HandleErrorOnLoad(CurrentScene.buildIndex - 1);
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
