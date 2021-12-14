using UnityEngine;

public class purchaseButton : MonoBehaviour
{
    public enum PurchaseType {dig20, dig60, dig130, dig300, dig875, dig2000,
    gem100, gem500, gem1000, gem2000, gem5000, gem10000, cash10k, cash30k, cash65k,
    cash150k, cash450k, cash1m};
    public PurchaseType purchaseType;

    public void ClickPurchaseButton()
    {
        switch(purchaseType)
        {
            case PurchaseType.dig20:
                IAPManager.instance.buyDig20();
                break;
            case PurchaseType.dig60:
                IAPManager.instance.buyDig60();
                break;
            case PurchaseType.dig130:
                IAPManager.instance.buyDig130();
                break;
            case PurchaseType.dig300:
                IAPManager.instance.buyDig300();
                break;
            case PurchaseType.dig875:
                IAPManager.instance.buyDig875();
                break;
            case PurchaseType.dig2000:
                IAPManager.instance.buyDig2000();
                break;
            case PurchaseType.gem100:
                IAPManager.instance.buyGem100();
                break;
            case PurchaseType.gem500:
                IAPManager.instance.buyGem500();
                break;
            case PurchaseType.gem1000:
                IAPManager.instance.buyGem1000();
                break;
            case PurchaseType.gem2000:
                IAPManager.instance.buyGem2000();
                break;
            case PurchaseType.gem5000:
                IAPManager.instance.buyGem5000();
                break;
            case PurchaseType.gem10000:
                IAPManager.instance.buyGem10000();
                break;
            case PurchaseType.cash10k:
                IAPManager.instance.buyCash10k();
                break;
            case PurchaseType.cash30k:
                IAPManager.instance.buyCash30k();
                break;
            case PurchaseType.cash65k:
                IAPManager.instance.buyCash65k();
                break;
            case PurchaseType.cash150k:
                IAPManager.instance.buyCash150k();
                break;
            case PurchaseType.cash450k:
                IAPManager.instance.buyCash450k();
                break;
            case PurchaseType.cash1m:
                IAPManager.instance.buyCash1m();
                break;
        }
    }
}
