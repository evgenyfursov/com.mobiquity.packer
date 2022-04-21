using com.mobiquity.packer.Exceptions;
using FluentAssertions;
using Xunit;

namespace com.mobiquity.packer.UnitTests;

public class PackerTests
{
    [Fact]
    public void Pack_WhenFileExists_ReturnStringWithItems()
    {
        // Arrange
        var expected = @"4
-
2,7
8,9";
        
        // Act
        var actual = Packer.Pack("example_input");

        // Assert
        actual.Should().Be(expected);
    }
    
    [Fact]
    public void Pack_WhenFileNotExists_ThrowAPIException()
    {
        // Arrange
        var fileName = "invalid";
        
        // Act
        var act = () => Packer.Pack(fileName);

        // Assert
        act.Should().Throw<APIException>()
            .Where(e => e.Message.StartsWith("Could not find file"));
    }
}