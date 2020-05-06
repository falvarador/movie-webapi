namespace Client.WebApi.Controller.v1
{
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using MovieWeb.WebApi.Common;
    using MovieWeb.WebApi.Service;
    using MovieWeb.WebApi.Model;

    [ApiController, 
     ApiVersion(Versions.v1), 
     Route("v{version:apiVersion}/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Description: Delete a movie from the system.
        /// Creation date: 06/05/2020
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <response code="204">Movie deleted successfully</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Movie not deleted, the movie id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse),  StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (await _service.DeleteAsync(id))
               return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Get all existing movies from the system.
        /// Creation date: 06/05/2020
        /// </summary>
        /// <response code="200">Movies found</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Movies not found, there are no records to show</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>List of recovered movies</returns>
        [ProducesResponseType(typeof(List<MovieViewModel>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var entities = await _service.GetAllAsync();

            if (entities.Count > 0)
                return Ok(entities);
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Gets a movie from the system.
        /// Creation date: 06/05/2020
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <response code="200">Movies found</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Movie not found, the movie id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Movie recovered</returns>
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpGet("{id}/{codActividad}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var entity = await _service.GetAsync(id);

            if (entity is null)
                return NotFound();
            else
                return Ok(entity);
        }

        /// <summary>
        /// Description: Add a new movie.
        /// Creation date: 06/05/2020
        /// </summary>
        /// <param name="model">Represents a property model for a movie, check the detail in the schematics section</param>
        /// <response code="200">Movie created successfully</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPost]
        public async Task<IActionResult> InsertMovie([FromBody] MovieViewModel model)
        {
            if (await _service.InsertAsync(model))
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Description: Update a movie.
        /// Creation date: 06/05/2020
        /// </summary>
        /// <param name="model">Represents a property model for a movie, check the detail in the schematics section</param>
        /// <response code="204">Movie successfully updated</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Movie not update, the movie id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPut]        
        public async Task<IActionResult> UpdateMovie([FromBody] MovieViewModel model)
        {
            if (await _service.UpdateAsync(model))
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Partially update a movie.
        /// Creation date: 06/05/2020
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <param name="model">Represents a property model for a movie, check the detail in the schematics section</param>
        /// <response code="204">Movie successfully updated</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Movie not update, the movie id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialMovie(int id, [FromBody] JsonPatchDocument<MovieViewModel> model)
        {
            var entity = await _service.GetAsync(id);
           if (entity is null)
                return NotFound();

            model.ApplyTo(entity);

            if (await _service.UpdateAsync(entity))
                return NoContent();
            else
                return NotFound();
        }
    }
}
