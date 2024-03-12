using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class MenuInstaller : MonoInstaller
    {
        [InjectOptional]
        public string LevelName = "default_level";
        public override void InstallBindings()
        {
            //Container.Bind<NeedObj>().FromComponentInHierarchy(_needObj).AsSingle();
            //Container.Bind<NetworkManager>().FromComponentInHierarchy(_networkManager).AsSingle();
            //Container.BindInstance(LevelName).WhenInjectedInto<NetworkManager>();
        }
    }
}