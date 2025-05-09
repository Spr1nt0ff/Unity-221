using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    public static float extraLifeChance = 0.2f;

    [SerializeField] private float easyForceMultiplier = 250f;   // сила для простого режима
    [SerializeField] private float hardForceMultiplier = 350f;   // сила для сложного режима
    [SerializeField] private bool isHardMode = false;            // флаг режима игры
    [SerializeField] private TMPro.TextMeshProUGUI triesTmp;

    private Rigidbody2D rb;
    public static float health;
    private float healthTimeout = 20.0f;                        // время, через которое здоровье будет уменьшаться
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * forceMultiplier * Time.timeScale); 
        }

        transform.eulerAngles = new Vector3(0, 0, 3f * rb.linearVelocityY); 

        health -= Time.deltaTime / healthTimeout;
        if (health <= 0)
        {
            Loose(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            FoodScript foodScript = other.GetComponent<FoodScript>();
            if (foodScript != null)
            {
                health = Mathf.Clamp01(health + foodScript.healthBonus / healthTimeout);

                // Увеличиваем счётчик нужного типа еды
                if (SpawnerScript.foodCount != null && foodScript.foodIndex < SpawnerScript.foodCount.Length)
                {
                    SpawnerScript.foodCount[foodScript.foodIndex]++;
                }

                CounterScript.UpdateCounter();
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Pipe"))
        {
            Loose();
        }
    }

    private void Loose()
    {
        // Сбрасываем счётчики еды
        SpawnerScript.foodCount[0] = 0;
        SpawnerScript.foodCount[1] = 0;
        CounterScript.UpdateCounter(); // Обновляем отображение счётчиков

        if (Random.value < extraLifeChance)
        {
            health = 1.0f;
            AlertScript.Show("Second Chance", "You got an extra life!", "Continue", () => DestroyerScript.ClearField());
            Time.timeScale = 0;
            return;
        }

        tries--;
        triesTmp.text = tries.ToString();

        if (tries > 0)
        {
            health = 1.0f;
            AlertScript.Show("Warning", "You have " + tries + " tries left!", "Continue", () => DestroyerScript.ClearField());
        }
        else
        {
            AlertScript.Show("Game Over", "You hit a pipe!", "Restart", () => SceneManager.LoadScene(0));
        }

        Time.timeScale = 0;
    }

}
