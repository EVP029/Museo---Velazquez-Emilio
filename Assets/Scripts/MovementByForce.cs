using UnityEngine;
using TMPro;

public class MovementByForce : MonoBehaviour
{
    public Rigidbody rb;
    public TextMeshProUGUI forceText;

    public ForceMode forceMode = ForceMode.Impulse;

    void Start()
    {
        forceText.text = "ForceMode: " + forceMode.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.right * 10f, forceMode);
        }
    }
}