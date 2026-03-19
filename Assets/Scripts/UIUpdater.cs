using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;

    [SerializeField] Image healthFillImage;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    Health playerHeath;
    Health[] healthList;
    float maxHealthPlayer;

    void Start()
    {   
        InitializePlayerHealth();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        // healthSlider.maxValue = playerHeath.GetHealth();
        
        maxHealthPlayer = playerHeath.GetHealth();
        healthFillImage.fillAmount = playerHeath.GetHealth() / maxHealthPlayer;
    }

    void InitializePlayerHealth()
    {
        healthList = FindObjectsByType<Health>(FindObjectsSortMode.None);

        foreach(Health health in healthList)
        {
            if (health.GetIsPlayer() == true)
            {
                playerHeath = health;
                break;
            }
        }
    }

    void Update()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000");
        // healthSlider.value = playerHeath.GetHealth();
        
        healthFillImage.fillAmount = playerHeath.GetHealth() / maxHealthPlayer;
    }
}
