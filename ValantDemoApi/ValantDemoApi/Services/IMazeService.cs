using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValantDemoApi.Infrastructure;
using ValantDemoApi.Shared.Dtos.Responses;

namespace ValantDemoApi.Services
{
  public interface IMazeService
  {
    MazeDto GetMazeById(int mazeId);
    MazeEntity GetMazeEntityById(int mazeId);
    List<MazeDto> GetMazes();
    string[] GetPossibleNextMoves(int mazeId, int positionX, int positionY);
    Task<int> SaveMazeAsync(IFormFile file);
  }
}
