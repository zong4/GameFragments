using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 3f;

    private void Update()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += movement * (speed * Time.deltaTime);
        transform.LookAt(transform.position + movement);
    }
}