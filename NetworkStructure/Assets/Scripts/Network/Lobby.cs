using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class Lobby : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        public void OnHostButtonClicked()
        {
            SceneManager.LoadScene("HostRoom");
        }

        public void OnJoinButtonClicked()
        {
            SceneManager.LoadScene("JoinRoom");
        }
    }
}