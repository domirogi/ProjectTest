using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Model.Common;
using Project.Model.Common.DTOs;
using Project.Model.Models;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly IMakeService _makeService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MakeController(IMakeService makeService, ILoggerManager logger, IMapper mapper)
        {
            _makeService = makeService;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMake([FromQuery]RequestParams requestParams)
        {
            try
            {
                var make = await _makeService.GetAllMake(requestParams);
                var makeDto = _mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<MakeDTO>>(make);
                return Ok(makeDto);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllMake)}action{ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMakeById(int id)
        {
            var make = await _makeService.GetMakeById(id);
            if (make == null)
            {
                _logger.LogInfo($"Make with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var makeDto = _mapper.Map<MakeDTO>(make);
                return Ok(makeDto);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateMake([FromBody] SaveMakeDTO saveMakeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateMake)}");
                return BadRequest(ModelState);
            }

            var make = _mapper.Map<VehicleMake>(saveMakeDTO);

            var newMake = await _makeService.CreateMake(make);

            var makeDto = _mapper.Map<MakeDTO>(newMake);

            return Ok(makeDto);

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] SaveMakeDTO updateMakeDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMake)}");
                return BadRequest(ModelState);
            }
            var make = await _makeService.GetMakeById(id);

            if (make == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMake)}");
                return NotFound();
            }
            _mapper.Map(updateMakeDTO, make);

            await _makeService.UpdateMake(id, make);
            return Ok(updateMakeDTO);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMake)}");
                return BadRequest();
            }

            var make = await _makeService.GetMakeById(id);
            if (make == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMake)}");
                return BadRequest("Submitted data is invalid");
            }

            await _makeService.DeleteMake(make);


            return NoContent();

        }


    }
}
