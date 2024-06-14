using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        SavaGamenManager.Instance.SaveCurrentSceneName();
        Application.Quit();
    }

}
