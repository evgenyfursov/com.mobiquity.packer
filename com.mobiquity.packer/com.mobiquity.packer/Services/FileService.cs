using System.Text.RegularExpressions;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Extensions;
using com.mobiquity.packer.Interfaces;
using com.mobiquity.packer.Models;

namespace com.mobiquity.packer.Services;

public class FileService : IFileService
{
    public List<Package> ParseFile(string filePath)
    {
        try
        {
            var result = new List<Package>();

            var reader = File.OpenText(filePath);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var pkg = new Package();
                var items = line.Split(' ');
                pkg.Weight = items[0].ToInt();

                foreach (var item in items.Skip(2))
                {
                    var matches = new Regex(@"\d+(\.\d+)?").Matches(item);
                    pkg.Items.Add(new PackageItem
                    {
                        Index = int.Parse(matches[0].Value), 
                        Weight = matches[1].Value.ToInt(), 
                        Cost = matches[2].Value.ToInt()
                    });
                }

                result.Add(pkg);
            }
            
            return result;
        }
        catch (Exception e)
        {
            throw new APIException(e.Message);
        }
    }
}