namespace AppStoreFramework.Infrastructure.Interfaces.Configuration
{
    public interface IConfigurationManager
    {
        T GetSetting<T>(string key);
    }
}
