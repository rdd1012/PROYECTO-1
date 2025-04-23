using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableVisuals : MonoBehaviour {
    Vector3 escalaOG;
    private Coroutine currentAnimation;

    private void Start()
    {
        escalaOG = transform.localScale;
    }

    public void DoSelectAnimation()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(SelectAnimation());
    }

    IEnumerator SelectAnimation()
    {
        float duration = 0.1f;
        float elapsed = 0;
        Vector3 targetScale = escalaOG * 1.1f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(escalaOG, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0;
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, escalaOG, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = escalaOG;
    }
}