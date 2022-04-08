using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Model.Common;
using Project.Model.Common.DTOs;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
using Project.Service.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleService _vehicleService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public VehicleMakesController(IUnitOfWork unitOfWork, IVehicleService vehicleService, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _vehicleService = vehicleService;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMakes([FromQuery] RequestParams requestParams)
        {
            try
            {
                var make = await _vehicleService.GetPagedMake(requestParams);
                var makeDTO = _mapper.Map<IEnumerable<MakeDTO>>(make);
                return Ok(makeDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllMakes)}action{ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}", Name = "GetMake")]
        public async Task<IActionResult> GetMake(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
            {
                _logger.LogInfo($"Make with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var makeDTO = _mapper.Map<MakeDTO>(make);
                return Ok(makeDTO);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateMake([FromBody] MakeDTO make)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateMake)}");
                return BadRequest(ModelState);
            }

            var makeEntity = _mapper.Map<VehicleMake>(make);
            await _unitOfWork.Make.CreateAsync(makeEntity);
            await _unitOfWork.SaveAsync();

            var makeToReturn = _mapper.Map<MakeDTO>(makeEntity);

            return CreatedAtRoute("GetMake", new { id = makeToReturn.Id }, make);

        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] MakeDTO makeUpdate)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMake)}");
                return BadRequest(ModelState);
            }


            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMake)}");
                return NotFound();
            }

            _mapper.Map(makeUpdate, make);

            await _unitOfWork.SaveAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMake)}");
                return BadRequest();
            }

            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMake)}");
                return BadRequest("Submitted data is invalid");
            }

           await _vehicleService.DeleteMakeAsync(make);
            await _unitOfWork.SaveAsync();

            return NoContent();

        }


    }
}
