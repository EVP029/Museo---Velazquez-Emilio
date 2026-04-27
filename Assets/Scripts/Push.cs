using UnityEngine;

public class Push : MonoBehaviour {
    [SerializeField]
    private float torqueAmount = 300f;
    
    Rigidbody rb;
    Vector3 torqueDireccion = new Vector3(0f, 1f, 0f);

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(torqueDireccion * torqueAmount, ForceMode.Force);

        }
    }
}