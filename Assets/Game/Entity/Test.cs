using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Mirror;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class Test : MonoBehaviour
    {
        [Inject]
        private NetworkManager _networkManager;
        public void Start()
        {
            Debug.Log(_networkManager.ToString() + "dsfsf");
        }
    }
}