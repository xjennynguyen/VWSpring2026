using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PandaInteraction : MonoBehaviour
{
    [Header("Text to show when Trig is pressed")]
    public GameObject uiText;  // drag your 3D Text here

    [Header("Hover highlight settings")]
    public Color hoverColor = new Color(1f, 0.5f, 1f);
    

    private Renderer[] renderers;
    private Color[] originalColors;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Start()
    {
        // Check if uiText is assigned
        if (uiText == null)
        {
            Debug.LogError("uiText not assigned on " + gameObject.name);
        }
        else
        {
            // Make sure text is hidden at start
            uiText.SetActive(false);
        }

        // Get all child renderers for hover highlighting
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }

        // Get XR Simple Interactable
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelected);  // Trig press
            interactable.hoverEntered.AddListener(OnHoverEnter); // hover
            interactable.hoverExited.AddListener(OnHoverExit);   // stop hover
        }
        else
        {
            Debug.LogError("XR Simple Interactable missing on " + gameObject.name);
        }
    }

    // Trig pressed → show text
    private void OnSelected(SelectEnterEventArgs args)
    {
        if (uiText != null)
            uiText.SetActive(true);
    }

    // Hover near Panda → highlight
    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (Renderer r in renderers)
            r.material.color = hoverColor;
    }

    // Leave hover → restore original color
    private void OnHoverExit(HoverExitEventArgs args)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }
}
