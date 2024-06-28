namespace CriEduc.School.Border.Shared
{
    public interface IUseCase<in TRequest, TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);        
    }  
}
