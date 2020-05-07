namespace MovieWeb.WebApi.Controller.v1
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
     Route("/api/v{version:apiVersion}/people")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Description: Delete a person from the system.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="id">Person id</param>
        /// <response code="204">Person deleted successfully</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Person not deleted, the person id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse),  StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (await _service.DeleteAsync(id))
               return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Get all existing people from the system.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <response code="200">People found</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">People not found, there are no records to show</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>List of recovered people</returns>
        [ProducesResponseType(typeof(List<PersonViewModel>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var entities = await _service.GetAllAsync();

            if (entities.Count > 0)
                return Ok(entities);
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Gets a person from the system.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="id">Person id</param>
        /// <response code="200">Person found</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Person not found, the person id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Person recovered</returns>
        [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpGet("{id:int}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var entity = await _service.GetAsync(id);

            if (entity is null)
                return NotFound();
            else
                return Ok(entity);
        }

        /// <summary>
        /// Description: Add a new person.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="apiVersion">Api version</param>
        /// <param name="model">Represents a property model for a person, check the detail in the schematics section</param>
        /// <response code="201">Person created successfully, return the person id</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPost]
        public async Task<IActionResult> InsertPerson([FromBody] PersonViewModel model, ApiVersion apiVersion)
        {
            var (isSuccess, id) = await _service.InsertAsync(model);

            if (isSuccess)
                return CreatedAtAction(nameof(GetPerson), new { id, version = apiVersion.ToString() }, id);
            else
                return BadRequest();
        }

        /// <summary>
        /// Description: Update a person.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="model">Represents a property model for a person, check the detail in the schematics section</param>
        /// <response code="204">Person successfully updated</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Person not update, the person id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPut]        
        public async Task<IActionResult> UpdatePerson([FromBody] PersonViewModel model)
        {
            if (await _service.UpdateAsync(model))
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Partially update a person.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="id">Person id</param>
        /// <param name="model">Represents a property model for a person, check the detail in the schematics section</param>
        /// <response code="204">Person successfully updated</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Person not update, the person id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdatePartialPerson(int id, [FromBody] JsonPatchDocument<PersonViewModel> model)
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
