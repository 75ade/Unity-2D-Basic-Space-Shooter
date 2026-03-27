using UnityEngine;

[CreateAssetMenu(fileName = "ShieldPowerUp", menuName = "ShieldPowerUp")]
public class ShieldPowerUp : PowerUpSO
{
    public override void Apply(GameObject player)
    {
        Health health = player.GetComponent<Health>();
        health.ActivateShield();
    }
}
