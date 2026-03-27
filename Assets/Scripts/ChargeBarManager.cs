using UnityEditor.Animations;
using UnityEngine;

public class ChargeBarManager : MonoBehaviour
{
    [SerializeField] AnimatorController[] animatorControllers;

    Animator animator;
    CharSelectManager charSelectManager;

    void Awake()
    {
        animator = GetComponent<Animator>();
        charSelectManager = FindFirstObjectByType<CharSelectManager>();
    }

    void Start()
    {
        animator.runtimeAnimatorController = animatorControllers[charSelectManager.GetCurrentShipIndex()];
    }

    public void PlayChargeBarAnimation()
    {
        animator.SetBool("isCharging", true);
    }

    public void StopChargeBarAnimation()
    {
        animator.SetBool("isCharging", false);
    }
}
