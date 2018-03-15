using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Purchasing;

public class UnityIAPManager : MonoBehaviour,IStoreListener {
	private static UnityIAPManager instance;
    private IStoreController controller;
    private IExtensionProvider extensions;

    public static UnityIAPManager Instance{get{return instance;}}

    public delegate void FinishBuyProduct(string productId);
    public event FinishBuyProduct OnFinishBuyProduct;

	public delegate void FailToBuyProduct (string productId);
	public event FailToBuyProduct OnFailToBuyProduct;

    void Awake(){
    	if(instance != this && instance != null){
			Destroy (this.gameObject);
    	} else{
			instance = this;
    	}
		DontDestroyOnLoad (this.gameObject);
    }

    void Start(){
		InitUnityIAP ();
    }

	public void InitUnityIAP () {
		//if it's already initialized
		if(IsInitialized()){
			return;
		}

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct (ShortCode.ProductIDs.id_Gem1, ProductType.Consumable);
		builder.AddProduct (ShortCode.ProductIDs.id_Gem2, ProductType.Consumable);
		builder.AddProduct (ShortCode.ProductIDs.id_Gem3, ProductType.Consumable);
		builder.AddProduct (ShortCode.ProductIDs.id_Gem4, ProductType.Consumable);
        UnityPurchasing.Initialize (this, builder);
    }

    bool IsInitialized(){
		return (controller != null && extensions != null);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed (InitializationFailureReason error)
    {

    }

    public void BuyConsumable(string productID){
		BuyProductID (productID);
    }

    void BuyProductID(string productID){
    	if(IsInitialized()){
			Product product = controller.products.WithID (productID);
//			Debug.Log ("string productID:" + productID);
//			Debug.Log ("inited productID:" + product.definition.storeSpecificId);
			if(product != null && product.availableToPurchase){
//				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				controller.InitiatePurchase (product);
			} else{
				if (OnFailToBuyProduct != null)
				OnFailToBuyProduct ("Purchase failed. Item is not found or not available to purchase");
			}

    	} else{
    		//InitFailed
			if (OnFailToBuyProduct != null)
				OnFailToBuyProduct ("Initialization failed");
    	}
    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
    {
    	string purchasedProductID = "";
		 if(string.Equals(e.purchasedProduct.definition.id,ShortCode.ProductIDs.id_Gem1,StringComparison.Ordinal)){
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", e.purchasedProduct.definition.id));
			purchasedProductID = ShortCode.ProductIDs.id_Gem1;
		} else if(string.Equals(e.purchasedProduct.definition.id,ShortCode.ProductIDs.id_Gem2,StringComparison.Ordinal)){
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", e.purchasedProduct.definition.id));
			purchasedProductID = ShortCode.ProductIDs.id_Gem2;
		} else if(string.Equals(e.purchasedProduct.definition.id,ShortCode.ProductIDs.id_Gem3,StringComparison.Ordinal)){
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", e.purchasedProduct.definition.id));
			purchasedProductID = ShortCode.ProductIDs.id_Gem3;
		} else if(string.Equals(e.purchasedProduct.definition.id,ShortCode.ProductIDs.id_Gem4,StringComparison.Ordinal)){
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", e.purchasedProduct.definition.id));
			purchasedProductID = ShortCode.ProductIDs.id_Gem4;
     	} 

     	if(string.IsNullOrEmpty(purchasedProductID)){
//			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", e.purchasedProduct.definition.id));
			if (OnFailToBuyProduct != null)
				OnFailToBuyProduct ("Purchased failed. Unknown product");
     	}
     	else {
			if (OnFinishBuyProduct != null)
				OnFinishBuyProduct (purchasedProductID);
     	}

        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
    {
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}