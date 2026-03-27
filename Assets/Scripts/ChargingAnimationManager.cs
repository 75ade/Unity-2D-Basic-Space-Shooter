using UnityEngine;

public class ChargingAnimationManager : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayChargingAnimation()
    {
        animator.SetBool("isCharging", true);
    }

    public void StopChargingAnimation()
    {
        animator.SetBool("isCharging", false);
    }
}
