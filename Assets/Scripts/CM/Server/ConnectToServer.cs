using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CM.Server
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private string loadSceneName;
        private void Start()
        {
            Connect();
        }

        private void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene(loadSceneName);
        }
    }
}
