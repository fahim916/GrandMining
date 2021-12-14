using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;


public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public Button digButt;

    //Step 1 create your products
    private string dig20 = "dig_20";
    private string dig60 = "dig_60";
    private string dig130 = "dig_130";
    private string dig300 = "dig_300";
    private string dig875 = "dig_875";
    private string dig2000 = "dig_2000";

    private string gem100 = "gem_100";
    private string gem500 = "gem_500";
    private string gem1000 = "gem_1000";
    private string gem2000 = "gem_2000";
    private string gem5000 = "gem_5000";
    private string gem10000 = "gem_10000";

    private string cash10k = "cash_10k";
    private string cash30k = "cash_30k";
    private string cash65k = "cash_65k";
    private string cash150k = "cash_150k";
    private string cash450k = "cash_450k";
    private string cash1m = "cash_1m";


    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(dig20, ProductType.Consumable);
        builder.AddProduct(dig60, ProductType.Consumable);
        builder.AddProduct(dig130, ProductType.Consumable);
        builder.AddProduct(dig300, ProductType.Consumable);
        builder.AddProduct(dig875, ProductType.Consumable);
        builder.AddProduct(dig2000, ProductType.Consumable);

        builder.AddProduct(gem100, ProductType.Consumable);
        builder.AddProduct(gem500, ProductType.Consumable);
        builder.AddProduct(gem1000, ProductType.Consumable);
        builder.AddProduct(gem2000, ProductType.Consumable);
        builder.AddProduct(gem5000, ProductType.Consumable);
        builder.AddProduct(gem10000, ProductType.Consumable);

        builder.AddProduct(cash10k, ProductType.Consumable);
        builder.AddProduct(cash30k, ProductType.Consumable);
        builder.AddProduct(cash65k, ProductType.Consumable);
        builder.AddProduct(cash150k, ProductType.Consumable);
        builder.AddProduct(cash450k, ProductType.Consumable);
        builder.AddProduct(cash1m, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void buyDig20()
    {
        BuyProductID(dig20);
    }
    public void buyDig60()
    {
        BuyProductID(dig60);
    }
    public void buyDig130()
    {
        BuyProductID(dig130);
    }
    public void buyDig300()
    {
        BuyProductID(dig300);
    }
    public void buyDig875()
    {
        BuyProductID(dig875);
    }
    public void buyDig2000()
    {
        BuyProductID(dig2000);
    }

    public void buyGem100()
    {
        BuyProductID(gem100);
    }
    public void buyGem500()
    {
        BuyProductID(gem500);
    }
    public void buyGem1000()
    {
        BuyProductID(gem1000);
    }
    public void buyGem2000()
    {
        BuyProductID(gem2000);
    }
    public void buyGem5000()
    {
        BuyProductID(gem5000);
    }
    public void buyGem10000()
    {
        BuyProductID(gem10000);
    }

    public void buyCash10k()
    {
        BuyProductID(cash10k);
    }
    public void buyCash30k()
    {
        BuyProductID(cash30k);
    }
    public void buyCash65k()
    {
        BuyProductID(cash65k);
    }
    public void buyCash150k()
    {
        BuyProductID(cash150k);
    }
    public void buyCash450k()
    {
        BuyProductID(cash450k);
    }
    public void buyCash1m()
    {
        BuyProductID(cash1m); 
    }

    void SetData()
    {
        DataStore.data.setData();
    }


    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, dig20, StringComparison.Ordinal))
        {
            DataStore.data.digAmount = DataStore.data.digAmount + 20;
            Manger.digRiward = true;
            if (DataStore.data.digAmount > 0)
                digButt.interactable = true;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, dig60, StringComparison.Ordinal))
        {
            DataStore.data.digAmount = DataStore.data.digAmount + 60;
            Manger.digRiward = true;
            if (DataStore.data.digAmount > 0)
                digButt.interactable = true;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, dig130, StringComparison.Ordinal))
        {
            DataStore.data.digAmount = DataStore.data.digAmount + 130;
            Manger.digRiward = true;
            if (DataStore.data.digAmount > 0)
                digButt.interactable = true;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, dig300, StringComparison.Ordinal))
        {
            DataStore.data.digAmount = DataStore.data.digAmount + 300;
            Manger.digRiward = true;
            if (DataStore.data.digAmount > 0)
                digButt.interactable = true;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, dig875, StringComparison.Ordinal))
        {
            DataStore.data.digAmount = DataStore.data.digAmount + 875;
            Manger.digRiward = true;
            if (DataStore.data.digAmount > 0)
                digButt.interactable = true;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, dig2000, StringComparison.Ordinal))
        {
            DataStore.data.digAmount = DataStore.data.digAmount + 2000;
            Manger.digRiward = true;
            if (DataStore.data.digAmount > 0)
                digButt.interactable = true;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gem100, StringComparison.Ordinal))
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 100;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gem500, StringComparison.Ordinal))
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 500;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gem1000, StringComparison.Ordinal))
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 1000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gem2000, StringComparison.Ordinal))
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 2000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gem5000, StringComparison.Ordinal))
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 5000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gem10000, StringComparison.Ordinal))
        {
            DataStore.data.gemAmount = DataStore.data.gemAmount + 10000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, cash10k, StringComparison.Ordinal))
        {
            DataStore.data.cashAmount = DataStore.data.cashAmount + 10000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, cash30k, StringComparison.Ordinal))
        {
            DataStore.data.cashAmount = DataStore.data.cashAmount + 30000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, cash65k, StringComparison.Ordinal))
        {
            DataStore.data.cashAmount = DataStore.data.cashAmount + 65000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, cash150k, StringComparison.Ordinal))
        {
            DataStore.data.cashAmount = DataStore.data.cashAmount + 150000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, cash450k, StringComparison.Ordinal))
        {
            DataStore.data.cashAmount = DataStore.data.cashAmount + 450000;
            SetData();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, cash1m, StringComparison.Ordinal))
        {
            DataStore.data.cashAmount = DataStore.data.cashAmount + 1000000;
            SetData();
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }








    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}