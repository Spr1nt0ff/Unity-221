using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private float easySpeed = 5f;   
    [SerializeField] private float hardSpeed = 15f;
    [SerializeField] private bool isHardMode = false; 

    private float speed;

    void Start() {
        speed = isHardMode ? hardSpeed : easySpeed;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World); 
    }

    #region TODO
    /*
     * Використовуючи [SerializeField] підібрати мінімальну та максимальну
     * швидкість руху елементів для складного та простого режимів гри.
     * Зробити аналогічні дії з множником сили для управління персонажем.
     */
    #endregion
}
