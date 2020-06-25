using UnityEngine;
using System.Collections;

public class UnderwaterCameraController : MonoBehaviour
{
    [SerializeField] private GameObject water = null;

    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;

    void Start()
    {
        //Set the background color
        this.GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1);
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultSkybox = RenderSettings.skybox;
    }

    void Update()
    {
        if (this.transform.position.y < water.transform.position.y)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            RenderSettings.fogDensity = 0.07f;
            RenderSettings.skybox = noSkybox;
        }
        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
        }
    }
}