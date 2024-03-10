using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using Minicop.Library.Stats;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    [RequireComponent(typeof(Image))]
    public class ParameterUI : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private Image _parameterImage;
        public void UpdateValue(Parameter parameter) =>
            _parameterImage.fillAmount = parameter.Value / parameter.Max;

        public void OnValidate() =>
            _parameterImage = GetComponent<Image>();
    }
}