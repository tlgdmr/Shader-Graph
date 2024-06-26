using System.Collections;
using UnityEngine;

public class DissolveShaderHandler : MonoBehaviour
{
    [SerializeField] private Material dissolveMaterial;
    [Header("Dissolve Time")]
    [SerializeField] private float duration;
    
    private bool _isDissolving;
    private static readonly int DissolveStrength = Shader.PropertyToID("_DissolveStrength");

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isDissolving)
        {
            StartCoroutine(AdjustDissolveStrength(0.0f, 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !_isDissolving)
        {
            StartCoroutine(AdjustDissolveStrength(1.0f, 0.0f));
        }
    }

    private IEnumerator AdjustDissolveStrength(float startValue, float endValue)
    {
        _isDissolving = true;
        var elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var currentStrength = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            dissolveMaterial.SetFloat(DissolveStrength, currentStrength);
            yield return null;
        }

        dissolveMaterial.SetFloat(DissolveStrength, endValue);
        _isDissolving = false;
    }
    
    
}
