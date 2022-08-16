namespace Persistence.Configuration;

public class StorageOptions
{
    public StorageOptions()
    {
        
    }
    public StorageOptions(string? storageContainerName, string? connectionString)
    {
        StorageContainerName = storageContainerName;
        ConnectionString = connectionString;
    }

    public string? StorageContainerName { get; set; }
    public string? ConnectionString { get; set; }
}