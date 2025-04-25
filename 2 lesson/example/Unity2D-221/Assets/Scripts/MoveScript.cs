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

}
