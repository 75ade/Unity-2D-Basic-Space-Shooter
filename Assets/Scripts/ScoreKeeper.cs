using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;

    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        score = Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log($"Score: {score}");
    }

    public void ResetScore()
    {
        score = 0;
    }
}
