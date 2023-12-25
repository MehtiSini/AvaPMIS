namespace AvaPMIS.Main;

public static class MainDbProperties
{
    public const string ConnectionStringName = "Main";
    public static string DbTablePrefix { get; set; } = "Main";

    public static string DbSchema { get; set; } = null;
}