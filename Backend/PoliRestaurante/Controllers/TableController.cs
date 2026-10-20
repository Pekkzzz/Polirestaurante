using Microsoft.AspNetCore.Mvc;
using PoliRestaurante.Models;

namespace ApiEcommerce.Controllers
{
  [Route("api/[controller]")] // EVAN: Requeriment RF-11
  [ApiController]
  public class TableController : ControllerBase
  {
    private readonly ITableService _service;

    public TableController(ITableService service)
    {
      _service = service;
    }

    [HttpGet("{id:int}", Name = "GetTableByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetByID(int id)
    {
      TableService_Response serviceResponse = _service.GetByID(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      if (serviceResponse.Table == null)
      {
        return StatusCode(500);
      }
      
      var Table = TablesMapper.TableToDto(serviceResponse.Table);
      return Ok(Table);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateTable([FromBody] Table_CreateDto createTableDto)
    {
      if (createTableDto == null)
      {
        return BadRequest(ModelState);
      }
      TableService_Response serviceResponse = _service.Create(createTableDto);

      if (!serviceResponse.Response || serviceResponse.Table == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      Table_Dto table_Dto = TablesMapper.TableToDto(serviceResponse.Table);
      return CreatedAtRoute("GetTableByID", new { id = table_Dto.Id }, table_Dto);
    }

    [HttpPatch(Name = "UpdateTable")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateTable([FromBody] Table_UpdateDto updateTableDto)
    {
      if (updateTableDto == null)
      {
        return BadRequest(ModelState);
      }
      TableService_Response serviceResponse = _service.Update(updateTableDto);

      if (!serviceResponse.Response || serviceResponse.Table == null)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }

      Table_Dto table_Dto = TablesMapper.TableToDto(serviceResponse.Table);
      return Ok(table_Dto);
    }

    [HttpDelete("{id:int}", Name = "DeleteTableByID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteTable(int id)
    {
      TableService_Response serviceResponse = _service.Delete(id);
      if (!serviceResponse.Response)
      {
        ModelState.AddModelError("CustomError", serviceResponse.Description);
        return BadRequest(ModelState);
      }
      return Ok();
    }



    
  }
}