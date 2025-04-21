using System.Collections;
using UnityEngine;

public class Lemings : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    Rigidbody body;
    public float rayLength = 2.0f;
    public float deathSpeed = 10;

    bool collided = false;
    float prevSpeed = 0;
    bool dead = false;
    public LayerMask wallLayer;
    public LayerMask dangerLayer;
    public LayerMask saveLayer;
    public LayerMask teleportLayer;
    public AudioSource deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Moving()
    {
        RaycastHit hit;
        Ray checkGround = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        if (Physics.Raycast(checkGround, out hit, rayLength))
        {
            animator.SetBool("Grounded", true);
            body.linearVelocity = transform.TransformDirection(new Vector3(speed, body.linearVelocity.y, 0));
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    void Collision()
    {
        RaycastHit hit;
        Ray checkGround = new Ray(transform.position, transform.TransformDirection(-Vector3.left));
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.left));
        if (Physics.Raycast(checkGround, out hit, rayLength, wallLayer))
        {
            collided = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        if (!collided)
        {
            Moving();
            Collision();
        }
        else
        {
            collided = false;
            transform.Rotate(Vector3.down * 180);
        }

        if (Mathf.Abs(body.linearVelocity.y) < 0.01)
        {
            if (Mathf.Abs(prevSpeed) > deathSpeed)
            {
                Death();
            }
        }
        prevSpeed = body.linearVelocity.y;

    }

    void Death()
    {
        dead = true;
        animator.SetTrigger("Death");
        deathSound.Play();
        StartCoroutine(FinalDeath());
    }

    void Save()
    {
        Destroy(gameObject);
    }

    IEnumerator FinalDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((dangerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Death();
        }
        if ((saveLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Save();
        }
        if ((teleportLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            var teleport = other.GetComponent<Teleport>();
            transform.position = teleport.destination;
        }
    }
}
