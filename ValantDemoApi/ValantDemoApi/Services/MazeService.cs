using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ValantDemoApi.Infrastructure;
using ValantDemoApi.Shared.Dtos.Responses;
using ValantDemoApi.Shared.Mappers;

namespace ValantDemoApi.Services
{
  public class MazeService : IMazeService
  {
    private readonly IMazeContext _mazeContext;
    public MazeService(IMazeContext mazeContext)
    {
      _mazeContext = mazeContext ?? throw new ArgumentNullException(nameof(mazeContext));
    }

    public MazeDto GetMazeById(int mazeId)
    {
      var mazeEntiy = _mazeContext.GetById(mazeId);
      return mazeEntiy.ToDTO();
    }

    public MazeEntity GetMazeEntityById(int mazeId)
    {
      var mazeEntiy = _mazeContext.GetById(mazeId);
      return mazeEntiy;
    }

    public List<MazeDto> GetMazes()
    {
      var mazeEntities = _mazeContext.GetAll();

      return mazeEntities.Select(m => m.ToDTO()).ToList();
    }

    public string[] GetPossibleNextMoves(int mazeId, int positionX, int positionY)
    {
      var result = new List<string>();

      var mazeMatrix = _mazeContext.GetById(mazeId).Maze;

      int maxY = mazeMatrix.GetLength(0) - 1;
      int maxX = mazeMatrix.GetLength(1) - 1;

      if (mazeMatrix[positionY, positionX] == 'E')
      {
        result.Add("You won!");

        return result.ToArray();
      }

      if (mazeMatrix[positionY, positionX] == 'X')
      {
        result.Add("Invalid Position!");

        return result.ToArray();
      }

      //verify UP
      if (positionY > 0)
      {
        if (mazeMatrix[positionY - 1, positionX] == 'O')
        {
          result.Add("UP");
        }
      }

      //verify DOWN
      if (positionY < maxY)
      {
        if (mazeMatrix[positionY + 1, positionX] == 'O')
        {
          result.Add("DOWN");
        }
      }

      //verify LEFT
      if (positionX > 0)
      {
        if (mazeMatrix[positionY, positionX - 1] == 'O')
        {
          result.Add("LEFT");
        }
      }

      //verify RIGHT
      if (positionX < maxX)
      {
        if (mazeMatrix[positionY, positionX + 1] == 'O')
        {
          result.Add("RIGHT");
        }
      }


      return result.ToArray();
    }

    public async Task<int> SaveMazeAsync(IFormFile file)
    {
      char[,] matrix;
      using (var stream = new StreamReader(file.OpenReadStream()))
      {
        var content = await stream.ReadToEndAsync();
        content = content.Replace("\r", "");
        var lines = content.Split('\n');

        int rows = lines.Length;
        int cols = lines[0].ToCharArray().Length;

        matrix = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
          var cells = lines[i].Trim().ToCharArray();

          for (int j = 0; j < cols; j++)
          {
            matrix[i, j] = cells[j];
          }
        }
      }

      return _mazeContext.Add(matrix);
    }
  }
}
