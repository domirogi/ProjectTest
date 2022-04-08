using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class VehicleModelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleService _vehicleService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public VehicleModelsController(IUnitOfWork unitOfWork, IVehicleService vehicleService, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels([FromQuery] RequestParams requestParams)
        {
            try
            {
                var model = await _vehicleService.GetPagedModel(requestParams);
                var modelDTO = _mapper.Map<IEnumerable<ModelDTO>>(model);
                return Ok(modelDTO);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllModels)}action{ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id:int}", Name = "GetModel")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model = await _unitOfWork.Model.Get(q => q.Id == id, q => q.Include(x => x.Make));
            var result = _mapper.Map<ModelDTO>(model);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] ModelDTO model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateModel)}");
                return BadRequest(ModelState);
            }

            var modelEntity = _mapper.Map<VehicleModel>(model);
            await _unitOfWork.Model.CreateAsync(modelEntity);
            await _unitOfWork.SaveAsync();

            var makeToReturn = _mapper.Map<ModelDTO>(modelEntity);

            return CreatedAtRoute("GetModel", new { id = makeToReturn.Id }, model);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelDTO modelUpdate)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateModel)}");
                return BadRequest(ModelState);
            }


            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateModel)}");
                return NotFound();
            }

            _mapper.Map(modelUpdate, model);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteModel)}");
                return BadRequest();
            }

            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteModel)}");
                return BadRequest("Submitted data is invalid");
            }

            await _vehicleService.DeleteModelAsync(model);
            await _unitOfWork.SaveAsync();

            return NoContent();

        }

    }
}
