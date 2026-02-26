using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void StartExperience()
    {
        SceneManager.LoadScene("IntroductionVR");
    }
}
