using UnityEngine;
using System.Collections;

public class HandleButtonShop : MonoBehaviour {

    public ScrollRectController MainScroller;
    public void OnClickMySelf()
    {
        int index = int.Parse(gameObject.name.Remove(0, 9));
        ModelHandle.Instance.setSpriTemp(index);
       
    }
}
