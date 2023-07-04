using Microsoft.EntityFrameworkCore;
using Moq;
using Smartwyre.Data.Context;
using Smartwyre.Data.DTO;
using Smartwyre.Data.Enum;
using Smartwyre.Data.Models;
using Smartwyre.Data.Repositories.Definitions;
using Smartwyre.Data.Repositories.Implementations;
using Smartwyre.DeveloperTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void Calculate_WithValidRequest_ReturnsResult()
    {
        // Arrange
        var productIdentifier = "product1";
        var rebateIdentifier = "rebate1"; 
        var options = new DbContextOptionsBuilder<SmartwyreContext>()
            .UseInMemoryDatabase(databaseName: "SmartwyreDb")
            .Options;
        var contextMock = new Mock<SmartwyreContext>(options) { CallBase = true };
        var mockedProducts = new List<Product> { new Product { Identifier = productIdentifier, Price = 100, Uom = string.Empty, SupportedIncentives = SupportedIncentiveType.FixedCashAmount } };
        var mockedRebates = new List<Rebate> { new Rebate { Identifier = rebateIdentifier, Incentive = IncentiveType.FixedCashAmount, Amount = 10, Percentage = 5 } };
       
        contextMock.Setup(c => c.Products)
                .Returns(CreateMockedDbSet(mockedProducts));
        contextMock.Setup(c => c.Rebates)
            .Returns(CreateMockedDbSet(mockedRebates));

        var dataRepositoryMock = new DataRepository(contextMock.Object);
        var rebateService = new RebateService(dataRepositoryMock);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier
        };
        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.True(result.Success);
    }
    [Fact]
    public void Calculate_WithInvalidRequest_ReturnsResult()
    {
        // Arrange
        var productIdentifier = "product1";
        var rebateIdentifier = "rebate1";
        var options = new DbContextOptions<SmartwyreContext>();
        var contextMock = new Mock<SmartwyreContext>(options) { CallBase = true };
        var mockedProducts = new List<Product> { new Product { Identifier = productIdentifier } };
        var mockedRebates = new List<Rebate> { new Rebate { Identifier = rebateIdentifier } };

        contextMock.Setup(c => c.Products)
            .Returns(CreateMockedDbSet(mockedProducts));
        contextMock.Setup(c => c.Rebates)
            .Returns(CreateMockedDbSet(mockedRebates));

        var dataRepositoryMock = new DataRepository(contextMock.Object);
        var rebateService = new RebateService(dataRepositoryMock);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier
        };
        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
        // Add additional assertions for the expected result values
    }
    [Fact]
    public void GetProduct_ReturnsProduct()
    {
        // Arrange
        var productIdentifier = "product1";
        var options = new DbContextOptions<SmartwyreContext>();
        var contextMock = new Mock<SmartwyreContext>(options) { CallBase = true };
        var mockedProducts = new List<Product> { new Product { Identifier = productIdentifier } };

        contextMock.Setup(c => c.Products)
            .Returns(CreateMockedDbSet(mockedProducts));

        var dataRepository = new DataRepository(contextMock.Object);

        var expectedProduct = new Product { Identifier = productIdentifier };

        // Act
        var result = dataRepository.GetProduct(productIdentifier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Identifier, result.Identifier);
    }
    [Fact]
    public void GetRebate_ReturnsRebate()
    {
        // Arrange
        var rebateIdentifier = "rebate1";
        var options = new DbContextOptions<SmartwyreContext>();
        var contextMock = new Mock<SmartwyreContext>(options) { CallBase = true };
        var mockedRebates = new List<Rebate> { new Rebate { Identifier = rebateIdentifier } };

        contextMock.Setup(c => c.Rebates)
            .Returns(CreateMockedDbSet(mockedRebates));

        var dataRepository = new DataRepository(contextMock.Object);

        var expectedRebate = new Rebate { Identifier = rebateIdentifier };

        // Act
        var result = dataRepository.GetRebate(rebateIdentifier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedRebate.Identifier, result.Identifier);
    }
    [Fact]
    public void DbSet_ReturnsMockedDbSet()
    {
        // Arrange
        var options = new DbContextOptions<SmartwyreContext>();
        var contextMock = new Mock<SmartwyreContext>(options) { CallBase = true };
        var mockedProducts = new List<Product> { };
        var mockedRebates = new List<Rebate> { };
        var mockedRebateCalculations = new List<RebateCalculation> { };

        contextMock.Setup(c => c.Products)
            .Returns(CreateMockedDbSet(mockedProducts));
        contextMock.Setup(c => c.Rebates)
            .Returns(CreateMockedDbSet(mockedRebates));
        contextMock.Setup(c => c.RebateCalculations)
            .Returns(CreateMockedDbSet(mockedRebateCalculations));

        // Act
        var resultProducts = contextMock.Object.Products.ToList();
        var resultRebates = contextMock.Object.Rebates.ToList();
        var resultRebateCalculations = contextMock.Object.RebateCalculations.ToList();

        // Assert
        Assert.Equal(mockedProducts, resultProducts);
        Assert.Equal(mockedRebates, resultRebates);
        Assert.Equal(mockedRebateCalculations, resultRebateCalculations);
    }
    private static DbSet<T> CreateMockedDbSet<T>(List<T> data) where T : class
    {
        var queryableData = data.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

        return dbSetMock.Object;
    }
}
