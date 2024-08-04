using CriEduc.School.Border.Dtos.Teacher;

namespace CriEduc.School.Test.Builders
{
    public class CreateTeacherRequestBuilder : TeacherRequestBuilder
    {
        public CreateTeacherRequest Build()
        {
            return new CreateTeacherRequest()
            {
                Name = _teacherRequest.Name,
                Birth = _teacherRequest.Birth,
                Specialty = _teacherRequest.Specialty,
                Workload = _teacherRequest.Workload,
                WorkPeriod = _teacherRequest.WorkPeriod
            };
        }

        public CreateTeacherRequest BuildNullBirth()
        {
            return new CreateTeacherRequest()
            {
                Name = _teacherRequest.Name,               
                Specialty = _teacherRequest.Specialty,
                Workload = _teacherRequest.Workload,
                WorkPeriod = _teacherRequest.WorkPeriod
            };
        }
    }
}
