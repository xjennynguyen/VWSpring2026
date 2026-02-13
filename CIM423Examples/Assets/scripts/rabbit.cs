using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
[RequireComponent(typeof(Collider))]
public class RabbitInteraction : MonoBehaviour
{
    public GameObject uiText;
    public Color hoverColor = new Color(1f, 0.8f, 0.5f);
    public Transform foodSpot;
    public float moveSpeed = 1f;
    public float reachDistance = 0.1f;

    private Renderer[] renderers;
    private Color[] originalColors;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool isWalking = false;

    void Awake()
    {
        // find all child renderers
        renderers = GetComponentsInChildren<Renderer>(true);
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].material.color;

        // XR Interactable
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        interactable.selectEntered.AddListener(OnTriggerPressed);

        // hide text at start
        if (uiText != null)
            uiText.SetActive(false);
    }

    void Update()
    {
        if (isWalking && foodSpot != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, foodSpot.position, moveSpeed * Time.deltaTime);

            Vector3 dir = foodSpot.position - transform.position;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(dir);

            if (Vector3.Distance(transform.position, foodSpot.position) < reachDistance)
            {
                isWalking = false;
                if (uiText != null)
                    uiText.SetActive(true);
            }
        }
    }

    private void OnTriggerPressed(SelectEnterEventArgs args)
    {
        isWalking = true;
        if (uiText != null)
            uiText.SetActive(false);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        foreach (Renderer r in renderers)
            r.material.color = hoverColor;  // highlight
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];  // reset
    }
}
