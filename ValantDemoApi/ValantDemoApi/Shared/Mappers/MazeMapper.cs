using System.Collections.Generic;
using ValantDemoApi.Infrastructure;
using ValantDemoApi.Shared.Dtos.Responses;

namespace ValantDemoApi.Shared.Mappers
{
  public static class MazeMapper
  {
    public static MazeDto ToDTO(this MazeEntity mazeEntity)
    {
      return new MazeDto
      {
        MazeId = mazeEntity.MazeId,
        Maze = ConvertMatrixToList(mazeEntity.Maze)
      };
    }

    private static List<List<char>> ConvertMatrixToList(char[,] matrix)
    {
      int rows = matrix.GetLength(0);
      int cols = matrix.GetLength(1);

      var listMatrix = new List<List<char>>(rows);

      for (int i = 0; i < rows; i++)
      {
        var row = new List<char>(cols);
        for (int j = 0; j < cols; j++)
        {
          row.Add(matrix[i, j]);
        }
        listMatrix.Add(row);
      }

      return listMatrix;
    }

  }
}
