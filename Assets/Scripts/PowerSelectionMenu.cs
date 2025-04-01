using UnityEngine;

public class PowerSelectionMenu : MonoBehaviour
{
    public void PowerSelected(int p)
    {
        PowerUpVaraible.InitializePowers(p);
        SceneNavigator.Navigator.LoadNextScene();
    }
}
