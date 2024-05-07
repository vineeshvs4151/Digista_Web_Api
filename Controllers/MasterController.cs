using Digista_Web_Api.DTOModels;
using Digista_Web_Api.IRepositories;
using Digista_Web_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Digista_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterRepository _masterRepository;
      
        public MasterController(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
          
        }


        [HttpPost("insert_state")]
        public async Task<IActionResult> InsertStateAsync(string state)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(state))
                {
                    return BadRequest("State cannot be empty or null.");
                }

                var lastInsertedId = await _masterRepository.InsertStateAsync(state);

                if (lastInsertedId == 0)
                {
                   
                    return StatusCode(500, "Failed to insert state.");
                }

              
                return Ok( "State inserted successfully");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while inserting state.");
            }
        }

        [HttpPost("insert_district")]
        public async Task<IActionResult> InsertDistrictAsync(DistrictDTOModel districtDTOModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid input values.");
                }

                var lastInsertedId = await _masterRepository.InsertDistrictAsync(districtDTOModel);
                if (lastInsertedId == 0)
                {

                    return StatusCode(500, "Failed to insert district.");
                }

                
                return Ok("District inserted successfully");
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while inserting district.");
            }
        }



        [HttpGet("list_states")]
        public async Task<IActionResult> ListStatesAsync()
        {
            try
            {
                var states = await _masterRepository.ListStatesAsync();
                if (states == null || !states.Any())
                {
                  
                    return NotFound("No states found.");
                }
                return Ok(states);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while fetching states.");
            }
        }


        [HttpGet("get_districts_by_state_id")]
        public async Task<IActionResult> GetDistrictByStateIdAsync(int stateId)
        {
            try
            {
                if (stateId <= 0) 
                {
                    return BadRequest("State ID must be a positive integer.");
                }
                var districts = await _masterRepository.GetDistrictByStateIdAsync(stateId);
                if (districts == null || !districts.Any())
                {
                   
                    return NotFound("No districts found.");
                }
                return Ok(districts);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while fetching districts.");
            }
        }

    }
}
