using UnityEngine;
using System.Collections;

public class HandleWinGame : MonoBehaviour {

    public MainGameController MainGame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "StartMove")
        {
            MainGame.CanMove = false;
            StartCoroutine(WaitForWin());
        }
    }
    private void Update()
    {
        if (MainGame.CanMove)
        {
            for (int i = 0; i < MainGame.ListTotalObject.Count; i++)
            {
                if (MainGame.ListTotalObject[i].activeSelf)
                {
                    break;
                }
                if (!MainGame.ListTotalObject[MainGame.ListTotalObject.Count-1].activeSelf)
                {
                    MainGame.CanWin = true;
                }
            }
        }
    }
    IEnumerator WaitForWin()
    {
        yield return new WaitUntil(()=> MainGame.CanWin);
        MainGame.WinGame();
    }
}
