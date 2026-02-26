using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    void Update()
    {
        // Find the Main Camera (the thing that actually moves in VR)
        Camera mainCam = Camera.main;
        
        if (mainCam != null)
        {
            Debug.Log("Camera position: " + mainCam.transform.position);
            Debug.Log("Finish spot position: " + transform.position);
            
            Vector3 camPos = new Vector3(mainCam.transform.position.x, 0, mainCam.transform.position.z);
            Vector3 spotPos = new Vector3(transform.position.x, 0, transform.position.z);
            
            float distance = Vector3.Distance(camPos, spotPos);
            Debug.Log("Distance: " + distance);
            
            if (distance < 0.5f)
            {
                Debug.Log("LOADING END SCENE!");
                SceneManager.LoadScene("EndScene");
            }
        }
    }
}