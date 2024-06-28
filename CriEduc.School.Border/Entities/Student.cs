using CriEduc.School.Border.Enum;

namespace CriEduc.School.Border.Entities
{
    public class Student : Person
    {
        public Level Level{ get; set; }
               
        public Period WorkPeriod { get; set; }

        public string Observation { get; set; }
    }
}
