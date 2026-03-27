using UnityEngine;

public class NormalShootingAnimationManager : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayNormalShootingAnimation()
    {
        animator.SetBool("isNormalShooting", true);
    }

    public void StopNormalShootingAnimation()
    {
        animator.SetBool("isNormalShooting", false);
    }
}
