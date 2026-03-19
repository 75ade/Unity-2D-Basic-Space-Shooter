using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    [SerializeField] Sprite[] healthBarSprites;
    [SerializeField] Image fillImage;
    CharSelectManager charSelectManager;

    void Awake()
    {
        charSelectManager = FindFirstObjectByType<CharSelectManager>();
    }

    void Start()
    {
        fillImage.sprite = healthBarSprites[charSelectManager.GetCurrentShipIndex()];
    }
}
