using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class NetworkInstaller : MonoInstaller
    {
        private NetworkManager _networkManager;
        public override void InstallBindings()
        {
            _networkManager = FindFirstObjectByType<NetworkManager>();
            //Container.Bind<NeedObj>().FromComponentInHierarchy(_needObj).AsSingle();
            Container.Bind<NetworkManager>().FromComponentInHierarchy(_networkManager).AsSingle().NonLazy();
        }
    }
}