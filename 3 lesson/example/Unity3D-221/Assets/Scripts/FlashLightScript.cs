using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private GameObject player;
    private Light _light;
    public static float charge;
    private float chargeLifeTime = 10.0f;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("FlashlightScript: Player not found");
        }

        _light = GetComponent<Light>();
        charge = 1.0f;
    }


    void Update()
    {
        if (player == null) return;

        this.transform.position = player.transform.position;
        this.transform.forward = Camera.main.transform.forward;

        if (GameState.isFpv & !GameState.isDay)
        {
            _light.intensity = charge;
            charge = Mathf.Clamp01(charge - Time.deltaTime / chargeLifeTime);
        }
        else
        {
            _light.intensity = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Battery")){
            charge += 1.0f;
            GameObject.Destroy(other.gameObject);
            Debug.Log("Battery collected" + charge);
            ToasterScript.Toast($"Ви знайшли батарейку. Заряд ліхтарика поповнено до {charge:F3}");
        }
    }
}
