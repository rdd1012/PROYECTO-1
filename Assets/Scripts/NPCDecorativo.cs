using UnityEngine;
using System.Collections;

public class NPCDecorativo : NPCBase {
    [SerializeField] Sprite normal;
    [SerializeField] Sprite pesta�eo;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Blink(normal, pesta�eo,spriteRenderer));
    }
}
   