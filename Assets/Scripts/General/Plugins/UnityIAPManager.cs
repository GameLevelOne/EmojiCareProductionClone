using UnityEngine;
using UnityEngine.Purchasing;

public class UnityIAPManager : IStoreListener {

    private IStoreController controller;
    private IExtensionProvider extensions;

    string id_Coin1 = "emojicare.coin1";
    string id_Gem1 = "emojicare.gem1";
    string id_Gem2 = "emojicare.gem2";
    string id_Gem3 = "emojicare.gem3";
    string id_Gem4 = "emojicare.gem4";

	public UnityIAPManager () {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct (id_Coin1, ProductType.Consumable);
		builder.AddProduct (id_Gem1, ProductType.Consumable);
		builder.AddProduct (id_Gem2, ProductType.Consumable);
		builder.AddProduct (id_Gem3, ProductType.Consumable);
		builder.AddProduct (id_Gem4, ProductType.Consumable);
        UnityPurchasing.Initialize (this, builder);
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

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed (Product i, PurchaseFailureReason p)
    {

    }
}