using UnityEngine;

public class Velocidad : MonoBehaviour {

    public float forceAmount = 300f;
    Vector3 fDirection = new Vector3(0f, 0f, 1f);
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        rb = GetComponent<Rigidbody>();


    }

    private void FixedUpdate() {

        if (Input.GetKey(KeyCode.Space)) {

            //rb.AddForce(fDirection * forceAmount * Time.deltaTime, ForceMode.Force);
            rb.AddForce(fDirection * forceAmount, ForceMode.Force);
            Debug.Log("La velocidad de la esfer es:" + rb.linearVelocity);

        }

    }
}