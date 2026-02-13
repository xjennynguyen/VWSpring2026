using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MonkeyInteraction : MonoBehaviour
{
    [Header("Text to show when Trig is pressed")]
    public GameObject uiText;  // drag your 3D Text here

    [Header("Hover highlight settings")]
    public Color hoverColor = Color.yellow;

    [Header("Jump settings")]
    public float jumpHeight = 0.5f;
    public float jumpSpeed = 5f;

    private Renderer[] renderers;
    private Color[] originalColors;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private bool isActive = false;       // tracks jump + text state
    private float jumpTimer = 0f;
    private Vector3 startPosition;

    void Start()
    {
        if (uiText != null)
            uiText.SetActive(false);

        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnTriggerPressed);  // toggle on trigger
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
        }
        else
        {
            Debug.LogError("XR Simple Interactable missing on " + gameObject.name);
        }

        startPosition = transform.position; // store original position for jumping
    }

    void Update()
    {
        if (isActive)
        {
            jumpTimer += Time.deltaTime * jumpSpeed;
            float yOffset = Mathf.Abs(Mathf.Sin(jumpTimer)) * jumpHeight;
            transform.position = startPosition + new Vector3(0, yOffset, 0);
        }
    }

    private void OnTriggerPressed(SelectEnterEventArgs args)
    {
        isActive = !isActive; // toggle jump + text

        if (uiText != null)
            uiText.SetActive(isActive);

        if (!isActive)
            transform.position = startPosition; // reset position when stopping
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
