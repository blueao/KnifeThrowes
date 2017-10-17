using UnityEngine;
using System.Collections;

public class HandleWinGame : MonoBehaviour {

    public MainGameController MainGame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "StartMove")
        {
            MainGame.WinGame();
        }
    }
}
