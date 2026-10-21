using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 50f;
    public float lifetime = 3f;
    public float hitRadius = 0.05f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Deactivate), lifetime);
    }

    public void Shoot(Vector3 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    private void FixedUpdate()
    {
        Vector3 movement = rb.linearVelocity * Time.fixedDeltaTime;

        if (Physics.SphereCast(transform.position, hitRadius,
            movement.normalized, out RaycastHit hit, movement.magnitude))
        {
            if (hit.collider.TryGetComponent<Target>(out Target target))
            {
                target.Hit();
                Deactivate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            target.Hit();
        }

        Deactivate();
    }

    private void Deactivate()
    {
        rb.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}