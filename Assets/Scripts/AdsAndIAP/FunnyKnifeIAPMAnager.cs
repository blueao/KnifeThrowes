using UnityEngine;
using System.Collections;

public class FunnyKnifeIAPMAnager : MonoBehaviour
{

    public string CONSUMABLE_PRODUCT_ID = "product id";
    public int ItemValue = 0;

    public string[] ListProductId;
    public int[] ListProductValue;

    private void Start()
    {
        InitIAPManager();
    }

    public void InitIAPManager()
    {
        UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
        UM_InAppPurchaseManager.Client.OnServiceConnected += OnConnectFinished;
        UM_InAppPurchaseManager.Client.OnServiceConnected += OnBillingConnectFinishedAction;
        UM_InAppPurchaseManager.Client.Connect();
    }

    public void PurchaseProduct( int index)
    {
        if (UM_InAppPurchaseManager.Client.IsConnected)
        {
            CONSUMABLE_PRODUCT_ID = ListProductId[index];
            ItemValue = ListProductValue[index];
            UM_InAppPurchaseManager.Client.Purchase(CONSUMABLE_PRODUCT_ID);
        }
        else
        {
            CONSUMABLE_PRODUCT_ID = ListProductId[index];
            ItemValue = ListProductValue[index];
            UM_InAppPurchaseManager.Client.Connect();
            UM_InAppPurchaseManager.Client.Purchase(CONSUMABLE_PRODUCT_ID);
        }
    }

    private void OnConnectFinished(UM_BillingConnectionResult result)
    {

        if (result.isSuccess)
        {
            Debug.Log("Billing init Success");
        }
        else
        {
            Debug.Log("Billing init Failed");
        }
    }

    private void OnPurchaseFlowFinishedAction(UM_PurchaseResult result)
    {
        UM_InAppPurchaseManager.Client.OnPurchaseFinished -= OnPurchaseFlowFinishedAction;
        if (result.isSuccess)
        {
            Debug.Log("Product " + result.product.id + " purchase Success");
            if (result.product.id == CONSUMABLE_PRODUCT_ID)
            {
                ModelHandle.Instance.PurchaseSuccess(ItemValue);
            }
        }
        else
        {
            Debug.Log("Product " + result.product.id + " purchase Failed");
        }
    }

    private void OnBillingConnectFinishedAction(UM_BillingConnectionResult result)
    {
        UM_InAppPurchaseManager.Client.OnServiceConnected -= OnBillingConnectFinishedAction;
        if (result.isSuccess)
        {
            Debug.Log("Connected");
        }
        else
        {
            Debug.Log("Failed to connect");
        }
    }
}
