using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class VehicleModelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public VehicleModelsController(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var model = await _unitOfWork.Model.GetAllAsync(trackChanges: false);
                var modelDTO = _mapper.Map<IEnumerable<ModelDTO>>(model);
                return Ok(modelDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAll)}action{ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int makeId, int id)
        {

            var make = await _unitOfWork.Make.GetMakeAsync(makeId, trackChanges: false);
            if (make == null)
            {
                _logger.LogInfo($"Make with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var modelDb = await _unitOfWork.Model.GetModelAsync(makeId, id, trackChanges: false);
            if (modelDb == null)
            {
                _logger.LogInfo($"Model with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var model = _mapper.Map<ModelDTO>(modelDb);
            return Ok(model);

        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(int makeId, [FromBody] ModelDTO model)
        {
            if (model == null)
            {
                _logger.LogError("CreateModel object sent from client is null.");
                return BadRequest("CreateModel object is null");
            }

            var make = await _unitOfWork.Make.GetMakeAsync(makeId, trackChanges: false);
            if (make == null)
            {
                _logger.LogInfo($"Make with id: {makeId} doesn't exist in the database.");
                return NotFound();
            }

            var modelEntity = _mapper.Map<VehicleModel>(model);
            _unitOfWork.Model.CreateModel(makeId, modelEntity);
            await _unitOfWork.SaveAsync();
            var modelToReturn = _mapper.Map<ModelDTO>(modelEntity);

            return CreatedAtAction("CreateModel", new { makeId, id = modelToReturn.Id }, modelToReturn);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int makeId, int id)
        {
            var model = await _unitOfWork.Make.GetMakeAsync(makeId, trackChanges: false);
            if (model == null)
            {
                _logger.LogInfo($"Make with id: {makeId} doesn't exist in the database.");
                return NotFound();
            }
            var modelForMake = await _unitOfWork.Model.GetModelAsync(makeId, id, trackChanges: false);
            if (modelForMake == null)
            {
                _logger.LogInfo($"Model with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _unitOfWork.Model.DeleteModel(modelForMake);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int makeId, int id, [FromBody] ModelDTO model)
        {
            if (model == null)
            {
                _logger.LogError("Update object sent from client is null.");
                return BadRequest("UpdateModel object is null");
            }
            var make = await _unitOfWork.Make.GetMakeAsync(makeId, trackChanges: true);
            if (make == null)
            {
                _logger.LogInfo($"Make with id: {makeId} doesn't exist in the database.");
                return NotFound();
            }
            var modelEntity = await _unitOfWork.Model.GetModelAsync(makeId, id, trackChanges: false);
            if (modelEntity == null)
            {
                _logger.LogInfo($"Model with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(model, modelEntity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }


       
    }
}
