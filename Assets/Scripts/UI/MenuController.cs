using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Texture2D cursor;
    public Vector2 cursorHotspot;

    public void StartGame()
    {
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
        SceneManager.LoadScene("World");
    }
}
