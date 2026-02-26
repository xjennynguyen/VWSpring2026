using UnityEngine;

public class StartPosition : MonoBehaviour
{
    void Start()
    {
        // Reset the XR Origin to starting position
        transform.position = new Vector3(0, 0, 0);
        
        // Also reset the Camera Offset if it exists
        Transform cameraOffset = transform.Find("Camera Offset");
        if (cameraOffset != null)
        {
            cameraOffset.localPosition = Vector3.zero;
        }
    }
}