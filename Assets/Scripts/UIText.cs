using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour {

    public Rigidbody rb;
    public TextMeshProUGUI velocityText;


    void Update() {
        velocityText.text = rb.linearVelocity.ToString();
    }
}