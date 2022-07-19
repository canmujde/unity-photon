using System;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace CM.Server
{
    public class CreateOrJoinRoom : MonoBehaviour
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

        private void JoinRoom()
        {
            
        }

        private void CreateRoom()
        {
        }
        
        
        private void OnDestroy() => Dispose();
        private void OnDisable()=> Dispose();

        private void Dispose()
        {
            createRoomButton.onClick.RemoveAllListeners();
            joinRoomButton.onClick.RemoveAllListeners();
        }
    }
}