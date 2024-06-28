using CriEduc.School.Border.Shared;
using CriEduc.School.Border.Shared.Enum;

namespace CriEduc.School.Border.Shared
{
    public class UseCaseResponse <T> : IResponse
    {
        public UseCaseResponseKind Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Result { get; private set; }
        public IEnumerable<ErrorMessage> Erros { get; private set; }

        public bool Sucess() => Erros?.All(e => string.IsNullOrWhiteSpace(e.Message)) ?? string.IsNullOrWhiteSpace(ErrorMessage);

        public UseCaseResponse<T> SetSuccess(T result)
        {
            Result = result;

            return SetStatus(UseCaseResponseKind.Sucess);
        }

        public UseCaseResponse<T> SetBadResquest( string errorMessage = null, IEnumerable<ErrorMessage> erros = null)
        {
            return SetStatus(UseCaseResponseKind.BadRequest, errorMessage, erros);
        }

        public UseCaseResponse<T> SetNotFound(string errorMessage = null, IEnumerable<ErrorMessage> erros = null)
        {
            return SetStatus(UseCaseResponseKind.NotFound, errorMessage, erros);
        }

        public UseCaseResponse<T> SetStatus(UseCaseResponseKind status, string errorMessage= null, IEnumerable<ErrorMessage> erros = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Erros = erros;

            return this;
        }
    }

   
}
