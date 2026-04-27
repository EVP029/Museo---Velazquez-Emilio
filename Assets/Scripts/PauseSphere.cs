using UnityEngine;
using System.Collections;

public class PauseSphere : MonoBehaviour {
    Rigidbody rb;

    public float delayAntes = 2f;   // espera antes de pausar
    public float tiempoPausa = 3f;  // tiempo que se queda pausado

    private bool enProceso = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !enProceso) {
            StartCoroutine(Secuencia());
        }
    }

    IEnumerator Secuencia() {
        enProceso = true;

        // Espera antes de hacer la pausa
        yield return new WaitForSeconds(delayAntes);

        // PAUSA (tu lógica original)
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // Tiempo pausado
        yield return new WaitForSeconds(tiempoPausa);

        // REANUDA
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

        enProceso = false;
    }
}
