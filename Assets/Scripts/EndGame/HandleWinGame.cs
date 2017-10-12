using UnityEngine;
using System.Collections;

public class HandleWinGame : MonoBehaviour
{

    public MainGameController MainGame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "StartMove")
        {
            MainGame.CanMove = false;
            StartCoroutine(WaitForWin());
        }
    }
    IEnumerator WaitForWin()
    {
        yield return new WaitUntil(() => MainGame.CanWin);
        MainGame.WinGame();
    }
}
