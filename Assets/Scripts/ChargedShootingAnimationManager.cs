using UnityEngine;

public class ChargedShootingAnimationManager : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayChargedShootingAnimation()
    {
        animator.SetBool("isShooting", true);
    }

    public void StopChargedShootingAnimation()
    {
        animator.SetBool("isShooting", false);
    }
}
