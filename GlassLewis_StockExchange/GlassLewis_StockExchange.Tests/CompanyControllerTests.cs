using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GlassLewis_StockExchange.Controllers;
using GlassLewis_StockExchange.Models;
using GlassLewis_StockExchange.Data;

public class CompanyControllerTests
{
    /// <summary>
    /// This test method checks the positive case of GetCompanyCollection method in Company Controller class.
    /// URL: https://localhost:7068/api/Company/GetCompanyCollection
    /// </summary>
    /// <returns>Company collection </returns>
    [Fact]
    public async Task GetCompanyCollection_ShouldReturnCompanyCollection()
    {
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company1 = new Company
        {
            Name = "Test Company 1",
            Ticker = "TEST1",
            Exchange = "Test Exchange 1",
            Isin = "US1234567891"
        };
        var company2 = new Company
        {
            Name = "Test Company 2",
            Ticker = "TEST2",
            Exchange = "Test Exchange 2",
            Isin = "US1234567892"
        };
        context.Company.AddRange(company1, company2);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.GetCompanyCollection() as OkObjectResult;

        Assert.Equal(2, (result!.Value as List<Company>)!.Count);
    }

    /// <summary>
    /// This test method checks the positive case of GetCompanyById method in Company Controller class.
    /// URL: https://localhost:7068/api/Company/GetCompanyById/1
    /// </summary>
    /// <returns>Company record</returns>
    [Fact]
    public async Task GetCompanyById_WithValidId_ShouldReturnCompanyRecord()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company = new Company
        {
            Name = "Test Company",
            Ticker = "TEST",
            Exchange = "Test Exchange",
            Isin = "US1234567890",
            WebsiteUrl = "http://test.com"
        };
        context.Company.Add(company);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.GetCompanyById(company.Id);

        Assert.NotNull(result);
        Assert.IsType<Company>(result.Value);
        Assert.Equal(company.Name, result.Value.Name);
    }

    /// <summary>
    /// This test method checks the negative case of GetCompanyById method in Company Controller class.
    /// URL: https://localhost:7068/api/Company/GetCompanyById/999
    /// </summary>
    /// <returns>NotFoundResult</returns>
    [Fact]
    public async Task GetCompanyById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);

        // Act
        var result = await controller.GetCompanyById(999);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// This test method checks the positive case of GetCompanyByIsin method in Company Controller class.
    /// URL: https://localhost:7068/api/Company/GetCompanyByIsin/US1234567890
    /// </summary>
    /// <returns>NotFoundResult</returns>
    [Fact]
    public async Task GetCompanyByIsin_WithValidIsin_ShouldReturnCompanyRecord()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company = new Company
        {
            Name = "Test Company",
            Ticker = "TEST",
            Exchange = "Test Exchange",
            Isin = "US1234567890",
            WebsiteUrl = "http://test.com"
        };
        context.Company.Add(company);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.GetCompanyByIsin("US1234567890");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result.Value);
    }

    /// <summary>
    /// This test method checks the negative case of GetCompanyByIsin method in Company Controller class.
    /// URL: https://localhost:7068/api/Company/GetCompanyByIsin/US9999999999
    /// </summary>
    /// <returns>NotFoundResult</returns>
    [Fact]
    public async Task GetCompanyByIsin_WithNonExistentIsin_ShouldReturnNotFound()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);

        // Act
        var result = await controller.GetCompanyByIsin("US9999999999");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// This test method checks the positive case of CreateCompany method in Company Controller class.
    /// URL: https://localhost:7068/api/Company/CreateCompany (POST)
    /// </summary>
    /// <returns>Created company object</returns>
    [Fact]
    public async void CreateCompany_WithValidRequestBody_ShouldReturnCreatedCompany()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company = new Company
        {
            Name = "Test Company",
            Ticker = "TEST",
            Exchange = "Test Exchange",
            Isin = "US1234567890",
            WebsiteUrl = "http://test.com"
        };

        // Act
        var result = await controller.CreateCompany(company);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Company>>(result);
        var createdCompany = Assert.IsType<Company>((actionResult.Result as CreatedAtActionResult)!.Value);

        Assert.Equal(company.Name, createdCompany.Name);
        Assert.Equal(company.Isin, createdCompany.Isin);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.Equal("GetCompanyById", createdAtActionResult.ActionName);
    }

    /// <summary>
    /// This test method checks the negative case of CreateCompany method in Company Controller class for Duplicate ISIN.
    /// URL: https://localhost:7068/api/Company/CreateCompany (POST)
    /// </summary>
    /// <returns>ConflictObjectResult</returns>
    [Fact]
    public async Task CreateCompany_WithDuplicateIsin_ShouldReturnConflict()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company1 = new Company
        {
            Name = "Company 1",
            Ticker = "COMP1",
            Exchange = "Exchange 1",
            Isin = "US1234567890"
        };
        var company2 = new Company
        {
            Name = "Company 2",
            Ticker = "COMP2",
            Exchange = "Exchange 2",
            Isin = "US1234567890"
        };
        context.Company.Add(company1);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.CreateCompany(company2);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Company>>(result);
        var conflictResultResponse = Assert.IsType<ConflictObjectResult>(actionResult.Result);
        Assert.Contains(MessageConstants.DuplicateIsin, conflictResultResponse.Value!.ToString());
    }

    /// <summary>
    /// This test method checks the negative case of CreateCompany method in Company Controller class for Invalid ISIN.
    /// URL: https://localhost:7068/api/Company/CreateCompany (POST)
    /// </summary>
    /// <returns>BadRequestObjectResult</returns>
    [Fact]
    public async Task CreateCompany_WithInvalidIsin_ShouldReturnBadRequest()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company = new Company
        {
            Name = "Invalid ISIN Company",
            Ticker = "INVALID",
            Exchange = "Invalid Exchange",
            Isin = "1234567890"  // Invalid ISIN
        };

        // Manually set ModelState error
        controller.ModelState.AddModelError("Isin", MessageConstants.InvalidIsinFormat);

        // Act
        var result = await controller.CreateCompany(company);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Company>>(result);
        var badRequestResponse = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(400, badRequestResponse.StatusCode);
    }

    /// <summary>
    /// This test method checks the positive case of UpdateCompany method in Company Controller class for Invalid ISIN.
    /// URL: https://localhost:7068/api/Company/UpdateCompany (PUT)
    /// </summary>
    /// <returns>NoContentResult</returns>
    [Fact]
    public async Task UpdateCompany_WithValidId_ShouldUpdateCompany()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company = new Company
        {
            Name = "Invalid ISIN Company",
            Ticker = "INVALID",
            Exchange = "Invalid Exchange",
            Isin = "1234567890"  // Invalid ISIN
        };

        context.Company.Add(company);
        await context.SaveChangesAsync();

        // Act
        company.Name = "Updated Company";
        var result = await controller.UpdateCompany(company) as NoContentResult;

        // Assert
        Assert.Equal(204, result!.StatusCode);
    }

    /// <summary>
    /// This test method checks the negative case of UpdateCompany method in Company Controller class for Invalid ISIN.
    /// URL: https://localhost:7068/api/Company/UpdateCompany (PUT)
    /// </summary>
    /// <returns>NotFoundResult</returns>
    [Fact]
    public async Task UpdateCompany_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        using var context = CreateContext();
        var controller = new CompanyController(context);
        var company = new Company
        {
            Name = "Non-existent Company",
            Ticker = "NONEX",
            Exchange = "Non-existent Exchange",
            Isin = "US0000000000"
        };

        // Act
        var result = await controller.UpdateCompany(company) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    private static DataContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: $"GL_StockExchange_{Guid.NewGuid()}")
            .Options;

        return new DataContext(options);
    }
}
