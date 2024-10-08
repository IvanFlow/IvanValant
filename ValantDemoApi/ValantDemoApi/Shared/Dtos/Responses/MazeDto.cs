using System.Collections.Generic;

namespace ValantDemoApi.Shared.Dtos.Responses
{
  public class MazeDto
  {
    public int MazeId { get; set; }
    public List<List<char>> Maze { get; set; }
  }
}
