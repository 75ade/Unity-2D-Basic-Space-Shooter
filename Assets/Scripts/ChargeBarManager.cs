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

    public void PlayChargeAnimation()
    {
        animator.SetBool("isCharging", true);
    }

    public void StopChargeAnimation()
    {
        animator.SetBool("isCharging", false);
    }
}
