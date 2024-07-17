using CriEduc.School.Border.Shared;
using CriEduc.School.Border.Shared.Enum;

namespace CriEduc.School.Border.Constants
{
    public static class Message
    {        
        public static string InternalServerErrorTeacherUpdate = "Unexpected failure during the update process.";
        public const string ValidateRequest = "Request not valid.";

        public static string NotFoundErrorTeacher(Guid teacherId) =>
            $"Could not find a teacher with id {teacherId}";
    }

  
}
