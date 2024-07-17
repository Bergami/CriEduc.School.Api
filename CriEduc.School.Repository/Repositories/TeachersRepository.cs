using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Enum;
using CriEduc.School.Repository.Data;
using CriEduc.School.Repository.Interfaces;
using CriEduc.School.Repository.StatmentSql;
using Dapper;
using System.Data;
using System.Data.Common;

namespace CriEduc.School.Repository.Repositories
{
    public class TeachersRepository : ITeachersRepository
    {
        private DbSession _session;
        public TeachersRepository(DbSession session) 
        {
            _session = session;
        }

        public async Task<GetTeacherResponse> Get(Guid id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Guid);

            return await _session.Connection.QueryFirstOrDefaultAsync<GetTeacherResponse>(TeacherStatmentSql.GetTeacherById, parameter);
        }

        public async Task<IEnumerable<GetTeacherResponse>> Search(SearchTeacherRequest request)
        {
            var parameters = CreateFilterParameter(request);
            var query = TeacherStatmentSql.SearchTeacher(request);

            return await _session.Connection.QueryAsync<GetTeacherResponse>(query, parameters);
        }

        public async Task<CreateTeacherResponse> Save(CreateTeacherRequest request)
        {
            var parameters = CreateInsertParameter(request);
            
            var resultId = await _session.Connection.ExecuteScalarAsync<Guid>(TeacherStatmentSql.InsertTeacher, parameters, _session.Transaction);

            return new CreateTeacherResponse(resultId);
        }
        public async Task<bool> UpdateTeacherAsync(UpdateTeacherRequest request)
        {
            var parameters = UpdateParameter(request);
           
            var result = await _session.Connection.ExecuteAsync(TeacherStatmentSql.UpdateTeacher, parameters, _session.Transaction);

            return result > 0;
        }
        private static DynamicParameters CreateInsertParameter(CreateTeacherRequest request)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", Guid.NewGuid(), DbType.Guid);

            parameter.AddDynamicParams(TeacherRequestParameter(request));

            return parameter;
        }

        private static DynamicParameters UpdateParameter(UpdateTeacherRequest request)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Guid);

            parameter.AddDynamicParams(TeacherRequestParameter(request));

            return parameter;
        }

        private static DynamicParameters TeacherRequestParameter(TeacherRequest request)
        {
            var parameter = new DynamicParameters();            
            parameter.Add("@Name", request.Name, DbType.String);
            parameter.Add("@Birth", request.Birth, DbType.Date);
            parameter.Add("@Specialty", request.Specialty, DbType.String);
            parameter.Add("@WorkPeriod", Enum.GetName(typeof(Period), request.WorkPeriod), DbType.String);
            parameter.Add("@Workload", request.Workload, DbType.Int32);

            return parameter;
        }

        private static DynamicParameters CreateFilterParameter(SearchTeacherRequest request)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Take", request.PagingParam.Take, DbType.Int16);
            parameter.Add("@Skip", request.PagingParam.Skip, DbType.Int16);

            if (request.WorkPeriod is not null)
                parameter.Add("@WorkPeriod", Enum.GetName(typeof(Period), request.WorkPeriod!.Value), DbType.String);

            if (!string.IsNullOrEmpty(request.Name))
                parameter.Add("@Name", request.Name, DbType.String);

            if (!string.IsNullOrEmpty(request.Specialty))
                parameter.Add("@Specialty", request.Specialty, DbType.String);

            return parameter;
        }       
    }
}