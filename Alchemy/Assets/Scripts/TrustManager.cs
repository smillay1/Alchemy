using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrustManager : MonoBehaviour
{
    public static TrustManager Instance;

    public int maxTrust = 20;
    public int minTrust = 0;
    public int trustLevel = 5;
    public Slider trustSlider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (trustSlider != null)
        {
            trustSlider.maxValue = maxTrust;
            trustSlider.minValue = minTrust;
            trustSlider.value = trustLevel;
        }
    }

    public void ModifyTrust(int amount)
    {
        trustLevel += amount;
        trustLevel = Mathf.Clamp(trustLevel, minTrust, maxTrust);

        if (trustSlider != null)
            trustSlider.value = trustLevel;

        if (trustLevel >= maxTrust)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if (trustLevel <= minTrust)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("playinggg");
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
