namespace WebApi.Data;

public class SuperHeroMongoSetting
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SuperHeroCollectionName { get; set; } = null!;
}