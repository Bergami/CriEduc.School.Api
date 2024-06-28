using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriEduc.School.UseCase.Teacher
{
    internal class UpdateTeacherUseCase : IUpdateTeacherUseCase
    {
        public Task<UseCaseResponse<UpdateTheacherResponse>> Execute((UpdateTheacherRequest, Guid) request)
        {
            throw new NotImplementedException();
        }
    }
}
