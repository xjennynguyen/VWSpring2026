using UnityEngine;
using System.Collections;

public class ResetPlayerPosition : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ResetSequence());
    }
    
    IEnumerator ResetSequence()
    {
        // HIDE the finish_spot temporarily
        GameObject finishSpot = GameObject.Find("finish_spot");
        if (finishSpot != null)
        {
            finishSpot.SetActive(false); // Turn it OFF
            Debug.Log("Finish spot disabled temporarily");
        }
        
        // Wait a moment
        yield return new WaitForSeconds(0.5f);
        
        // Reset camera
        transform.position = Vector3.zero;
        Transform cameraOffset = transform.Find("Camera Offset");
        if (cameraOffset != null)
        {
            cameraOffset.localPosition = new Vector3(0, 1.6f, 0);
        }
        
        // Wait another moment
        yield return new WaitForSeconds(0.5f);
        
        // Turn finish_spot back ON
        if (finishSpot != null)
        {
            finishSpot.SetActive(true);
            Debug.Log("Finish spot re-enabled");
        }
    }
}