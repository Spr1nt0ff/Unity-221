using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    private GameObject content;
    private Image indicatorImage;
    void Start()
    {
        content = transform.Find("Content").gameObject;
        indicatorImage = transform
           .Find("Indicator/Canvas/Foreground")
           .GetComponent<Image>();
        indicatorImage.fillAmount = 0.45f;
    }

    // Update is called once per frame
    void Update()
    {
        content.transform.Rotate(0, Time.deltaTime * 10f,0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            GameState.isKey1Collected = true;

            Destroy(gameObject);
        }
    }
}
