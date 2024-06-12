namespace Lanpuda.Lims;

public static class LimsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Lims_";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Lims";

    public const int MaxTitleLength = 128;
    
}
