using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class NetworkManager : Mirror.NetworkManager
    {
        [Scene]
        [SerializeField]
        private string _startScene;
        public bool playerSpawned;


        public void OnCreateCharacter(NetworkConnectionToClient conn, PosMessage message)
        {
            Transform startPos = GetStartPosition();
            GameObject go = Instantiate(playerPrefab, message.vector3, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, go);
        }
        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkServer.RegisterHandler<PosMessage>(OnCreateCharacter);
        }

        public void ActivatePlayerSpawn()
        {
            PosMessage m = new PosMessage() { vector3 = new Vector3(0, 1, 0) };
            NetworkClient.Send(m);
            playerSpawned = true;
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            if (!playerSpawned) ActivatePlayerSpawn();
        }


        public void Create()
        {
            if (!NetworkClient.isConnected && !NetworkServer.active)
                StartHost();
        }

        public void Connect(string ipAdress)
        {
            if (!NetworkClient.isConnected && !NetworkServer.active && !string.IsNullOrWhiteSpace(ipAdress))
            {
                networkAddress = ipAdress;
                StartClient();
                if (isNetworkActive)
                    StartClient();
            }
        }

        private void LoadScene(int Scane)
        {
            SceneManager.LoadScene(Scane);
        }
    }

    public struct PosMessage : NetworkMessage
    {
        public Vector3 vector3;
    }
}