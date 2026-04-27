using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class S_Movement : MonoBehaviour
{
    public float forceAmount = 10f;
    public float torqueForce = 10f;
    public AudioSource audioBall;

    Vector3 fDirection = new Vector3(1, 0, 0);
    private Rigidbody rb;
    private bool moveBall = false;
    private bool pauseBall = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioBall = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveBall = true;
            rb.AddForce(fDirection * forceAmount, ForceMode.Impulse);
            audioBall.Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseBall == false)
            {
                Time.timeScale = 0f;
                pauseBall = true;
                audioBall.Pause();
            }
            else if (pauseBall == true)
            {
                Time.timeScale = 1f;
                pauseBall = false;
                audioBall.UnPause();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TorqueZone"))
        {
            StartCoroutine(Torque());
        }
    }
    IEnumerator Torque()
    {
        float timer = 0f;

        while (timer < 2f)
        {
            rb.AddTorque(Vector3.forward * torqueForce);
            timer += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(10f);

        timer = 0f;

        while (timer < 10f)
        {
            rb.AddTorque(Vector3.forward * torqueForce);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
