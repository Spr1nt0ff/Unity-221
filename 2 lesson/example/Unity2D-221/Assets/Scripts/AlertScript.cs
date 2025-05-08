using System;
using UnityEngine;

public class AlertScript :MonoBehaviour {
    private static TMPro.TextMeshProUGUI title;
    private static TMPro.TextMeshProUGUI message;
    private static TMPro.TextMeshProUGUI actionButtonText;

    private static GameObject content;
    private static Action action;

    public static void Show(string title, string message, string actionButtonText = "Close", Action action = null) {
        AlertScript.title.text = title;
        AlertScript.message.text = message;
        AlertScript.actionButtonText.text = actionButtonText;
        AlertScript.action = action;

        content.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void Start() {
        Transform c = transform.Find("Content");
        title = c.Find("Title").GetComponent<TMPro.TextMeshProUGUI>();
        message = c.Find("Message").GetComponent<TMPro.TextMeshProUGUI>();
        actionButtonText = c.Find("Button/Text").GetComponent<TMPro.TextMeshProUGUI>();

        content = c.gameObject;
        content.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && content.activeSelf) {
            OnActionButtonClick();
        }
    }

    public void OnActionButtonClick() {
        content.SetActive(false);
        Time.timeScale = 1.0f;

        if (action != null) {
            action.Invoke(); 
        }

        DestroyerScript.ClearField();
    }
}
