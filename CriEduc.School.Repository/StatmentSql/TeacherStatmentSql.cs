using CriEduc.School.Border.Dtos.Teacher;
using System.Text;

namespace CriEduc.School.Repository.StatmentSql
{
    public static class TeacherStatmentSql
    {
        public const string InsertTeacher = @"INSERT INTO Teacher (Id, Name, Birth, Specialty, Workload, WorkPeriod)
                                            VALUES (@Id, @Name, @Birth, @Specialty, @Workload, @WorkPeriod) RETURNING Id";

        public const string GetTeacherById = @"SELECT * FROM Teacher WHERE Id = @Id";

        public const string UpdateTeacher = @"UPDATE Teacher SET 
                                                Name        = @Name, 
                                                Birth       = @Birth, 
                                                Specialty   = @Specialty, 
                                                Workload    = @Workload, 
                                                WorkPeriod  = @WorkPeriod
                                              WHERE Id = @Id";

        public static string SearchTeacher(SearchTeacherRequest request)
        {
            var queryBuilder = new StringBuilder("SELECT * FROM Teacher WHERE 1=1");

            // Add filters based on request parameters
            if (!string.IsNullOrEmpty(request.Name))
                queryBuilder.Append(" AND Name ILIKE @Name");

            if (!string.IsNullOrEmpty(request.Specialty))
                queryBuilder.Append(" AND Specialty ILIKE @Specialty");

            if (request.WorkPeriod.HasValue)
            {
                queryBuilder.Append(" AND WorkPeriod = @WorkPeriod");
            }

            // Add pagination
            queryBuilder.Append(" ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY");

            return queryBuilder.ToString();
        }
    }
}
