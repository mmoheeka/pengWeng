using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurveController : MonoBehaviour
{

    public Transform CurveOrigin;

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

    void Start()
    {

        bendAmountId = Shader.PropertyToID("_BendAmount");
        bendOriginId = Shader.PropertyToID("_BendOrigin");
        bendFalloffId = Shader.PropertyToID("_BendFalloff");
    }

    void Update()
    {
        bendAmount.x = x;
        bendAmount.y = y;

        Shader.SetGlobalVector(bendAmountId, bendAmount);
        Shader.SetGlobalVector(bendOriginId, CurveOrigin.position);
        Shader.SetGlobalFloat(bendFalloffId, falloff);
    }

}