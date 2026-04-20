using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatesController : ControllerBase
{
    private readonly DataContext _context;

    public StatesController(DataContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<State>>> GetListAsync()
    {
        try
        {
            var listItem = await _context.States.OrderBy(x => x.Name).ToListAsync();
            return Ok(listItem);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<State>> GetItemAsync(int id)
    {
        try
        {

            var itemModel = await _context.States.FindAsync(id);

            return Ok(itemModel);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] State modelo)
    {
        try
        {
            _context.States.Add(modelo);
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
    public async Task<ActionResult<State>> PutAsync(State modelo)
    {
        try
        {
            var UpdateModelo = await _context.States.FirstOrDefaultAsync(x => x.StateId == modelo.StateId);
            UpdateModelo!.Name = modelo.Name;
            _context.States.Update(UpdateModelo);
            await _context.SaveChangesAsync();

            return Ok(UpdateModelo);
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var DataRemove = await _context.States.FindAsync(id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontro el registro para eliminar");
            }
            _context.States.Remove(DataRemove);
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
