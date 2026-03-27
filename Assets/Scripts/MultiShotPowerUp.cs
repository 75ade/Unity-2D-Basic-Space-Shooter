using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MultiShotPowerUp", menuName = "MultiShotPowerUp")]
public class MultiShotPowerUp : PowerUpSO
{
    [SerializeField] int projectileAmount;
    [SerializeField] float duration;

    public override void Apply(GameObject player)
    {
        Shooter shooter = player.GetComponentInChildren<Shooter>();
        shooter.CallMultiShotCoroutine(projectileAmount, duration);
    }
}
