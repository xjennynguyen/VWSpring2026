using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MimeInteraction : MonoBehaviour
{
    [Header("Drag your UI Text here")]
    public GameObject uiText; // drag mime_hi text here

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        // Get the XR Simple Interactable component
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("XRBaseInteractable (or XR Simple Interactable) component missing on " + gameObject.name);
            return;
        }

        // Add event listener for selection
        interactable.selectEntered.AddListener(OnSelected);
    }

    // This runs when the player presses Trig
    private void OnSelected(SelectEnterEventArgs args)
    {
        if (uiText != null)
        {
            uiText.SetActive(true); // show text
            Debug.Log(gameObject.name + " interaction triggered!");
        }
        else
        {
            Debug.LogError("uiText is not assigned in MimeInteraction script!");
        }
    }
}
