using AgroSolutions.History.Application.DTOs;

namespace AgroSolutions.History.Application.Interfaces;

public interface ISensorService
{
    Task<ReadingResponse> RegisterReadingAsync(CreateReadingRequest request);
}