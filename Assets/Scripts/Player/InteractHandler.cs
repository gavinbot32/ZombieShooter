using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractHandler : MonoBehaviour
{
    public Interactable curInteract;
    public PlayerController player;
    private GameObject interactGO;
    [SerializeField] private float interactDist;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera cam;

    [Header("Interact Pip")]
    public Transform interactPip;
    public TextMeshPro interactLabelText;
    public TextMeshPro interactText;

    private void Start()
    {
        player = this.SafeGetComponent(player);
    }
    private void FixedUpdate()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, interactDist, layerMask))
        {
            if (hit.collider.gameObject != interactGO)
            {
                curInteract = hit.collider.gameObject.GetComponent<Interactable>();
                interactGO = hit.collider.gameObject;
            }
            
        }
        else
        {
            curInteract = null;
            interactGO = null;
        }

        

    }

    private void Update()
    {
        interactPip.gameObject.SetActive(curInteract);

        if (curInteract != null)
        {
            SetInteractPip();
        }
    }
    private void SetInteractPip()
    {
        interactPip.position = curInteract.transform.position;
        interactLabelText.text = curInteract.interactLabel;
        string text = "(E) Pickup";
        switch (curInteract.interactType)
        {
            case InteractType.Pickup:
                text = "(E) Pickup";
                break;
        
        }
        interactText.text = text;
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(curInteract != null)
            {
                curInteract.Interact(this);
            }
        }
    }


}
