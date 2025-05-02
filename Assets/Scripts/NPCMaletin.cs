using UnityEngine;
using System.Collections;

public class NPCMaletin : NPCBase {
    private InteractableData interactableData;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite pestañeo;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        interactableData = GetComponent<InteractableData>();
        StartCoroutine(Blink(normal, pestañeo,spriteRenderer));
    }
}
   