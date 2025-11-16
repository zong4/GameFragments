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

        private void Awake()
        {
            readyButton.GetComponentInChildren<TMP_Text>().text = _isReady ? "Unready" : "Ready";
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
            readyButton.GetComponentInChildren<TMP_Text>().text = _isReady ? "Unready" : "Ready";
        }
    }
}