using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    private Image indicator;

    void Start()
    {
        indicator = transform.Find("Indicator").GetComponent<Image>();
    }

    void Update()
    {
        indicator.fillAmount = Mathf.Clamp01(BirdScript.health); 

        Color color;
        if (indicator.fillAmount > 0.5f)
        {
            float t = (indicator.fillAmount - 0.5f) * 2f;
            color = Color.Lerp(Color.yellow, Color.green, t);
        }
        else
        {
            float t = indicator.fillAmount * 2f;
            color = Color.Lerp(Color.red, Color.yellow, t);
        }

        indicator.color = color;
    }
}
