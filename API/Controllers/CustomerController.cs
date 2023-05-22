using Business;
using Business.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CustomerEntity = DataAccess.Data.Customer;

namespace API.Controllers.Customer
{
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        protected ICustomerService<CustomerEntity> _customerService;

        public CustomerController(ICustomerService<CustomerEntity> CustomerRepository)
        {
            _customerService = CustomerRepository;
        }


        [HttpGet()]
        public IQueryable<CustomerEntity> GetAll()
        {
            return _customerService.GetAll();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerEntity entity)
        {
            try
            {
                var createdEntity = _customerService.Create(entity.Name, entity);
                if (createdEntity == null)
                {
                    return BadRequest("Ya existe un customer con el mismo nombre.");
                }
                return Ok(createdEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear el customer: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] CustomerEntity entity)
        {
            try
            {
                bool changed;
                var updatedEntity = _customerService.Update(entity.CustomerId, entity, out changed);
                if (changed)
                {
                    return Ok(updatedEntity);
                }
                else
                {
                    return BadRequest("No se encontró el customer a actualizar.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar la entidad: " + ex.Message);
            }
        }


        [HttpDelete()]
        public IActionResult Delete([FromBodyAttribute] CustomerEntity entity)
        {

            try
            {
                var delete = _customerService.Delete(entity);
                if (delete != null)
                {
                    return Ok(delete);
                }
                else
                {
                    return BadRequest("No se encontró el customer a eliminar.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar : " + ex.Message);
            }
        }
    }
}
