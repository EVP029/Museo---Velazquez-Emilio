using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DominoForce : MonoBehaviour {
    public Rigidbody esfera;
    public TextMeshProUGUI textoUI;

    public ForceMode modoFuerza = ForceMode.Impulse;
    public float fuerza = 10f;

    void Start() {
        ActualizarTexto();
    }

    void Update() {
        // Empujar con espacio
        if (Input.GetKeyDown(KeyCode.Space)) {
            esfera.AddForce(Vector3.forward * fuerza, modoFuerza);
        }

        // Cambiar tipo de fuerza con teclas
        if (Input.GetKeyDown(KeyCode.W)) {
            modoFuerza = ForceMode.Force;
            ActualizarTexto();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            modoFuerza = ForceMode.Impulse;
            ActualizarTexto();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            modoFuerza = ForceMode.VelocityChange;
            ActualizarTexto();
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            modoFuerza = ForceMode.Acceleration;
            ActualizarTexto();
        }
    }

    void ActualizarTexto() {
        textoUI.text = "ForceMode: " + modoFuerza.ToString();
    }
}