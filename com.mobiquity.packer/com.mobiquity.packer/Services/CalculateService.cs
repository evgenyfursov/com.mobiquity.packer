using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Interfaces;
using com.mobiquity.packer.Models;

namespace com.mobiquity.packer.Services;

public class CalculateService : ICalculateService
{
    // dynamic programming based implementation
    public string FindKnapsackItems(Package package)
    {
        ValidatePackage(package);
            
        var items = package.Items.ToArray();
        var count = items.Length;
        
        var dp = new int[count + 1,package.Weight + 1];
 
        for (var i = 0; i <= count; i++)
            for (var w = 0; w <= package.Weight; w++)
                if (i == 0 || w == 0)
                    dp[i,w] = 0;
                else if (items[i - 1].Weight <= w)
                    dp[i, w] = Math.Max(items[i - 1].Cost + dp[i - 1, w - items[i - 1].Weight], dp[i - 1, w]);
                else
                    dp[i, w] = dp[i - 1, w];
     
 
        var cost = dp[count,package.Weight];

        if (cost == 0)
            return "-";
        
        var weight = package.Weight;
        var result = new List<int>();
        for (var i = count; i > 0 && cost > 0; i--) {
            while (cost == dp[i,weight - 1] && weight >= 0)
                weight -= 1;
            if (cost != dp[i - 1,weight]) {
                result.Add(items[i - 1].Index);
                cost -= items[i - 1].Cost;
                weight -= items[i - 1].Weight;
            }
        }

        result.Sort();
        return string.Join(",", result);
    }

    private void ValidatePackage(Package package)
    {
        if (package.Weight > 10000)
            throw new APIException("Max weight that a package can take more than 100");
        
        if (package.Items.Count > 1500)
            throw new APIException("There are more than 15 items");
        
        if (package.Items.Max(i => i.Weight) > 10000 || package.Items.Max(i => i.Cost) > 10000)
            throw new APIException("Max weight or cost of an item more than 100");
    }
}