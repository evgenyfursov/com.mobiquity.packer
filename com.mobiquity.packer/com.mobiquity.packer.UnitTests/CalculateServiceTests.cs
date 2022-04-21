using System.Collections.Generic;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Interfaces;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using FluentAssertions;
using Xunit;

namespace com.mobiquity.packer.UnitTests;

public class CalculateServiceTests
{
    private readonly ICalculateService _service = new CalculateService();
    
    [Fact]
    public void FindKnapsackItems_WhenValidInput_ReturnStringWithItems()
    {
        // Arrange
        var package = new Package
        {
            Weight = 5,
            Items = new List<PackageItem>
            {
                new() {Index = 1, Weight = 2, Cost = 4},
                new() {Index = 2, Weight = 3, Cost = 5},
                new() {Index = 3, Weight = 1, Cost = 3},
                new() {Index = 4, Weight = 4, Cost = 7}
            }
        };
        
        // Act
        var actual = _service.FindKnapsackItems(package);

        // Assert
        actual.Should().Be("3,4");
    }
    
    [Fact]
    public void FindKnapsackItems_WhenValidInput_ReturnEmptyString()
    {
        // Arrange
        var package = new Package
        {
            Weight = 5,
            Items = new List<PackageItem>
            {
                new() {Index = 1, Weight = 12, Cost = 4},
                new() {Index = 2, Weight = 13, Cost = 5},
            }
        };
        
        // Act
        var actual = _service.FindKnapsackItems(package);

        // Assert
        actual.Should().Be("-");
    }
    
    [Fact]
    public void FindKnapsackItems_WhenWeightMoreThan100_ThrowAPIException()
    {
        // Arrange
        var package = new Package
        {
            Weight = 10100,
            Items = new List<PackageItem> { new() {Index = 1, Weight = 2, Cost = 4} }
        };
        
        // Act
        var act = () => _service.FindKnapsackItems(package);
    
        // Assert
        act.Should().Throw<APIException>().WithMessage("Max weight that a package can take more than 100");
    }
    
    [Fact]
    public void FindKnapsackItems_WhenMaxItemCostMoreThan100_ThrowAPIException()
    {
        // Arrange
        var package = new Package
        {
            Weight = 10,
            Items = new List<PackageItem> { new() {Index = 1, Weight = 2, Cost = 10100} }
        };
        
        // Act
        var act = () => _service.FindKnapsackItems(package);
    
        // Assert
        act.Should().Throw<APIException>().WithMessage("Max weight or cost of an item more than 100");
    }
    
    [Fact]
    public void FindKnapsackItems_WhenMaxItemCountMoreThan15_ThrowAPIException()
    {
        // Arrange
        var package = new Package { Weight = 10 };
        for(var i = 1; i < 1600; i++)
            package.Items.Add(new PackageItem { Index = i, Cost = i*2, Weight = i*3 });
        
        // Act
        var act = () => _service.FindKnapsackItems(package);
    
        // Assert
        act.Should().Throw<APIException>().WithMessage("There are more than 15 items");
    }
}