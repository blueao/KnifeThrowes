using UnityEngine;
using System.Collections;

public class HandleButtonShop : MonoBehaviour {

    public void OnClickMySelf()
    {
        ModelHandle.Instance.ClosePanelSound();
        int index = int.Parse(gameObject.name.Remove(0, 9));
        ModelHandle.Instance.setSpriTemp(index);
        if (transform.GetChild(1).GetComponent<HandleButtonBuy>().IsCanbuy)
        {
            ModelHandle.Instance.setActiveLock(true);
        }
        else
            ModelHandle.Instance.setActiveLock(false);
        //ModelHandle.Instance.setActiveLock(true);
    }
}
