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
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IMakeService _makeService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ModelController(IModelService modelService, IMakeService makeService, IMapper mapper, ILoggerManager logger)
        {
            _modelService = modelService;
            _makeService = makeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels([FromQuery] RequestParams requestParams)
        {
            try
            {
                var model = await _modelService.GetAllWithMake(requestParams);
                var modelDto = _mapper.Map<IEnumerable<IVehicleModel>, IEnumerable<ModelDTO>>(model);
                return Ok(modelDto);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllModels)}action{ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var model = await _modelService.GetModelById(id);
            if (model == null)
            {
                return NotFound();
            }
            var modelDto = _mapper.Map<IVehicleModel, ModelDTO>(model);
            return Ok(modelDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] SaveModelDTO modelSave)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateModel)}");
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<SaveModelDTO, VehicleModel>(modelSave);
            var newModel = await _modelService.CreateModel(model);
            return Ok(newModel);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] SaveModelDTO updateModelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateModel)}");
                return BadRequest(ModelState);
            }
            var model = await _modelService.GetModelById(id);

            if (model == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateModel)}");
                return NotFound();
            }
            _mapper.Map(updateModelDTO, model);

            await _modelService.UpdateModel(id, model);
            return Ok(updateModelDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteModel)}");
                return BadRequest();
            }

            var model = await _modelService.GetModelById(id);
            if (model == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteModel)}");
                return BadRequest("Submitted data is invalid");
            }

            await _modelService.DeleteModel(model);
         

            return NoContent();

        }

        [HttpGet("VehicleMake/id")]
        public async Task<IActionResult> GetAllModelByMaketID(int id)
        {
            var make = await _makeService.GetMakeById(id);
            if (make == null)
            {
                _logger.LogError($"NotFound {nameof(GetAllModelByMaketID)}");
                return NotFound();
            }
            var model = await _modelService.GetModelByMaketId(id);
            var modelDto = _mapper.Map<IEnumerable<IVehicleModel>, IEnumerable<ModelDTO>>(model);
            return Ok(modelDto);

        }

    }
}
