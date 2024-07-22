using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Repository.Helpers;
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

        public const string DeleteTeacherById = @"DELETE FROM Teacher WHERE Id = @Id";

        public static string SearchTeacher(SearchTeacherRequest request)
        {
            var whereClause = TeacherQueryBuilder.BuildWhereClause(request);
            var queryBuilder = new StringBuilder("SELECT * FROM Teacher");
            queryBuilder.Append(whereClause);

            // Add pagination
            queryBuilder.Append(" ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY");

            return queryBuilder.ToString();            
        }

        public static string SearchTeacherTotalCount(SearchTeacherRequest request)
        {
            var whereClause = TeacherQueryBuilder.BuildWhereClause(request);
            var queryBuilder = new StringBuilder("SELECT COUNT(*) FROM Teacher");
            queryBuilder.Append(whereClause);

            return queryBuilder.ToString();
        }

    }
}
