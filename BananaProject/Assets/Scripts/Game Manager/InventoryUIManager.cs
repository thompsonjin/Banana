using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpUI
    {
        public Image bananaSprite;
        public string powerUpName;
        [TextArea(3, 5)]
        public string description;
    }

    [Header("Power Up UI Elements")]
    [SerializeField] private PowerUpUI shadowKickUI;
    [SerializeField] private PowerUpUI chargeUI;
    [SerializeField] private PowerUpUI groundPoundUI;
    [SerializeField] private PowerUpUI bananaGunUI;
    [SerializeField] private PowerUpUI shieldUI;
    [SerializeField] private PowerUpUI fUI;

    private PlayerController playerController;

    private void Awake()
    {
        if(shadowKickUI.bananaSprite != null) shadowKickUI.bananaSprite.enabled = false;
        if(chargeUI.bananaSprite != null) chargeUI.bananaSprite.enabled = false;
        if(groundPoundUI.bananaSprite != null) groundPoundUI.bananaSprite.enabled = false;
        if(bananaGunUI.bananaSprite != null) bananaGunUI.bananaSprite.enabled = false;
        if(shieldUI.bananaSprite != null) shieldUI.bananaSprite.enabled = false;

        SetupTooltip(shadowKickUI);
        SetupTooltip(chargeUI);
        SetupTooltip(groundPoundUI);
        SetupTooltip(bananaGunUI);
        SetupTooltip(shieldUI);
        SetupTooltip(fUI);
    }

    private void SetupTooltip(PowerUpUI powerUp)
    {
        if (powerUp.bananaSprite != null)
        {
            ToolTipTrigger tooltipTrigger = powerUp.bananaSprite.gameObject.GetComponent<ToolTipTrigger>();
            if (tooltipTrigger == null)
                tooltipTrigger = powerUp.bananaSprite.gameObject.AddComponent<ToolTipTrigger>();

            //Set tooltip content from description
            tooltipTrigger.tooltipContent = $"<b>{powerUp.powerUpName}</b>\n{powerUp.description}";
        }
    }

    private void OnEnable()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        if (playerController != null)
        {
            Debug.Log("InventoryUIManager found PlayerController");
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogWarning("InventoryUIManager couldn't find PlayerController!");
        }
    }

    public void SetPlayerController(PlayerController player)
    {
        playerController = player;
        if (playerController != null)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        if (playerController == null)
        {
            Debug.LogWarning("PlayerController is null when updating UI");
            return;
        }

        Debug.Log($"Power-up states: Shadow Kick: {playerController.HasShadowKick}, " +
                  $"Charge: {playerController.HasCharge}, " +
                  $"Ground Pound: {playerController.HasGroundPound}, " +
                  $"Banana Gun: {playerController.HasBananaGun}, " +
                  $"Shield: {playerController.HasBananaShield}");

        if (shadowKickUI.bananaSprite != null)
        {
            shadowKickUI.bananaSprite.enabled = playerController.HasShadowKick;
            Debug.Log($"Shadow Kick image enabled: {shadowKickUI.bananaSprite.enabled}");
        }
        else
            Debug.LogError("Shadow Kick banana sprite reference is missing!");

        if (chargeUI.bananaSprite != null)
        {
            chargeUI.bananaSprite.enabled = playerController.HasCharge;
            Debug.Log($"Charge image enabled: {chargeUI.bananaSprite.enabled}");
        }
        else
            Debug.LogError("Charge banana sprite reference is missing!");

        if (groundPoundUI.bananaSprite != null)
        {
            groundPoundUI.bananaSprite.enabled = playerController.HasGroundPound;
            Debug.Log($"Ground Pound image enabled: {groundPoundUI.bananaSprite.enabled}");
        }
        else
            Debug.LogError("Ground Pound banana sprite reference is missing!");

        if (bananaGunUI.bananaSprite != null)
        {
            bananaGunUI.bananaSprite.enabled = playerController.HasBananaGun;
            Debug.Log($"Banana Gun image enabled: {bananaGunUI.bananaSprite.enabled}");
        }
        else
            Debug.LogError("Banana Gun banana sprite reference is missing!");

        if (shieldUI.bananaSprite != null)
        {
            shieldUI.bananaSprite.enabled = playerController.HasBananaShield;
            Debug.Log($"Shield image enabled: {shieldUI.bananaSprite.enabled}");
        }
        else
            Debug.LogError("Shield banana sprite reference is missing!");
    }

}
