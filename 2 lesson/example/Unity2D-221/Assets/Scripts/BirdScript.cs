using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private float easyForceMultiplier = 250f;   
    [SerializeField] private float hardForceMultiplier = 350f;  
    [SerializeField] private bool isHardMode = false;           

    private Rigidbody2D rb;
    public static float health;
    private float forceMultiplier;

    private float healthTimeout = 100.0f; // 100sec

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = 1.0f; 
        forceMultiplier = isHardMode ? hardForceMultiplier : easyForceMultiplier; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * forceMultiplier); 
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); 
    
        health -= Time.deltaTime / healthTimeout; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            FoodScript food = other.GetComponent<FoodScript>();
            if (food != null)
            {
                Destroy(other.gameObject);
                health = Mathf.Clamp01(health + food.healthBonus / healthTimeout); 
                Debug.Log("Health: " + health);
            }
        }
        if (other.CompareTag("Pipe"))
        {
            AlertScript.instance.Show("Collision", "You hit an obstacle and lose a life");
        }
    }
}
