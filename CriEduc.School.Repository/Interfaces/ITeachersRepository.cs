﻿using CriEduc.School.Border.Dtos.Teacher;

namespace CriEduc.School.Repository.Interfaces
{
    public interface ITeachersRepository
    {
        Task<CreateTeacherResponse> Save(CreateTeacherRequest request);

        Task<GetTeacherResponse> Get(Guid id);

        Task<IEnumerable<GetTeacherResponse>> Search(SearchTeacherRequest request);

        Task<bool> UpdateTeacherAsync(UpdateTheacherRequest request, Guid id);
    }
}
