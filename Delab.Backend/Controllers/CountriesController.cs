using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        try
        {
            var listCountry = await _context.Countries.Include(x => x.States)!.ThenInclude(x => x.Cities).OrderBy(x => x.Name).ToListAsync();
            return Ok(listCountry);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        try
        {
            // var idCountry = await _context.Countries.Where(x => x.CountryId == id).FirstOrDefaultAsync();

            var idCountry = await _context.Countries.FindAsync(id);

            //var IdCountry2 = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

            return Ok(idCountry);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostCountry([FromBody] Country modelo)
    {
        try
        {
            _context.Countries.Add(modelo);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe un registro con el mismo nombre.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Country>> PutCountry(Country modelo)
    {
        try
        {
            var UpdateCountry = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == modelo.CountryId);
            UpdateCountry!.Name = modelo.Name;
            UpdateCountry.CodPhone = modelo.CodPhone;
            _context.Countries.Update(UpdateCountry);
            await _context.SaveChangesAsync();

            return Ok(UpdateCountry);
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe un registro con el mismo nombre.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            var DataRemove = await _context.Countries.FindAsync(id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontro el registro para eliminar");
            }
            _context.Countries.Remove(DataRemove);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("REFERENCE"))
            {
                return BadRequest("No se puede eliminar el registro porque tiene datos relacionados");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}
