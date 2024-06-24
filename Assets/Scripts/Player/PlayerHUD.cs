using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI gunNameText;


    [Header("Components")]
    [SerializeField] private PlayerController playerController;

    private void FixedUpdate()
    {
        UpdateHealthFill();
    }
    public void UpdateHealthFill()
    {
        float amount = (float)playerController.h_health.health / (float)playerController.h_health.maxHealth;
        healthFill.fillAmount = amount;
    }

    public void UpdateGunText()
    {
        Gun gun = playerController.h_weapon.equippedGun;
        magazineText.text = gun.magazineCur.ToString();
        ammoText.text = gun.ammo.ToString();
        gunNameText.text = gun.gunData.gunName.ToString();
    }

}
