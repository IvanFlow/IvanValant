using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using ValantDemoApi.Infrastructure;
using ValantDemoApi.Services;
using ValantDemoApi.Shared.Dtos.Responses;

namespace ValantDemoApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MazeController : ControllerBase
  {
    private readonly ILogger<MazeController> _logger;
    private readonly IMazeService _mazeService;

    public MazeController(ILogger<MazeController> logger, IMazeService mazeService)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _mazeService = mazeService ?? throw new ArgumentNullException(nameof(mazeService));
    }

    /// <summary>
    /// Retrieves a txt file as it was introduces that containts the string for the maze related to the provided id 
    /// </summary>
    /// <param name="mazeId">Desired maze Id</param>
    /// <returns>A txt file</returns>
    [HttpGet("maze-file/{mazeId}")]
    [ProducesResponseType(200, Type = typeof(FileStreamResult))]
    public IActionResult GetMazeFileById([FromRoute] int mazeId)
    {
      var result = _mazeService.GetMazeEntityById(mazeId);

      return CreateFileStream(result);
    }

    /// <summary>
    /// Gets a dto model based on a List of List of Chars associated with the provided maze id
    /// </summary>
    /// <param name="mazeId">Desired maze Id</param>
    /// <returns>Maze Dto</returns>
    [HttpGet("{mazeId}")]
    [ProducesResponseType(200, Type = typeof(MazeDto))]
    public IActionResult GetMazeById([FromRoute] int mazeId)
    {
      var result = _mazeService.GetMazeById(mazeId);

      return Ok(result);
    }

    /// <summary>
    /// Gets a list of all dto models based on a List of List of Chars
    /// </summary>
    /// <returns>List of Maze Dto</returns>
    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(List<MazeDto>))]
    public IActionResult GetAllMazes()
    {
      var result = _mazeService.GetMazes();

      return Ok(result);
    }

    /// <summary>
    /// Save a new maze in the database based on a formated txt file
    /// </summary>
    /// <param name="file">the txt file</param>
    /// <returns>The maze id of the just created maze</returns>
    [HttpPost()]
    [ProducesResponseType(200, Type = typeof(int))]
    public async Task<IActionResult> SaveMaze(IFormFile file)
    {
      if (file == null || file.Length == 0)
        return BadRequest("File is empty or not provided.");

      var result = await _mazeService.SaveMazeAsync(file);

      return Ok(result);
    }

    /// <summary>
    /// Gets all posible moves for a certain scenario based on maze id, and the current position
    /// </summary>
    /// <param name="mazeId"></param>
    /// <param name="positionX"></param>
    /// <param name="positionY"></param>
    /// <returns>A string collection with the possible values: "You won!", "Invalid position!", "UP", "DOWN", "LEFT", "RIGHT" </returns>

    [HttpGet("GetPossibleNextMoves")]
    public IActionResult GetPossibleNextMoves([FromQuery] int mazeId, [FromQuery] int positionX, [FromQuery] int positionY)
    {
      var result = _mazeService.GetPossibleNextMoves(mazeId, positionX, positionY);

      return Ok(result);
    }

    #region private
    private FileStreamResult CreateFileStream(MazeEntity mazeEntity)
    {
      var matrix = mazeEntity.Maze;
      var memoryStream = new MemoryStream();
      using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, 1024, leaveOpen: true))
      {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
          for (int j = 0; j < cols; j++)
          {
            writer.Write(matrix[i, j]);
          }
          writer.WriteLine();
        }
      }

      memoryStream.Position = 0;

      var file = File(memoryStream, "text/plain", $"matrix-{mazeEntity.MazeId}.txt");
      return file;
    }
    #endregion

  }
}
