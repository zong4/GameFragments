using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public Vector2 boundaryX = new Vector2(-6f, 6f);
    public Vector2 boundaryY = new Vector2(-4f, 4f);
    public float moveSpeed = 10f;

    private void Awake()
    {
        if (!photonView.IsMine)
            return;

        transform.position = new Vector3(Random.Range(boundaryX.x, boundaryX.y), Random.Range(boundaryY.x, boundaryY.y),
            0f);
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        var movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        transform.Translate(movement * (moveSpeed * Time.deltaTime));
    }
}