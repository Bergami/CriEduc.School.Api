using CriEduc.School.Api.Extensions;
using CriEduc.School.Api.Models;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CriEduc.School.Api.Controllers
{
    [ApiController]
    [Route("v1/teacher/")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class TeacherController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;

        public TeacherController(IActionResultConverter actionResultConverter)
        {
            _actionResultConverter = actionResultConverter;
        }

        /// <summary>
        /// Returns a teacher according to the given parameter        
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTeacherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id, [FromServices] IGetTeacherUseCase getTeacherUseCase)
        {
            var result = await getTeacherUseCase.Execute(id);

            return _actionResultConverter.Convert(result);
        }

        /// <summary>
        /// Search teachers
        /// </summary>        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetTeacherResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] SearchTeacherRequest request, [FromServices] ISearchTeacherUseCase searchTeacherUseCase)
        {
            var result = await searchTeacherUseCase.Execute(request);                        

            return _actionResultConverter.Convert(result);
        }

        /// <summary>
        /// Create a teacher in database        
        /// </summary>        
        [HttpPost]
        [ProducesResponseType(typeof(CreateTeacherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateTeacherResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTeacherRequest request, [FromServices] ICreateTeacherUseCase createTeacherUseCase)
        {
            var result = await createTeacherUseCase.Execute(request);

            return _actionResultConverter.Convert(result);            
        }

        [HttpPut]        
        [ProducesResponseType(typeof(CreateTeacherResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromQuery] Guid id,[FromBody] UpdateTeacherRequest request, [FromServices] IUpdateTeacherUseCase updateTeacherUseCase)
        {
            request.Id = id;

            var result = await updateTeacherUseCase.Execute(request);

            return _actionResultConverter.Convert(result);
        }

        /// <summary>
        /// Delete a teacher in database        
        /// </summary>        
        [HttpDelete("{id}")]        
        [ProducesResponseType(typeof(CreateTeacherResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, [FromServices] IDeleteTeacherUseCase deleteTeacherUseCase)
        {
            var result = await deleteTeacherUseCase.Execute(id);

            return _actionResultConverter.Convert(result);
        }
    }
}