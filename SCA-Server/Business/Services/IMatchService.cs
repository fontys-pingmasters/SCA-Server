using Business.Dtos.RequestDtos;
using Business.Entities;

namespace Business.Services;

public interface IMatchService
{
    Match CreateMatch(CreateMatchRequest createMatchRequest);
}