using AgroSolutions.History.Application.DTOs.Requests;
using AgroSolutions.History.Application.DTOs.Responses;

namespace AgroSolutions.History.Application.Interfaces;

public interface ISensorService
{
    Task<ReadingResponse> RegisterReadingAsync(CreateReadingRequest request);
    Task<IEnumerable<ReadingResponse>> GetReadingsAsync(GetReadingsQuery query);
}