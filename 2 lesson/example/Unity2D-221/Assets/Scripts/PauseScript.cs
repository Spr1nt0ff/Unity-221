using System;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private GameObject content;

    void Start()
    {
        Transform c = transform.Find("Content");
        content = c.gameObject;

        if (content.activeSelf) {
            Time.timeScale = 0.0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            TogglePause();
        }
    }

    private void TogglePause() {
        if (content.activeSelf) {
            content.SetActive(false);
            Time.timeScale = 1.0f;
        } else {
            content.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void OnButtonClick() {
        content.SetActive(false);
        Time.timeScale = 1.0f; 
    }

    public void OnIntervalValueChanged(Single value) {
        SpawnerScript.difficulty = value; 
    }
}
