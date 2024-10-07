using System.Collections.Generic;

namespace ValantDemoApi.Infrastructure
{
  public interface IMazeContext
  {
    public int Add(char[,] maze);
    MazeEntity GetById(int mazeId);
    List<MazeEntity> GetAll();
  }
}
