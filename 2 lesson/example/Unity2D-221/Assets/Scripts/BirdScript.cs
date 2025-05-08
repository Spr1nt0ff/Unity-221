using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    [SerializeField] private float easyForceMultiplier = 250f;   
    [SerializeField] private float hardForceMultiplier = 350f;   
    [SerializeField] private bool isHardMode = false;           
    [SerializeField] private TMPro.TextMeshProUGUI triesTmp;

    private Rigidbody2D rb;
    public static float health;
    private float healthTimeout = 20.0f;                        
    private float forceMultiplier;

    private int tries;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        health = 1.0f; 
        forceMultiplier = isHardMode ? hardForceMultiplier : easyForceMultiplier; 
        tries = 3; 
        triesTmp.text = tries.ToString(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * forceMultiplier * Time.timeScale); 
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); 
    
        health -= Time.deltaTime / healthTimeout; 
        if (health <= 0) {
            Loose(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food")) {
            FoodScript foodScript = other.GetComponent<FoodScript>(); 
            if (foodScript != null) {
                health = Mathf.Clamp01(health + foodScript.healthBonus / healthTimeout); 
            }

            Destroy(other.gameObject);
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Pipe")) {
            Loose();
        }
    }

    private void Loose() {
        tries--; 
        triesTmp.text = tries.ToString();

        if (tries > 0) {
            health = 1.0f; 
            AlertScript.Show("Warning", "You have " + tries + " tries left!", "Continue", () => DestroyerScript.ClearField());
        } else {
            AlertScript.Show("Game Over", "You hit a pipe!", "Restart", () => SceneManager.LoadScene(0)); 
        }

        Time.timeScale = 0; 
    }
}
