using UnityEngine;

public class AlertScript : MonoBehaviour
{
    public static AlertScript instance;

    private  TMPro.TextMeshProUGUI title;
    private  TMPro.TextMeshProUGUI message;
    private  TMPro.TextMeshProUGUI actionButtonText;

    private  GameObject content;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Multiple instances of AlertScript detected.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }



    public void Show(string title, string message, string actionButtonText = "Close")
    {
        if (this.title != null) this.title.text = title;
        else Debug.LogError("Title TextMeshProUGUI is not assigned.");

        if (this.message != null) this.message.text = message;
        else Debug.LogError("Message TextMeshProUGUI is not assigned.");

        if (this.actionButtonText != null) this.actionButtonText.text = actionButtonText;
        else Debug.LogError("Action Button TextMeshProUGUI is not assigned.");

        if (content != null)
        {
            content.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            Debug.LogError("Content GameObject is not assigned.");
        }
    }


    void Start()
    {
        Debug.Log("AlertScript started!");
        Transform c = transform.Find("Content");
        if (c != null)
        {
            Debug.Log("Found Content!");

            Transform titleText = c.Find("Title/Text");
            Transform messageText = c.Find("Message/Text");
            Transform buttonText = c.Find("Button/Text");

            if (titleText != null) title = titleText.GetComponent<TMPro.TextMeshProUGUI>();
            if (messageText != null) message = messageText.GetComponent<TMPro.TextMeshProUGUI>();
            if (buttonText != null) actionButtonText = buttonText.GetComponent<TMPro.TextMeshProUGUI>();

            content = c.gameObject;
            content.SetActive(false);
        }
        else
        {
            Debug.LogError("AlertScript: Не найден Content объект!");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            content.SetActive(false); 
            Time.timeScale = 1.0f;
        }
    }

    public void OnActionButtonClick()
    {
        content.SetActive(false );
        Time.timeScale = 1.0f;  
    }
}
