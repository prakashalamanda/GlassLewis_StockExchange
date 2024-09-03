using GlassLewis_StockExchange.Data;
using GlassLewis_StockExchange.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlassLewis_StockExchange.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CompanyController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet, Route("GetCompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection()
    {
        var companies = await _context.Company.ToListAsync();

        return Ok(companies);
    }

    [HttpGet("GetCompanyById/{id}")]
    public async Task<ActionResult<Company>> GetCompanyById(int id)
    {
        var company = await _context.Company.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        return company;
    }

    [HttpGet("GetCompanyByIsin/{isin}")]
    public async Task<ActionResult<Company>> GetCompanyByIsin(string isin)
    {
        var company = await _context.Company.FirstOrDefaultAsync(c => c.Isin == isin);

        if (company == null)
        {
            return NotFound();
        }

        return company;
    }

    [HttpPost, Route("CreateCompany")]
    public async Task<ActionResult<Company>> CreateCompany([FromBody] Company company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else if (_context.Company.Any(c => c.Isin == company.Isin))
        {
            return Conflict(new { message = MessageConstants.DuplicateIsin });
        }

        _context.Company.Add(company);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
    }

    [HttpPut, Route("UpdateCompany")]
    public async Task<IActionResult> UpdateCompany(Company company)
    {
        if (_context.Company.All(e => e.Id != company.Id))
        {
            return NotFound();
        }
        else if (_context.Company.Any(c => c.Isin == company.Isin && c.Id != company.Id))
        {
            return Conflict(new { message = MessageConstants.DuplicateIsin });
        }

        _context.Entry(company).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}
