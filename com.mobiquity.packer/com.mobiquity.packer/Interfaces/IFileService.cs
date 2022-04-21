using com.mobiquity.packer.Models;

namespace com.mobiquity.packer.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Parse file by file path
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>Package things with index number, weight and cost</returns>
    List<Package> ParseFile(string filePath);
}