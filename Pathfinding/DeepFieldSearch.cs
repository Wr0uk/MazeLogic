using System.Diagnostics;
using MazeLogic;

namespace MazeLogic.Pathfinding;

internal static class DeepFieldSearch
{
    internal static MazeCellType[,] Start(MazeCellType[,] maze)
    {
        (int, int) start= Pathfinder.FindStart(ref maze);

        MoveNext(ref maze, start);
        
        return maze;
    }

    private static bool MoveNext(ref MazeCellType[,] maze, (int, int) position)
    {
        if (maze[position.Item1, position.Item2] is MazeCellType.Finish)
        {
            maze[position.Item1, position.Item2] = MazeCellType.Route;
            return true;
        }
        maze[position.Item1, position.Item2] = MazeCellType.Visited;
        
        List<(int, int)> avaliableNextPositions = Pathfinder.FindNextRoads(ref maze, position);
        
        if (avaliableNextPositions.Count == 0) 
            return false;
        
        Random random = new Random();

        while (avaliableNextPositions.Count!=0)
        {
            (int, int) nextPosition = avaliableNextPositions[random.Next(0, avaliableNextPositions.Count)];
            avaliableNextPositions.Remove(nextPosition);
            if (!MoveNext(ref maze, nextPosition)) continue;
            maze[position.Item1, position.Item2] = MazeCellType.Route;
            return true;
        }

        return false;
    }
} 