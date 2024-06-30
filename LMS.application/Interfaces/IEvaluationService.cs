using LMS.Application.DTOs;

namespace LMS.Application.Interfaces
{
    public interface IEvaluationService
    {
        Task<bool> AddEvaluationAsync(EvaluationDTO evaluationDTO);
        Task<bool> DeleteEvaluationAsync(string courseId);
    }
}
