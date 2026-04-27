using UnityEngine;
using System.Collections;

public class PelotaController : MonoBehaviour {

    Rigidbody rb;
    AudioSource audioSource;

    public float fuerzaEmpuje = 10f;
    public float fuerzaTorque = 10f;

    bool usandoTorque = false;
    bool pausado = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

        // PAUSA CON ESC
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PausarJuego();
        }

        if (pausado) return;

        // EMPUJE INICIAL
        if (Input.GetKeyDown(KeyCode.Space) && !usandoTorque) {
            rb.AddForce(Vector3.forward * fuerzaEmpuje, ForceMode.Impulse);

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("ZonaTorque")) {
            usandoTorque = true;

            StartCoroutine(MovimientoTorque());
        }
    }

    IEnumerator MovimientoTorque() {

        float tiempo = 0;

        while (tiempo < 5f) {
            rb.AddTorque(Vector3.up * fuerzaTorque);
            tiempo += Time.deltaTime;
            yield return null;
        }

        // detener velocidad
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // esperar
        yield return new WaitForSeconds(3f);

        // volver a moverse
        StartCoroutine(MovimientoTorque());
    }

    void PausarJuego() {

        pausado = !pausado;

        if (pausado) {
            Time.timeScale = 0;
            AudioListener.pause = true;
        } else {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

    }
}