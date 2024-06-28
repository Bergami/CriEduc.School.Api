using CriEduc.School.Border.Enum;

namespace CriEduc.School.Border.Dtos.Teacher
{
    public class SearchTeacherRequest
    {
        public Period? WorkPeriod { get; set; }

        public string? Name { get; set; }

        public string? Specialty { get; set; }

        public PagingParam PagingParam { get; set; } 

        public SearchTeacherRequest()
        {
            this.PagingParam = new PagingParam();
        }
    }   
}
