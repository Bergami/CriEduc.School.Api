using CriEduc.School.Border.Enum;
using System.Text.Json.Serialization;

namespace CriEduc.School.Border.Dtos.Teacher
{
    public class GetTeacherResponse
    {
        /// <summary>
        /// Identificador do registro
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do professor / professora
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        /// <example>2022-03-28T15:35:32.384Z</example>
        public DateTime Birth { get; set; }

        /// <summary>
        /// Especialidade
        /// </summary>
        public string Specialty { get; set; }

        /// <summary>
        /// Carga horária
        /// </summary>
        public int WorkLoad { get; set; }

        /// <summary>
        /// Período em que irá trabalhar
        /// </summary>
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Period WorkPeriod { get; set; }
    }
}
