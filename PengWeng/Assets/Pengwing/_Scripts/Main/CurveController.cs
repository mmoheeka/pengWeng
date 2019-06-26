using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CurveController : MonoBehaviour
{

    public Transform CurveOrigin;

    public float worldBendAmount;

    [Range(-500f, 500f)]
    [SerializeField]
    public float x = 0f;

    [Range(-500f, 500f)]
    [SerializeField]
    public float y = 0f;

    [Range(0f, 50f)]
    [SerializeField]
    public float falloff = 0f;

    public Transform player;


    private Vector2 bendAmount = Vector2.zero;

    // Global shader property ids
    private int bendAmountId;
    private int bendOriginId;
    private int bendFalloffId;
    private int bendDirectionId;

    void Start()
    {

        bendAmountId = Shader.PropertyToID("WorldBendAmount");
        //     bendOriginId = Shader.PropertyToID("_BendOrigin");
        //     bendFalloffId = Shader.PropertyToID("_BendFalloff");
        //     bendDirectionId = Shader.PropertyToID("_BendDirection");

    }

    void Update()
    {
        bendAmount.x = x;
        bendAmount.y = y;

        // Shader.SetGlobalVector(bendAmountId, bendAmount);
        // Shader.SetGlobalVector(bendOriginId, CurveOrigin.position);
        // Shader.SetGlobalFloat(bendFalloffId, falloff);
        Shader.SetGlobalFloat(bendAmountId, worldBendAmount);
    }

}