using System.Diagnostics;

namespace MazeLogic.Pathfinding;

internal static class BreadthFirstSearch
{
    internal static MazeCellType[,] Start(MazeCellType[,] maze)
    {
        (int, int) start= Pathfinder.FindStart(ref maze);
        
        if(MoveNext(ref maze, start))
            Debug.WriteLine("Path was finded sucessful!");
        else
            Debug.WriteLine("Cannot get path!");
        
        return maze;
    }

    private static bool MoveNext(ref MazeCellType[,] maze, (int, int) position)
    {
        List<(int, int)> avaliableNextCell = Pathfinder.FindNextRoads(ref maze, position);
        
        

        return false;
    }
}