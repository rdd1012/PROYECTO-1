using UnityEngine;
using System.Collections;

public class NPCDecorativo : NPCBase {
    [SerializeField] Sprite normal;
    [SerializeField] Sprite pestañeo;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Blink(normal, pestañeo,spriteRenderer));
    }
}
   