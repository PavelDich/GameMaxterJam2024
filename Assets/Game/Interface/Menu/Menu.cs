using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;


namespace GCinc.GameMaxterJam2024.PavelDich
{
    [DisallowMultipleComponent]
    public class Menu : MonoBehaviour
    {
        [Inject]
        private NetworkManager _networkManager;

        public void Create()
        {
            _networkManager.Create();
        }

        public void Connect(TMP_InputField inputIPAdress)
        {
            _networkManager.Connect(inputIPAdress.text);
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}