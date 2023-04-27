# IAP Product Processors

IAP Product Processors exist to respond to the purchase of a product.

There are 2 different types of IAPProductProcessors

AtomIAPProductProcessor - more preferred. Used for creating behaviors that do not depend on the scene
MonoIAPProductProcessor - Used for creating behaviors that depend on the scene


there is the code that describes the IIAPProductProcessor interface.

```csharp
public interface IIAPProductProcessor
{
    string ProductID { get; }
    PurchaseProcessingResult Process(Product product);
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason);
}
```

"ProductID" is used to contain information about the product identifier, with the help of this IAPStore will be able to understand which processor should be used to get the correct reaction to the purchase of the product


The “Process” method is the main part of the processor. It needs to implement the main logic of the purchasing. The example for disabling ads from the game is below.


And the "OnPurchaseFailed" method is called when the purchase has failed. For example, a player canceled a purchase.


Example of the simple processor that disables the ads on “remove_ads” product purchased.

```csharp
public class RemoveAdsProcessor: AtomIAPProductProcessor
{
    private readonly IAdsRunner _adsRunner;
    public override string ProductID => "remove_ads";
    
    public RemoveAdsProcessor(IAdsRunner adsRunner)
    {
        _adsRunner = adsRunner;
    }
    
    public override PurchaseProcessingResult Process(Product product)
    {
        _adsRunner.Disable();
        return PurchaseProcessingResult.Complete;
    }
}
```

Also, all the processors with the same "ProductID" that have been added to the IAPStore will be executed in the order in which they were added

# Bootstrap

Bootstrap is the script that boots IAP and other elements that can be related to it.

```csharp
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private IAPStore _iapStore;
    
    //Put your mono processors to this field in scene 
    [SerializeField] private List<MonoIAPProductProcessor> monoIAPProductPurchaseProcessors;

    private void Awake() => Boot();
    
    private async void Boot()
    {
        await UnityGamingServiceLoader.LoadAsync();
        
        //Cerate services
        var adsRunner = new AdsRunner();
        
        //Proccesors cration
        var processors = GetProcessors(adsRunner);
        
        _iapStore.Initialize(processors);
        var iapConfigurationBuilderFactory = new IAPConfigurationBuilderFactory();
        
        var storeLoader = new IAPStoreLoader(iapConfigurationBuilderFactory, _iapStore, true);
        
        storeLoader.BootIAPStore();
    }

    private IEnumerable<IIAPProductProcessor> GetProcessors(IAdsRunner adsRunner)
    {
        foreach (var monoProcessor in monoIAPProductPurchaseProcessors) yield return monoProcessor;

        yield return new RemoveAdsProcessor(adsRunner);
        //Put your atom processors here by this template ** yield return new ProcessorCalssName(arguments); **
    }
}
```



# IIAPStore

IIAPStore is the main part of the whole IAP controlling.

Read the NAMES of the elements and you will understand their functionality



```csharp
public interface IIAPStore
{
    event Action<Product, PurchaseFailureReason> PurchaseFailed;
    event Action<Product> PurchaseProcessed;
    event Action<bool, string> TransactionsRestored;
    
    
    void Purchase(string productId);
    void AddProcessor(IIAPProductProcessor processor);
    void RemoveProcessor(IIAPProductProcessor processor);
    Product GetProduct(string productId);
}
```

Purchase - is a method that exists to provoke a purchase by calling the more low-level “IStoreController.InitiatePurchase(productId)” method.
There is how purchase works under the hood:






AddProcessor - is a method that is used for Adding processors dynamically. Call it where you want to add a new processor.


RemoveProcessor - is a method that is used for REMOVING processors dynamically. Call it where you want to remove the processor.


Be careful that is if IAPSotre doesn't contain any processor that depends on the ID of the product you trying to purchase, then you will get an error in the console stating "The given key "PRODUCTID" was not present in the dictionary".


RemoveProcessor - is a method that gives you a product by their id. Call it where you want to get information about the product.


Here are a few ways that show how you can use the IIAPStore :


Create a class that depends on IIAPStore


Then Implement your super complex logic;


public class IAPStoreTest
{
private readonly IIAPStore _iapStore;


public IAPStoreTest(IIAPStore iapStore)
{
_iapStore = iapStore;
}


private void Do()
{
_iapStore.Purchase("remove_ads");
}
}




Then in the Bootstrap script or any other class/script that has a link to the IAPStore object, you need to add the line that creates a new Object with the IAPStore argument


var testClass = new IAPStoreTest(iapStore);


If you need to do the same with an object that is on the stage (any MonoBehavior script), you need to do it a little differently.


First, add the "Initialize(IIAPStore iapStore)" method to your MonoBehaviour Class.


public class IAPStoreMonoTest: MonoBehaviour
{
private IIAPStore _iapStore;


public void Initialize(IIAPStore iapStore)
{
_iapStore = iapStore;
}


private void Do()
{
_iapStore.Purchase("remove_ads");
}
}


Then add the field used to store your object into the Bootstrap script or any other script that has a link to the IAPStore object
[SerializeField] private IAPStoreMonoTest iapStoreMonoTest;



After that, initialize the object in the place you want.


For example, in Bootstrap it is the end of the “Boot” method


private async void Boot()
{
///
//Some code
var iapStore = new IAPStore(purchaseProcessors);
///
//Some code
iapStoreMonoTest.Initialize(iapStore);  
}




Adding Products

To add the new product you need to open the Services->In-App Purchasing->IAP Catalog window.




After opening the window you will see the product list so you can control the products from it.
You can read more about this window from this article:
https://docs.unity3d.com/Packages/com.unity.purchasing@4.0/manual/UnityIAPDefiningProducts.html




















Buttons

IAPButton

IAPButton is a component that can be placed on a UI element with a button component
After setting the productID and launching the game, if the button is pressed, it will automatically trigger a purchase.




IAPStoreUIElement


IAPStoreUIElement It is my custom version of the previous IAPButton.


But the component can be placed on the object without the Button component because this requires putting the button component into the field.


Please note that the IAPStoreUIElement must be initialized by calling the “Initialize(arg)” method from the script that contains the IIAPStore object.

upd change the UpdateText

