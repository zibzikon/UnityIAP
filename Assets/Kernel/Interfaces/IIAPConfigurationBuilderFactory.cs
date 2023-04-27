using UnityEngine.Purchasing;

namespace Kernel.Interfaces
{
    public interface IIAPConfigurationBuilderFactory
    {
        ConfigurationBuilder CreateBuilder(ProductCatalog catalog);
    }
}