using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
[RequireComponent(typeof(Collider))]
public class DumpsterInteraction : MonoBehaviour
{
    [Header("Canvas to show")]
    public GameObject dumpsterCanvas;

    [Header("Hover settings")]
    public Color hoverColor = Color.yellow;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private Renderer[] renderers;
    private Color[] originalColors;

    void Awake()
    {
        // Get all child renderers for hover highlight
        renderers = GetComponentsInChildren<Renderer>(true);
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;

        // XR Interactable
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        interactable.selectEntered.AddListener(OnTriggerPressed);

        // Hide canvas at start
        if (dumpsterCanvas != null)
            dumpsterCanvas.SetActive(false);
    }

    // Trigger pressed → show Canvas
    private void OnTriggerPressed(SelectEnterEventArgs args)
    {
        if (dumpsterCanvas != null)
            dumpsterCanvas.SetActive(true);
    }

    // Hover enter → highlight
    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (Renderer r in renderers)
            r.material.color = hoverColor;
    }

    // Hover exit → reset colors
    private void OnHoverExit(HoverExitEventArgs args)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }
}
