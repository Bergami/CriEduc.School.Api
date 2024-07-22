using CriEduc.School.Border.Dtos.Teacher;
using System.Text;

namespace CriEduc.School.Repository.Helpers
{
    public static class TeacherQueryBuilder
    {
        public static StringBuilder BuildWhereClause(SearchTeacherRequest request)
        {
            var queryBuilder = new StringBuilder(" WHERE 1=1");

            if (!string.IsNullOrEmpty(request.Name))
                queryBuilder.Append(" AND Name ILIKE @Name");

            if (!string.IsNullOrEmpty(request.Specialty))
                queryBuilder.Append(" AND Specialty ILIKE @Specialty");

            if (request.WorkPeriod.HasValue)
            {
                queryBuilder.Append(" AND WorkPeriod = @WorkPeriod");
            }

            return queryBuilder;
        }
    }
}
