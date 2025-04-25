using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private float easyForceMultiplier = 250f;   
    [SerializeField] private float hardForceMultiplier = 350f;  
    [SerializeField] private bool isHardMode = false;           

    private Rigidbody2D rb;
    private float health;
    private float forceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = 100f; 
        forceMultiplier = isHardMode ? hardForceMultiplier : easyForceMultiplier; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * forceMultiplier); 
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); 
    
        health -= Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food")) {
            Destroy(other.gameObject);
            health = Mathf.Min(health + 10f, 100f);

            Debug.Log("Health: " + health);
        }
    }
}
