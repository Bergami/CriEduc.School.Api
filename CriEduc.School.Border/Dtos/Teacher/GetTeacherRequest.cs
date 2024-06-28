namespace CriEduc.School.Border.Dtos.Teacher;

/// <summary>
/// Request necessário para obter o registro desejado
/// </summary>
/// <param name="Id">identificador do registro</param>
public record GetTeacherRequest(Guid Id);
