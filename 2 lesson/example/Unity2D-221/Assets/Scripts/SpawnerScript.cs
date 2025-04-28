using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    private const float pipeOffSetMax = 2.5f;

    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject food1Prefab;

    private const float foodOffSetMax = 4.5f;

    private float period = 1.0f;
    private float timeout;
    private float foodTimeout;

    void Start()
    {
        timeout = 0;
        foodTimeout = period * 1.5f;
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
        int randomChoice = Random.Range(0, 3);

        if (randomChoice == 0)
        {
            GameObject food = Instantiate(foodPrefab);
            food.transform.position = this.transform.position +
                Random.Range(-foodOffSetMax, foodOffSetMax) * Vector3.up;
            food.transform.Rotate(0, 0, Random.Range(0, 360));
        }
        else if (randomChoice == 1)
        {
            GameObject food1 = Instantiate(food1Prefab);
            food1.transform.position = this.transform.position +
                Random.Range(-foodOffSetMax, foodOffSetMax) * Vector3.up;
            food1.transform.Rotate(0, 0, Random.Range(0, 360));
        }
    }
}
