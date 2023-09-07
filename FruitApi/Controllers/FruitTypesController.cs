using FruitApi.BusinessLogic;
using FruitApi.DataAccess.Models;
using FruitApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FruitTypesController : ControllerBase
    {
        private readonly IBLFruitType _blFruitType;

        public FruitTypesController(IBLFruitType blFruitType)
        {
            _blFruitType = blFruitType;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitTypeVM>>> FindAllFruitTypesAsync()
        {
            return Ok(await _blFruitType.FindAllFruitTypesAsync());
        }
    }
}
