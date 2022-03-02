using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Common;
using Project.Model.Common.DTOs;
using Project.Model.Models;
using Project.Repository.Common.IRepository;
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
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public VehicleMakesController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMake()
        {
            try
            {
                var make = await _unitOfWork.Make.GetAllMakesAsync(trackChanges: false);
                var makeDTO = _mapper.Map<IEnumerable<MakeDTO>>(make);
                return Ok(makeDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllMake)}action{ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id:int}", Name = "GetAllMakes")]
        public async Task<IActionResult> GetByIdMake(int id)
        {
            var make = await _unitOfWork.Make.GetMakeAsync(id, trackChanges: false);
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
            _unitOfWork.Make.CreateMake(makeEntity);
            await _unitOfWork.SaveAsync();

            var makeToReturn = _mapper.Map<MakeDTO>(makeEntity);

            return CreatedAtRoute("GetAllMakes", new { id = makeToReturn.Id }, make);

        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] MakeDTO makeUpdate)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMake)}");
                return BadRequest(ModelState);
            }


            var make =  await _unitOfWork.Make.GetMakeAsync(id, trackChanges: true);
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

            var make = await _unitOfWork.Make.GetMakeAsync(id, trackChanges: false);
            if (make == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMake)}");
                return BadRequest("Submitted data is invalid");
            }

            _unitOfWork.Make.DeleteMake(make);
            await _unitOfWork.SaveAsync();

            return NoContent();

        }

        
    }
}
