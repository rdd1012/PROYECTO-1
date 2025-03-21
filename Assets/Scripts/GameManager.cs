using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private Texture2D interactiveCursorTexture;

    
    public void SetCursorInteractive()
    {
        Cursor.SetCursor(interactiveCursorTexture, default, default);
    }
    public void SetCursorDefault()
    {
        Cursor.SetCursor(default, default, default);
    }
}