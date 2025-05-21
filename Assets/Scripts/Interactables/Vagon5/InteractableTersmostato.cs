using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableTermostato : MonoBehaviour, IInteractable {
    private InteractableData interactableData;
    AudioSource audioSource;
    public InteractableData InteractableData
    {
        get { return interactableData; }
        set { interactableData = value; }
    }


    private float temperatura = 0f;
    public float Temperatura
    {
        get { return temperatura; }
        set { temperatura = value; }
    }

    [SerializeField] private List<SpriteRenderer> vahoLista;
    [SerializeField] private Canvas termostatoPantalla;

    private Coroutine currentVahoCoroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    private bool isVahoVisible = false;
    public bool IsvahoVisible{get {return isVahoVisible;}  set{ isVahoVisible = value; } }

    private void Update()
    {
        bool shouldShow = temperatura >= 0.75f;  

        if (shouldShow != isVahoVisible)
        {
            isVahoVisible = shouldShow;

            if (currentVahoCoroutine != null)
            {
                StopCoroutine(currentVahoCoroutine);
            }

            currentVahoCoroutine = StartCoroutine(
                shouldShow ? MostrarVaho() : OcultarVaho()
            );
        }
    }
    public void Sonar() 
    {
        if (!audioSource.isPlaying) audioSource.Play();

    }

    IEnumerator MostrarVaho()
    {
        float duration = 1f;
        float elapsed = 0f;

        
        List<float> startAlphas = new List<float>();
        foreach (SpriteRenderer sp in vahoLista)
        {
            startAlphas.Add(sp.color.a);
        }

        while (elapsed < duration)
        {
            foreach (SpriteRenderer sp in vahoLista)
            {
                int index = vahoLista.IndexOf(sp);
                float alpha = Mathf.Lerp(startAlphas[index], 1f, elapsed / duration);
                Color tmp = sp.color;
                tmp.a = alpha;
                sp.color = tmp;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        
        foreach (SpriteRenderer sp in vahoLista)
        {
            Color tmp = sp.color;
            tmp.a = 1f;
            sp.color = tmp;
        }
    }

    IEnumerator OcultarVaho()
    {
        float duration = 1f;
        float elapsed = 0f;

        
        List<float> startAlphas = new List<float>();
        foreach (SpriteRenderer sp in vahoLista)
        {
            startAlphas.Add(sp.color.a);
        }

        while (elapsed < duration)
        {
            foreach (SpriteRenderer sp in vahoLista)
            {
                int index = vahoLista.IndexOf(sp);
                float alpha = Mathf.Lerp(startAlphas[index], 0f, elapsed / duration);
                Color tmp = sp.color;
                tmp.a = alpha;
                sp.color = tmp;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        
        foreach (SpriteRenderer sp in vahoLista)
        {
            Color tmp = sp.color;
            tmp.a = 0f;
            sp.color = tmp;
        }
    }

}