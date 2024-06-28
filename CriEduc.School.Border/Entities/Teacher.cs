using CriEduc.School.Border.Enum;

namespace CriEduc.School.Border.Entities
{
    public class Teacher : Person
    {
        public string Specialty { get; set; }
        public int Workload { get; set; }
        public Period WorkPeriod { get; set; }
    }
}
