using UnityEngine;

public class SpeedPower : BasePowerUp
{
    public float speedMultiplier = 1.5f;
    public float effectDuration = 5f;

    protected override void Execute(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.MultiplySpeed(speedMultiplier, effectDuration);
    }
}
