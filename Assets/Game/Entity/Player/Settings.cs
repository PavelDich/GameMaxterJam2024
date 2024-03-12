using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        public static string SaveNameSensativityX;
        [SerializeField]
        public static string SaveNameSensativityY;
        [SerializeField]
        public static string SaveNameAudioValume;
        [SerializeField]
        private TMP_InputField _inputSensativityX;
        [SerializeField]
        private TMP_InputField _inputSensativityY;
        [SerializeField]
        private TMP_InputField _inputAudioValume;
        public void SaveSensativityX(TMP_InputField inputField)
        {
            PlayerPrefs.SetFloat(SaveNameSensativityX, float.Parse(inputField.text));
            UpdateValue();
        }
        public void SaveSensativityY(TMP_InputField inputField)
        {
            PlayerPrefs.SetFloat(SaveNameSensativityY, float.Parse(inputField.text));
            UpdateValue();
        }
        public void SaveAudioValume(TMP_InputField inputField)
        {
            PlayerPrefs.SetFloat(SaveNameAudioValume, float.Parse(inputField.text));
            UpdateValue();
        }

        private void Start()
        {
            UpdateValue();
        }
        private void UpdateValue()
        {
            if (_inputSensativityX != null && PlayerPrefs.HasKey(Settings.SaveNameSensativityX))
                _inputSensativityX.text = PlayerPrefs.GetFloat(Settings.SaveNameSensativityX).ToString();

            if (_inputSensativityY != null && PlayerPrefs.HasKey(Settings.SaveNameSensativityY))
                _inputSensativityY.text = PlayerPrefs.GetFloat(Settings.SaveNameSensativityY).ToString();

            if (_inputAudioValume != null && PlayerPrefs.HasKey(Settings.SaveNameAudioValume))
                _inputAudioValume.text = PlayerPrefs.GetFloat(Settings.SaveNameAudioValume).ToString();
        }
    }
}