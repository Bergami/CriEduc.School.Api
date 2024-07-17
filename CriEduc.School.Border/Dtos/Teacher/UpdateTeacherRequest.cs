using System.Text.Json.Serialization;

namespace CriEduc.School.Border.Dtos.Teacher
{
    public class UpdateTeacherRequest : TeacherRequest
    {
        /// <summary>
        /// Identificador do professor / professora
        /// </summary>
        
        [JsonIgnore]
        public Guid Id { get; set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        //public Period WorkPeriod { get; set; }
    }
}
