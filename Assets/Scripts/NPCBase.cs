using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBase :MonoBehaviour {
    public virtual IEnumerator Yap(string _text, Sprite _hablando, Sprite _normal, SpriteRenderer _spriteRenderer, YapBubble _yapbubble)
    {
        _spriteRenderer.sprite = _hablando;
        _yapbubble.gameObject.SetActive(true);
        _yapbubble.SetupText(_text);
        yield return new WaitForSeconds(3f);
        _yapbubble.gameObject.SetActive(false);
        _spriteRenderer.sprite = _normal;
    }
    public IEnumerator Blink(Sprite _pestañeo,SpriteRenderer _spriteRenderer)
    {
        Sprite _normal; 

        while (true) 
        {
            yield return new WaitForSeconds(4f);
            _normal = _spriteRenderer.sprite;
            _spriteRenderer.sprite = _pestañeo; 
            yield return new WaitForSeconds(0.1f); 
            _spriteRenderer.sprite = _normal; 
        }

    }
}
