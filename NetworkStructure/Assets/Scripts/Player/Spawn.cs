using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class Spawn : MonoBehaviourPun
    {
        public Vector2 boundaryX = new Vector2(-6f, 6f);
        public Vector2 boundaryY = new Vector2(-4f, 4f);

        private void Awake()
        {
            if (!photonView.IsMine)
                return;

            transform.position = new Vector3(Random.Range(boundaryX.x, boundaryX.y),
                Random.Range(boundaryY.x, boundaryY.y), 0f);
        }
    }
}