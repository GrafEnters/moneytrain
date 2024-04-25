using System;
using UnityEngine;

public class CursorController : MonoBehaviour {
    [SerializeField]
    private Texture2D _normal, _red, _reload;

    private void Start() {
        ChangeCursor(CursorState.Normal);
    }

    public void ChangeCursor(CursorState state) {
        Texture2D texture2D = state switch {
            CursorState.Normal => _normal,
            CursorState.Red => _red,
            CursorState.Reload => _reload,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };

        Cursor.SetCursor(texture2D, new Vector2(texture2D.width / 2f, texture2D.height / 2f), CursorMode.ForceSoftware);
    }
}

public enum CursorState {
    Normal,
    Red,
    Reload
}