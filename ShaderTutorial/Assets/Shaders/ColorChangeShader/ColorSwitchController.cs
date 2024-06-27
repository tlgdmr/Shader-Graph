using System;
using System.Collections;
using UnityEngine;

namespace Shaders.ColorChangeShader
{
    public class ColorSwitchController : MonoBehaviour
    {
        [SerializeField] private Material colorSwitchMaterial;
        [SerializeField] private Color currentColor;
        [SerializeField] private Color targetColor;
        [SerializeField] private float colorSwitchingDuration;
        private static readonly int CurrentColor = Shader.PropertyToID("_CurrentColor");
        private static readonly int TargetColor = Shader.PropertyToID("_TargetColor");
        private static readonly int ColorSwitchValue = Shader.PropertyToID("_ColorSwitchValue");

        private bool _isSwitchingTarget;

        private void Start()
        {
            colorSwitchMaterial.SetColor(CurrentColor, currentColor);
            colorSwitchMaterial.SetColor(TargetColor, targetColor);
            StartCoroutine(SwitchColors());
        }

        private IEnumerator SwitchColors()
        {
            while (true)
            {
                yield return StartCoroutine(_isSwitchingTarget ? SwitchCurrent() : SwitchTarget());
                _isSwitchingTarget = !_isSwitchingTarget;
            }
        }

        private IEnumerator SwitchTarget()
        {
            float elapsedTime = 0;
            while (elapsedTime < colorSwitchingDuration)
            {
                elapsedTime += Time.deltaTime;
                var switchingColorValue = Mathf.Lerp(0, 1, elapsedTime / colorSwitchingDuration);
                colorSwitchMaterial.SetFloat(ColorSwitchValue, switchingColorValue);
                yield return null;
            }
            colorSwitchMaterial.SetFloat(ColorSwitchValue, 1);
        }

        private IEnumerator SwitchCurrent()
        {
            float elapsedTime = 0;
            while (elapsedTime < colorSwitchingDuration)
            {
                elapsedTime += Time.deltaTime;
                var switchingColorValue = Mathf.Lerp(1, 0, elapsedTime / colorSwitchingDuration);
                colorSwitchMaterial.SetFloat(ColorSwitchValue, switchingColorValue);
                yield return null;
            }
            colorSwitchMaterial.SetFloat(ColorSwitchValue, 0);
        }
    }
}
