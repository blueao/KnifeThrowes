using UnityEngine;
using System.Collections;

public class HandleButtonShop : MonoBehaviour {

    public void OnClickMySelf()
    {
        int index = int.Parse(gameObject.name.Remove(0, 9));
        ModelHandle.Instance.setSpriTemp(index);
        if (transform.GetChild(2).GetComponent<HandleButtonBuy>().isCanbuy)
        {
            ModelHandle.Instance.setActiveLock(true);
        }
        else
            ModelHandle.Instance.setActiveLock(false);
        ModelHandle.Instance.setActiveLock(true);
    }
}
