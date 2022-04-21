using com.mobiquity.packer.Models;

namespace com.mobiquity.packer.Interfaces;

public interface ICalculateService
{
    /// <summary>
    /// Dynamic programming based implementation of the 0-1 knapsack problem 
    /// </summary>
    /// <param name="package"></param>
    /// <returns>String with a list of things</returns>
    string FindKnapsackItems(Package package);
}