using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 10;
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool isMoving;

    public static event Action<int> OnTargetHit;

    private Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }
    private void Move()
    {
        float x = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2f) - moveDistance;

        transform.position = startPosition + Vector3.right * x;
    }
    public void Hit()
    {
        OnTargetHit?.Invoke(points);
        Destroy(gameObject);
    }
}
