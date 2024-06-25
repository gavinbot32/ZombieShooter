using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAdjust : MonoBehaviour
{
    public Camera cam;
    private Rect baseRect;
    private PlayerManager playerManager;

    private void Awake()
    {
        cam = this.SafeGetComponent(cam);
        baseRect = cam.rect;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        AdjustCam();
    }

    private void OnEnable()
    {
        if(playerManager != null)
        {
            playerManager.onPlayerJoined.AddListener(AdjustCam);
        }
    }

    private void OnDisable()
    {
        if(playerManager != null)
        {
            playerManager.onPlayerJoined.RemoveListener(AdjustCam);
        }
    }

    public void AdjustCam()
    {
        if (playerManager.m_playerInput.playerCount == 2)
        {
            cam.rect = new Rect(baseRect.x, baseRect.y, baseRect.width / playerManager.m_playerInput.playerCount, baseRect.height);
        }
        else
        {
            cam.rect = baseRect;
        }
    }

}
