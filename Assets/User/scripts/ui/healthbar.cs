using UnityEngine;

public class healthbar : MonoBehaviour {
    [Header("controllers")]
    public PlayerController player;
    public MainMenace menace;

    [Header("type")]
    public BarType type;
    public enum BarType { Menace, Player, Stamina, BaseAttackStamina, BaseAttackStaminaMenace }

    [Header("componenets")]
    public GameObject Bar;

    [Header("config")]
    public float smoothSpeed = 5f;
    public bool vertical = false;

    void Update() { 
        if (Bar == null) return;
        float targetRatio = 1f;

        switch (type) {
            case BarType.Menace:
                if (menace != null && menace.maxHealth > 0)
                    targetRatio = menace.health / menace.maxHealth;
                else
                    targetRatio = 0;
                break;

            case BarType.Player:
                if (player != null && player.maxHealth > 0)
                    targetRatio = player.health / player.maxHealth;
                else
                    targetRatio = 0;
                break;

            case BarType.Stamina:
                if (player != null && player.PlayerSpecificMeter > 0)
                    targetRatio = player.PlayerSpecificMeter / player.PlayerSpecificMeterMax;
                else
                    targetRatio = 0;
                break;
            
            case BarType.BaseAttackStamina:
                if (player != null && player.BaseAttackStamina > 0)
                    targetRatio = player.BaseAttackStamina / player.BaseAttackStaminaMax;
                else
                    targetRatio = 0;
                break;
            
            case BarType.BaseAttackStaminaMenace:
                if (menace != null && menace.BaseAttackStamina > 0)
                    targetRatio = menace.BaseAttackStamina / menace.BaseAttackStaminaMax;
                else
                    targetRatio = 0;
                break;

        }

        Vector3 currentScale = Bar.transform.localScale;
        if (vertical) {
            float smoothedY = Mathf.Lerp(currentScale.y, targetRatio, Time.deltaTime * smoothSpeed);
            Bar.transform.localScale = new Vector3(currentScale.x, smoothedY, currentScale.z);
        } else {
            float smoothedX = Mathf.Lerp(currentScale.x, targetRatio, Time.deltaTime * smoothSpeed);
            Bar.transform.localScale = new Vector3(smoothedX, currentScale.y, currentScale.z);
        }
    }
}
