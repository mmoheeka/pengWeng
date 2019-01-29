using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBender : MonoBehaviour
{

    public float attenuate = 1.0f;
    public float horizonOffset = 0.0f;
    public float spread = 1.0f;
    public Transform player;

    private void Update()
    {
        Shader.EnableKeyword("BEND_ON");
        Shader.SetGlobalFloat("_HORIZON", player.position.z + horizonOffset);
        Shader.SetGlobalFloat("_SPREAD", spread);
        Shader.SetGlobalFloat("_ATTENUATE", attenuate);
    }

    private void OnDisable()
    {
        Shader.DisableKeyword("BEND_ON");
        Shader.SetGlobalFloat("_ATTENUATE", 0);
        Shader.SetGlobalFloat("_SPREAD", 0);
        Shader.SetGlobalFloat("_HORIZON", 0);
    }
}