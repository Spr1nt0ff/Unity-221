using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public static float _difficulty = 0.5f;
    public static float difficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            foodTimeout = timeout + period * 1.5f;
        }
    }

    public static float _lifeDropChance = 0.2f;
    public static float lifeDropChance
    {
        get => _lifeDropChance;
        set => _lifeDropChance = Mathf.Clamp01(value);
    }

    [SerializeField] private GameObject pipePrefab;
    private const float pipeOffSetMax = 2.5f;

    [SerializeField] private GameObject[] foodPrefabs;
    private const float foodOffSetMax = 4.5f;

    public static float _foodDropChance = 0.4f;
    public static float foodDropChance
    {
        get => _foodDropChance;
        set
        {
            _foodDropChance = value;
            foodTimeout = timeout + period * 1.5f;
        }
    }

    private static float period => 2.0f - 0.9f * difficulty;
    private static float timeout;
    private static float foodTimeout;

    public static int[] foodCount;
    private static bool initialized = false;

    void Start()
    {
        timeout = 0;
        foodTimeout = period * 1.5f;

        if (!initialized)
        {
            foodCount = new int[foodPrefabs.Length];
            initialized = true;
        }
    }

    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout < 0)
        {
            timeout = period;
            SpawnPipe();
        }

        foodTimeout -= Time.deltaTime;
        if (foodTimeout < 0)
        {
            foodTimeout = period;
            SpawnFood();
        }
    }

    private void SpawnPipe()
    {
        GameObject pipe = Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position +
            Random.Range(-pipeOffSetMax, pipeOffSetMax) * Vector3.up;
    }

    private void SpawnFood()
    {
        if (foodPrefabs == null || foodPrefabs.Length == 0)
            return;

        if (Random.value < foodDropChance)
            return;

        int index = Random.Range(0, foodPrefabs.Length);

        GameObject food = Instantiate(foodPrefabs[index]);
        food.transform.position = transform.position + Random.Range(-foodOffSetMax, foodOffSetMax) * Vector3.up;
        food.transform.Rotate(0, 0, Random.Range(0, 360));

        // Задаём индекс еде
        var foodScript = food.GetComponent<FoodScript>();
        if (foodScript != null)
        {
            foodScript.foodIndex = index;
        }
    }

}
