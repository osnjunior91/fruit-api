using FruitApi.BusinessLogic;
using FruitApi.DataAccess.Models;
using FruitApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FruitApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FruitsController : ControllerBase
    {
        private readonly IBLFruit _blFruit;

        public FruitsController(IBLFruit blFruit)
        {
            _blFruit = blFruit;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindAllFruits() => Ok(await _blFruit.FindAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<FruitDTO>> FindFruitById(int id) => Ok(await _blFruit.GetByIdAync(id));

        [HttpPost]
        public async Task<ActionResult<long>> SaveFruitAsync(FruitDTO fruit)
        {
            var id = await _blFruit.CreateAsync(fruit);
            return StatusCode((int)HttpStatusCode.Created, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFruit(int id, FruitDTO updatedFruit)
        {
            await _blFruit.EditAync(id, updatedFruit);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFruit(int id)
        {
            await _blFruit.DeleteAync(id);
            return Ok(); 
        }
    }
}
