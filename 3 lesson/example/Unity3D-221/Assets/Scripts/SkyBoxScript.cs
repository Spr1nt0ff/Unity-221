using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;

    void Start()
    {
        GameState.AddListener(OnGameStateChanged);
    }


    private void OnGameStateChanged(string fieldName)
    {
        if(fieldName == nameof(GameState.isDay)){
            RenderSettings.skybox = GameState.isDay ? daySkybox : nightSkybox;
        }
    }


    private void OnDestroy()
    {
        GameState.RemoveListener(OnGameStateChanged);
    }

}
