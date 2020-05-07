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
     Route("/api/v{version:apiVersion}/genders")]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _service;

        public GenderController(IGenderService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Description: Delete a gender from the system.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="id">Gender id</param>
        /// <response code="204">Gender deleted successfully</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Gender not deleted, the gender id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse),  StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            if (await _service.DeleteAsync(id))
               return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Get all existing genders from the system.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <response code="200">Genders found</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Genders not found, there are no records to show</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>List of recovered genders</returns>
        [ProducesResponseType(typeof(List<GenderViewModel>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpGet]
        public async Task<IActionResult> GetAllGenders()
        {
            var entities = await _service.GetAllAsync();

            if (entities.Count > 0)
                return Ok(entities);
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Gets a gender from the system.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="id">Gender id</param>
        /// <response code="200">Gender found</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Gender not found, the gender id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Gender recovered</returns>
        [ProducesResponseType(typeof(GenderViewModel), StatusCodes.Status200OK),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpGet("{id:int}")]
        public async Task<IActionResult> GetGender(int id)
        {
            var entity = await _service.GetAsync(id);

            if (entity is null)
                return NotFound();
            else
                return Ok(entity);
        }

        /// <summary>
        /// Description: Add a new gender.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="apiVersion">Api version</param>
        /// <param name="model">Represents a property model for a gender, check the detail in the schematics section</param>
        /// <response code="201">Gender created successfully, return the gender id</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPost]
        public async Task<IActionResult> InsertGender([FromBody] InsertGenderViewModel model, ApiVersion apiVersion)
        {
            var (isSuccess, id) = await _service.InsertAsync(model);

            if (isSuccess)
                return CreatedAtAction(nameof(GetGender), new { id, version = apiVersion.ToString() }, id);
            else
                return BadRequest();
        }

        /// <summary>
        /// Description: Update a gender.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="model">Represents a property model for a gender, check the detail in the schematics section</param>
        /// <response code="204">Gender successfully updated</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Gender not update, the gender id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPut]        
        public async Task<IActionResult> UpdateGender([FromBody] GenderViewModel model)
        {
            if (await _service.UpdateAsync(model))
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Description: Partially update a gender.
        /// Creation date: 07/05/2020
        /// </summary>
        /// <param name="id">Gender id</param>
        /// <param name="model">Represents a property model for a gender, check the detail in the schematics section</param>
        /// <response code="204">Gender successfully updated</response>
        /// <response code="400">Bad request, verify the parameters sent or fulfillment of the validations</response>
        /// <response code="404">Gender not update, the gender id entered was not found</response>
        /// <response code="500">Sorry, the request could not be attended by the server</response>
        /// <returns>Operation indicator based on http code, does not return values</returns>
        [ProducesResponseType(typeof(BlankResponse), StatusCodes.Status204NoContent),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(BlankResponse), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError),
         HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdatePartialGender(int id, [FromBody] JsonPatchDocument<GenderViewModel> model)
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
