using UnityEngine;

public class OutlineShaderHandler : MonoBehaviour
{
    [SerializeField] private Material outlineShaderMaterial;
    [Header("Outline Values")]
    [SerializeField] private float outlineScale;
    [SerializeField] private Color outlineColor;
    [SerializeField] private float outlineFlashingSpeed;
    
    private static readonly int OutlineScale = Shader.PropertyToID("_OutlineScale");
    private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    private static readonly int OutlineFlashing = Shader.PropertyToID("_FlashingValue");
    
    private void Start()
    {
        SetShaderValues();
    }

    private void SetShaderValues()
    {
        outlineShaderMaterial.SetColor(OutlineColor, outlineColor);
        outlineShaderMaterial.SetFloat(OutlineScale, outlineScale);
    }

    private void Update()
    {
        FlashOutline();
    }

    private void FlashOutline()
    {
       var currentFlashingValue = Mathf.PingPong(Time.time / outlineFlashingSpeed, 1.0f);
       outlineShaderMaterial.SetFloat(OutlineFlashing,currentFlashingValue);
    }
}
