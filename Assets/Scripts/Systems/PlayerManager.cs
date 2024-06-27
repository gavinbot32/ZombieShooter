using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent onPlayerJoined;
    public UnityEvent onPlayerLeft;
    public PlayerInputManager m_playerInput;

    public LayerMask[] baseMasks;
    public LayerMask[] weaponMasks;
    public LayerMask[] uioMasks;
    public byte[] weaponLayers;
    public byte[] uioLayers;
    public byte[] weaponModelLayers;

    public Material[] playerMaterials;

    public List<PlayerController> players;

    private void Start()
    {
        players = new List<PlayerController>();
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.weaponMask = weaponMasks[m_playerInput.playerCount - 1];
        pc.weaponLayer = weaponLayers[m_playerInput.playerCount - 1];

        pc.uioMask = uioMasks[m_playerInput.playerCount - 1];
        pc.uioLayer = uioLayers[m_playerInput.playerCount - 1];

        pc.baseMask = baseMasks[m_playerInput.playerCount - 1];
        pc.modelWeaponLayer = weaponModelLayers[m_playerInput.playerCount - 1];

        pc.SetCameraLayer(pc.uioCamera, pc.uioMask);
        pc.SetCameraLayer(pc.weaponCamera, pc.weaponMask);
        pc.SetCameraLayer(pc.baseCamera, pc.baseMask);

        pc.model.material = playerMaterials[m_playerInput.playerCount - 1];

        players.Add(pc);

        onPlayerJoined.Invoke();
    }
    public void OnPlayerLeft(PlayerInput player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        players.Remove(pc);
        onPlayerLeft.Invoke();
    }

}

