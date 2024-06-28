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
            using var activity = OpenTelemetryExtension.ActivitySource.StartActivity("GetTeacher");

            activity?.SetTag("Id", id);
            activity?.SetTag("User", "Wander Vinicius Bergami");
            activity?.SetTag("baz", new int[] { 1, 2, 3 });

            var result = await getTeacherUseCase.Execute(new GetTeacherRequest(id));

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
        /// Register a teacher in database        
        /// </summary>        
        [HttpPost("register")]
        [ProducesResponseType(typeof(CreateTeacherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateTeacherResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] CreateTeacherRequest request, [FromServices] ICreateTeacherUseCase createTeacherUseCase)
        {
            var result = await createTeacherUseCase.Execute(request);

            return _actionResultConverter.Convert(result);            
        }
    }
}