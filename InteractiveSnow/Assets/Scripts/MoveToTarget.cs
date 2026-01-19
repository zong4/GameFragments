using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 3;

    private void Awake()
    {
        target.transform.position =
            new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
            speed * Time.deltaTime);
        transform.LookAt(target);
    }
}