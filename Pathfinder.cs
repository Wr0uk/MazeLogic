using MazeLogic.Pathfinding;

namespace MazeLogic;

public sealed class Pathfinder
{
    private MazeCellType[,] _maze;
    public Pathfinder(FinderType finderType, MazeCellType[,]? whereSearch)
    {
        _pathfindingMethodeOfClassType = finderType switch
        {
            //FinderType.Bfs => BreadthFirstSearch.Start,
            FinderType.Dfs => DeepFieldSearch.Start,
            //FinderType.AStar => AStar.Start,
            _ => throw new ArgumentException($"cant find this finderType: {finderType.ToString()}")
        };
        _maze = whereSearch ?? throw new ArgumentException("Maze can not be null!");
    }
    
    private delegate MazeCellType[,] PathfindingFunction(MazeCellType[,] maze);

    private readonly PathfindingFunction _pathfindingMethodeOfClassType;

    public MazeCellType[,] FindRoute() => _pathfindingMethodeOfClassType(_maze);
    
    internal static (int, int) FindStart(ref MazeCellType[,] maze)
    {
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                if (maze[y,x] is MazeCellType.Start)
                {
                    return (y, x);
                }
            }
        }

        throw new ArgumentException("Can't find start");
    }

    internal static List<(int, int)> FindNextRoads(ref MazeCellType[,] maze, (int, int) position)
    {
        List<(int, int)> avaliable = new List<(int, int)>();
        List<(int, int)> localOffset = new List<(int, int)>();

        if (position.Item1!=0) localOffset.Add((-1,0));
        if (position.Item2!=0) localOffset.Add((0,-1));
        if (position.Item1!=maze.GetLength(0)-1) localOffset.Add((1,0));
        if (position.Item2!=maze.GetLength(1)-1) localOffset.Add((0,1));
        for (int i = 0; i < localOffset.Count; i++)
        {
            int offsetY = position.Item1 + localOffset[i].Item1;
            int offsetX = position.Item2 + localOffset[i].Item2;
            if (maze[offsetY, offsetX] is MazeCellType.Road || maze[offsetY, offsetX] is MazeCellType.Finish)
                avaliable.Add((offsetY, offsetX));
        }

        return avaliable;
    }
}

public enum FinderType : byte
{
    Dfs,
    Bfs,
    AStar
}