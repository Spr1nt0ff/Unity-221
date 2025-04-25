using UnityEngine;

public class SpawnerScript :MonoBehaviour {
    [SerializeField]
    private GameObject pipePrefab;
    private const float pipeOffSetMax = 2.5f;

    [SerializeField]
    private GameObject foodPrefab;
    private const float foodOffSetMax = 4.5f;

    private float period = 1.0f;
    private float timeout;
    private float foodTimeout;

    void Start() {
        timeout = 0;
        foodTimeout = period * 1.5f;
    }

    void Update() {
        timeout -= Time.deltaTime;
        if (timeout < 0) {
            timeout = period;
            SpawnPipe();
        }

        foodTimeout -= Time.deltaTime;
        if (foodTimeout < 0) {
            foodTimeout = period;
            SpawnFood();
        }
    }

    private void SpawnObject(GameObject gameObject) {

    }

    private void SpawnPipe() {
        GameObject pipe = Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position +
            Random.Range(-pipeOffSetMax, pipeOffSetMax) * Vector3.up;
    }
    private void SpawnFood() {
        GameObject food = Instantiate(foodPrefab);
        food.transform.position = this.transform.position +
            Random.Range(-foodOffSetMax, foodOffSetMax) * Vector3.up;
        food.transform.Rotate(0, 0, Random.Range(0, 360));
    }
}
