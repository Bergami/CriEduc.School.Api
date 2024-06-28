using CriEduc.School.Border.Enum;
using CriEduc.School.Border.Models;

namespace CriEduc.School.Border.Entities
{
    public class Person : Entity<Person>
    {    
        public string Name { get; set; }        
        public DateTime Birth { get; set; }
    }
}
