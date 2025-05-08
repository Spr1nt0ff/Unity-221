using UnityEngine;

public class Gates1Script : MonoBehaviour
{
    private float size = 1.0f;
    private float openTime = 2.0f;
    private bool isKeyInserted;
    private int hitCount;
    void Start()
    {
        isKeyInserted = false;
        hitCount = 0;
    }


    void Update()
    {
        if(isKeyInserted && transform.localPosition.z <= size)
        {
            transform.Translate(0f,0f,size * Time.deltaTime / openTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (GameState.isKey1Collected)
            {
                isKeyInserted = true;
            }
            else
            {
                hitCount += 1;
                if (hitCount == 1)
                {
                    ToasterScript.Toast("For Open door need #1 black key");
                }
                else
                {
                    ToasterScript.Toast($"{hitCount}th you need #1 black key");
                }
               ;
                
            }
        }
    }
}
