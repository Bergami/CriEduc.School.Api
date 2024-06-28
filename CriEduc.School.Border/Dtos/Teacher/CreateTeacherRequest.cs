using CriEduc.School.Border.Enum;

namespace CriEduc.School.Border.Dtos.Teacher
{
    public class CreateTeacherRequest
    {
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
        public int Workload { get; set; }

        /// <summary>
        /// Período em que irá trabalhar
        /// </summary>
        public Period WorkPeriod { get; set; }
    }
}
