using System;
using UnityEngine;

public class CursorController : MonoBehaviour {
    public static CursorController Instance;

    [SerializeField]
    private Texture2D _normal, _red, _reload;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ChangeCursor(CursorState.Normal);
    }

    public static void ChangeCursor(CursorState state) {
        Texture2D texture2D = state switch {
            CursorState.Normal =>Instance. _normal,
            CursorState.Red => Instance._red,
            CursorState.Reload => Instance._reload,
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