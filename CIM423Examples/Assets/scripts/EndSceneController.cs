using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    // Called by the Restart button
    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    // Called by a Main Menu button (optional)
    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
