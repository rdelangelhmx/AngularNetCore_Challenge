using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Interfaces;

namespace Server.Controller;

[Route("[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomersRepository customersRepository;

    public CustomersController(ICustomersRepository _customersRepository)
    {
        customersRepository = _customersRepository;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(customersRepository.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(customersRepository.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add(TblCustomers customer)
    {
        try
        {
            return Ok(customersRepository.Put(customer, null));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update(TblCustomers customer)
    {
        try
        {
            return Ok(customersRepository.Put(customer, customer.CustomerId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            return Ok(customersRepository.DeleteById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}