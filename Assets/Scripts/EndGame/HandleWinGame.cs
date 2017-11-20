using UnityEngine;
using System.Collections;

public class HandleWinGame : MonoBehaviour
{
    Coroutine WinGame;
    public MainGameController MainGame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "StartMove")
        {
            MainGame.CanMove = false;
            WinGame = StartCoroutine(WaitForWin());
        }
    }
    IEnumerator WaitForWin()
    {
      
        yield return new WaitForSeconds(10f);
        if (ModelHandle.Instance.isLose)
        {
            StopCoroutine(WinGame);
        }
        else
        MainGame.WinGame();
    }
}
