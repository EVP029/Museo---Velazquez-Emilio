using System.Collections.Generic;
using UnityEngine;

public class ClonTarea : MonoBehaviour {
    public GameObject myPrefab;
    public Vector3 startPosition;

    private List<GameObject> clones = new List<GameObject>();

    void Update() {
        // Al SOLTAR la barra espaciadora
        if (Input.GetKeyUp(KeyCode.Space)) {
            GenerateClones();
        }

        // Tecla T → desactivar clones
        if (Input.GetKeyDown(KeyCode.T)) {
            foreach (GameObject c in clones) {
                c.SetActive(false);
            }
        }

        // Tecla D → destruir scripts de los clones
        if (Input.GetKeyDown(KeyCode.D)) {
            foreach (GameObject c in clones) {
                Destroy(c.GetComponent<CloneLife>());
            }
        }
    }

    void GenerateClones() {
        clones.Clear();

        int totalClones = 6;
        int columns = 2;
        float spacing =2f;

        for (int i = 0; i < totalClones; i++) {
            int row = i / columns;
            int col = i % columns;

            Vector3 position = startPosition + new Vector3(col * spacing, 0, row * spacing);

            GameObject clone = Instantiate(myPrefab, position, Quaternion.identity);
            clones.Add(clone);
        }
    }

    // Este método lo conectas al botón X en Unity
    public void DestroyClones() {
        foreach (GameObject c in clones) {
            Destroy(c);
        }

        clones.Clear();
    }
}
