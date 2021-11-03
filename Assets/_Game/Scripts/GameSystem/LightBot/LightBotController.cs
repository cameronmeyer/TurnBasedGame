using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBotController : MonoBehaviour
{
    [SerializeField]
    private Light _light;
    [SerializeField]
    private Color _greenLightColor;
    [SerializeField]
    private Color _redLightColor;
    [SerializeField]
    private Collider _detectionCollider;

    public Light Light => _light;
    public Color GreenLightColor => _greenLightColor;
    public Color RedLightColor => _redLightColor;
    public Collider DetectionCollider => _detectionCollider;
}
