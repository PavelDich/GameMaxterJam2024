using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private NetworkManager _networkManager;
        public override void InstallBindings()
        {
            //Container.Bind<NeedObj>().FromNew().AsSingle();
            Container.Bind<NetworkManager>().FromComponentInNewPrefab(_networkManager).AsSingle().NonLazy();
        }
    }
}