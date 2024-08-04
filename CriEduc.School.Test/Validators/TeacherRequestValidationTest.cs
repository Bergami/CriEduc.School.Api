using Bogus.DataSets;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Entities;
using CriEduc.School.Border.Enum;
using CriEduc.School.Border.Validators;
using CriEduc.School.Test.Builders;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Xunit;

namespace CriEduc.School.Test.Validators
{
    public class CreateTeacherValidationTests
    {
        private readonly CreateTeacherValidation _validator;
        private CreateTeacherRequest _request;

        public CreateTeacherValidationTests()
        {
            _validator = new CreateTeacherValidation();
            _request = new CreateTeacherRequestBuilder().Build();
        }

        [Theory]
        [InlineData("João Silva", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Name_Should_Not_Be_Empty(string name, bool expectError)
        {
            _request.Name = name;
            
            var result = _validator.TestValidate(_request);

            if (!expectError)
                result.ShouldHaveValidationErrorFor(x => x.Name);
            else
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("1990-05-15", true)] 
        [InlineData("", false)] 
        [InlineData(null, false)]
        public void Birth_Should_Not_Be_Empty(string birth, bool expectError)
        {
            _request = new CreateTeacherRequestBuilder().BuildNullBirth();

            if (!string.IsNullOrEmpty(birth)) 
                _request.Birth = DateTime.Parse(birth);

            var result = _validator.TestValidate(_request);

            if (!expectError)
                result.ShouldHaveValidationErrorFor(x => x.Birth);
            else
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("Matemática", true)] 
        [InlineData("", false)] 
        [InlineData(null, false)] 
        public void Specialty_Should_Not_Be_Empty(string specialty, bool expectError)
        {            
            _request.Specialty = specialty;

            var result = _validator.TestValidate(_request);

            if (!expectError)
                result.ShouldHaveValidationErrorFor(x => x.Specialty);
            else
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(40, true)] 
        [InlineData(-10, false)] 
        [InlineData(0, false)] 
        public void Workload_Should_Not_Be_Empty(int workload, bool expectError)
        {
            _request.Workload = 0;
            _request.Workload = workload;
            var result = _validator.TestValidate(_request);

            if (!expectError)
                result.ShouldHaveValidationErrorFor(x => x.Workload);
            else
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(Period.Morning, true)]     
        [InlineData(null, false)] 
        public void WorkPeriod_Should_Be_Valid(Period workPeriod, bool expectError)
        {
            _request.WorkPeriod =  workPeriod;
            var result = _validator.TestValidate(_request);           

            if (!expectError)
                result.ShouldHaveValidationErrorFor(x => x.WorkPeriod);
            else
                result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
