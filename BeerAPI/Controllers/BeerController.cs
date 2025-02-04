using BeerAPI.Interfaces;
using BeerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BeerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerRepository _repository;

        public BeerController(IBeerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all beers from database
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Beers fetched successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult GetAllBeers()
        {
            try
            {
                var result = _repository.GetBeers();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in action: GetAllBeers. Details: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Filters beers from database
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <response code="200">Beers filtered successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("filter")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult FilterBeers([FromQuery] string filter)
        {
            try
            {
                var result = _repository.FilterBeers(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in action: FilterBeers. Details: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new beer
        /// </summary>
        /// <param name="beer"></param>
        /// <returns></returns>
        /// <response code="200">Beer added successfully in database</response>
        /// <response code="400">Bad request object</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult AddBeer([FromBody] Beer beer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = _repository.AddBeer(beer);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in action: AddBeer. Details: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Updates beer rating
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Beer updated successfully</response>
        /// <response code="400">Bad request object</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Beer))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult UpdateBeer([FromBody] UpdateBeer model)
        {
            try
            {
                var result = _repository.UpdateBeer(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in action: UpdateBeer. Details: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
    }
}