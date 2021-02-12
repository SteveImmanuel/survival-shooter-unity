using UnityEngine;

public class HealthPower : BasePowerUp
{
    public int healthPoint = 20;

    protected override void Execute(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.AddHealth(healthPoint);
    }
}
