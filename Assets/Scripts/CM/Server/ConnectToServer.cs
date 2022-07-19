using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CM.Server
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private string loadSceneName;
        [SerializeField] private Image fadeInImage;
        [SerializeField] private Text loadingText;
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
            loadingText.text = "Connected";
            fadeInImage.DOColor(Color.black, 0.3f).SetDelay(2f).OnComplete((() =>
            {
                SceneManager.LoadScene(loadSceneName);
            }));
            
        }
    }
}
