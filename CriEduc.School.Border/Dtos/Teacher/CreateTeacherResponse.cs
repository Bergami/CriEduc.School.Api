namespace CriEduc.School.Border.Dtos.Teacher
{
    public record CreateTeacherResponse
    {
        public CreateTeacherResponse(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Identificador do registro criado
        /// </summary>
        public Guid Id { get; init; }
    };    
}
