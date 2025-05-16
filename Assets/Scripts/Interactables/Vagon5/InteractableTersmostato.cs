using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableTermostato : MonoBehaviour, IInteractable {
    private InteractableData interactableData;
    public InteractableData InteractableData
    {
        get { return interactableData; }
        set { interactableData = value; }
    }

    [SerializeField] private float temperatura;
    public float Temperatura
    {
        get { return temperatura; }
        private set { temperatura = value; }
    }

    [SerializeField] private List<SpriteRenderer> vahoLista;
    [SerializeField] private Canvas termostatoPantalla;

    private Coroutine currentVahoCoroutine;

    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        InitializeVahoAlpha();
    }

    private void InitializeVahoAlpha()
    {
        foreach (SpriteRenderer sp in vahoLista)
        {
            Color tmp = sp.color;
            tmp.a = 0f;
            sp.color = tmp;
        }
    }

    public void OnClickAction()
    {
        termostatoPantalla.gameObject.SetActive(true);
    }

    public bool TieneItem() { return true; }
    public bool IsInteractable() { return true; }

    private void Update()
    {

        if (temperatura >= 0.75f && currentVahoCoroutine == null)
        {
            currentVahoCoroutine = StartCoroutine(MostrarVaho());
        }
        else if (temperatura < 0.75f && currentVahoCoroutine != null)
        {
            StopCoroutine(currentVahoCoroutine);
            currentVahoCoroutine = StartCoroutine(OcultarVaho());
        }
    }

    IEnumerator MostrarVaho()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            foreach (SpriteRenderer sp in vahoLista)
            {
                Color tmp = sp.color;
                float alpha= Mathf.Lerp(0f, 1f, elapsed / duration); ;
                tmp = new Color(tmp.r, tmp.g, tmp.b, alpha);
                sp.color = tmp;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator OcultarVaho()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            foreach (SpriteRenderer sp in vahoLista)
            {
                Color tmp = sp.color;
                float alpha = Mathf.Lerp(0f, 1f, elapsed / duration); ;
                tmp = new Color(tmp.r, tmp.g, tmp.b, alpha);
                sp.color = tmp;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}