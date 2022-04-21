using com.mobiquity.packer.Interfaces;
using com.mobiquity.packer.Services;

namespace com.mobiquity.packer;

public class Packer 
{
    private static readonly IFileService FileService = new FileService();
    private static readonly ICalculateService CalculateService = new CalculateService();
    
    public static string Pack(string filePath)
    {
        var packages = FileService.ParseFile(filePath);

        var result = packages.Select(package => CalculateService.FindKnapsackItems(package)).ToList();

        return string.Join(Environment.NewLine, result);
    }
}