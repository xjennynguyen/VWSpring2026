using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashBagInteraction : MonoBehaviour
{
    [Header("Hover highlight settings")]
    public Color hoverColor = Color.yellow;

    [Header("Move settings")]
    public Vector3 moveOffset = new Vector3(0, 0, 0.05f); // small forward move
    public float moveSpeed = 5f; // speed of movement

    private Renderer[] renderers;
    private Color[] originalColors;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private bool isActive = false;
    private float moveTimer = 0f;
    private Vector3 startPosition;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
            interactable.selectEntered.AddListener(OnTriggerPressed);
        }
        else
        {
            Debug.LogError("XR Simple Interactable missing on " + gameObject.name);
        }

        startPosition = transform.position;
    }

    void Update()
    {
        if (isActive)
        {
            moveTimer += Time.deltaTime * moveSpeed;
            float zOffset = Mathf.Abs(Mathf.Sin(moveTimer)) * moveOffset.z;
            transform.position = startPosition + new Vector3(moveOffset.x, moveOffset.y, zOffset);
        }
    }

    private void OnTriggerPressed(SelectEnterEventArgs args)
    {
        isActive = !isActive;

        if (!isActive)
            transform.position = startPosition; // reset position
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (Renderer r in renderers)
            r.material.color = hoverColor;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }
}