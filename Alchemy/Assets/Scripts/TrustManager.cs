using UnityEngine;
using UnityEngine.UI;

public class TrustManager : MonoBehaviour
{
    public static TrustManager Instance;

    public int trustLevel = 5;
    public int maxTrust = 20;
    public int minTrust = 0;
    public Slider trustSlider;

    void Awake()
    {
        Instance = this;
    }

    void Start()
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

        Debug.Log("🔷 Trust Level: " + trustLevel);

        if (trustLevel == maxTrust)
        {
            Debug.Log("🎉 You win!");
            // Add win logic here
        }
        else if (trustLevel == minTrust)
        {
            Debug.Log("💀 You lose!");
            // Add lose logic here
        }

        // Optionally update UI here if you make a trust bar
        if (trustSlider != null)
        {
            trustSlider.value = trustLevel;
        }
    }
}
