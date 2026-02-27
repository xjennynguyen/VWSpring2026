using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    bool trigger = false;
    void Start()
    {
        
        
        DontDestroyOnLoad(this.gameObject);

        
    }

    void Update(){
        if(SceneManager.GetActiveScene().name == "EndScene" && trigger == false) {
            Debug.Log("move....");
            // grab
            transform.position = new Vector3(-5.3f,0.0f,6.25f);
            trigger = true;
        }
    }
}
