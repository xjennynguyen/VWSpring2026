using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TreeColorInteraction : MonoBehaviour
{
    public Color hoverColor = Color.yellow;
    public Color activeColor = new Color(0.5f, 1f, 0); // lime green

    private Renderer rend;
    private Color originalColor;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool active = false;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        interactable.activated.AddListener(OnActivate);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        rend.material.color = hoverColor;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        rend.material.color = active ? activeColor : originalColor;
    }

    private void OnActivate(ActivateEventArgs args)
    {
        active = !active;
        rend.material.color = active ? activeColor : originalColor;
    }
}
