using System.Collections.Generic;
using System.Linq;

namespace ValantDemoApi.Infrastructure
{
  public class MazeContext : IMazeContext
  {
    private readonly List<MazeEntity> _mazes;

    public MazeContext()
    {
      _mazes = new List<MazeEntity>();
    }

    public int Add(char[,] maze)
    {
      var newMazeEntity = new MazeEntity()
      {
        MazeId = _mazes.Count + 1,
        Maze = maze
      };

      _mazes.Add(newMazeEntity);

      return newMazeEntity.MazeId;
    }

    public List<MazeEntity> GetAll()
    {
      return _mazes;
    }

    public MazeEntity GetById(int mazeId)
    {
      return _mazes.FirstOrDefault(m => m.MazeId == mazeId);
    }
  }

  public class MazeEntity
  {
    public int MazeId { get; set; }

    public char[,] Maze { get; set; }
  }
}
