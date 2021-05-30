using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    public float accelerationTime = 1f;

    private float timeToChangeDirection;
    private Rigidbody rb;
    private Vector3 movement;
    private bool stopped = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            movement = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            timeToChangeDirection += accelerationTime;
        }
    }

    void FixedUpdate()
    {
        if (!stopped)
        {
            rb.AddForce(movement);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("stop!!!!");
            stopped = true;
        }
    }
}
