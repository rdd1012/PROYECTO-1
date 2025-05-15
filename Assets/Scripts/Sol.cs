using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol : MonoBehaviour
{
    Light luz;
    [SerializeField] InteractableCortina[] cortinas;
    private void Start()
    {
        luz = GetComponent<Light>();
        StartCoroutine(ComprobarCortinas());
    }

    IEnumerator ComprobarCortinas()
    {
        while (true)
        {
            int contadorCortinas = 0;
            foreach (InteractableCortina cortina in cortinas)
            {
                if (cortina.IsOpen) contadorCortinas++;
            }
            //Debug.Log(contadorCortinas);
            luz.intensity = 0.0667f * contadorCortinas;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
