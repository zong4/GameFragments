using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class Room : MonoBehaviour
    {
        public Button readyButton;
        private bool _isReady = false;
        private const string PlayerReadyKey = "Ready";

        private void Awake()
        {
            UpdateReadyButtonText();
            readyButton.onClick.AddListener(OnReadyButtonClicked);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PhotonNetwork.LeaveRoom();
        }

        private void OnReadyButtonClicked()
        {
            _isReady = !_isReady;
            UpdateReadyButtonText();

            var props = new Hashtable
            {
                { PlayerReadyKey, _isReady }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        private void UpdateReadyButtonText()
        {
            readyButton.GetComponentInChildren<TMP_Text>().text =
                _isReady ? "Unready" : "Ready";
        }
    }
}