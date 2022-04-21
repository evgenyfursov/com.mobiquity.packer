namespace com.mobiquity.packer.Models;

public class Package
{
    public int Weight { get; set; }
    public List<PackageItem> Items { get; set; } = new();
}