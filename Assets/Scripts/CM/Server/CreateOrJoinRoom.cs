using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace CM.Server
{
    public class CreateOrJoinRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Dropdown maxPlayersDropdown;
        [SerializeField] private InputField createRoomNameInputField;
        [SerializeField] private InputField joinRoomNameInputField;

        [SerializeField] private Button createRoomButton;
        [SerializeField] private Button joinRoomButton;

        private void Awake()
        {
            createRoomButton.onClick.AddListener(CreateRoom);
            joinRoomButton.onClick.AddListener(JoinRoom);
        }

        private void CreateRoom()
        {
            var roomOptions = new RoomOptions();
            var roomPlayerCount = (byte)((byte)maxPlayersDropdown.value + 2);
            
            
            roomOptions.MaxPlayers = roomPlayerCount;
            
            PhotonNetwork.CreateRoom(createRoomNameInputField.text, roomOptions);
        }
        private void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinRoomNameInputField.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }

        private void OnDestroy() => Dispose();

        private void Dispose()
        {
            createRoomButton.onClick.RemoveAllListeners();
            joinRoomButton.onClick.RemoveAllListeners();
        }
    }
}