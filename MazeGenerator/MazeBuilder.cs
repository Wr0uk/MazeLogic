namespace MazeLogic.MazeGenerator;

public static class MazeBuilder
{
    public static MazeCellType[,] Build(int horizontalSize, int verticalSize)
    {
        if (horizontalSize % 2 == 0 || verticalSize % 2 == 0)
            throw new ArgumentException("Sizes must be odd!");
        
        MazeCellType[,] maze = Initialize(horizontalSize,verticalSize);

        RoadBuilder[] builders = new RoadBuilder[15];

        for (int i = 0; i < builders.Length; i++)
        {
            builders[i] = new RoadBuilder();
        }

        while (IsMazeNotCompleted(maze))
        {
            foreach (var builder in builders)
            {
                builder.BuildRoad(ref maze);
            }
        }
        
        return maze;
    }

    private static bool IsMazeNotCompleted(MazeCellType[,] maze)
    {
        for (int y = 0; y < maze.GetLength(0); y+=2)
        {
            for (int x = 0; x < maze.GetLength(1); x+=2)
            {
                if (maze[y, x] is MazeCellType.Wall) return true;
            }
        }

        return false;
    }

    private static MazeCellType[,] Initialize(int horizontalSize, int verticalSize)
    {
        MazeCellType[,] maze = new MazeCellType[verticalSize, horizontalSize];
        for (int y = 0; y < verticalSize; y++)
        {
            for (int x = 0; x < horizontalSize; x++)
            {
                maze[y, x] = MazeCellType.Wall;
            }
        }

        maze[0, 0] = MazeCellType.Road;

        return maze;
    }

    public static MazeCellType[,] CreateRandomStartAndFinish(MazeCellType[,] maze)
    {
        if (IsNotClear(ref maze)) 
            ClearMaze(ref maze);
        
        (int posY, int posX) = GetRandomPositionOnRoad(ref maze);
        maze[posY, posX] = MazeCellType.Start;

        (posY, posX) = GetRandomPositionOnRoad(ref maze);
        maze[posY, posX] = MazeCellType.Finish;
        
        return maze;
    }

    private static (int, int) GetRandomPositionOnRoad(ref MazeCellType[,] maze)
    {
        Random rnd = new Random();
        int sizeAboveY = maze.GetLength(0);
        int sizeAboveX = maze.GetLength(1);
        int posY = rnd.Next(0,sizeAboveY);
        int posX = rnd.Next(0, sizeAboveX);
        while (maze[posY, posX] is not MazeCellType.Road)
        {
            posY = rnd.Next(0, sizeAboveY);
            posX = rnd.Next(0, sizeAboveX);
        }

        return (posY, posX);
    }

    private static bool IsNotClear(ref MazeCellType[,] maze)
    {
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                if (maze[y,x] is MazeCellType.Wall || maze[y,x] is MazeCellType.Road) continue;
                
                return true;
            }
        }

        return false;
    }

    private static void ClearMaze(ref MazeCellType[,] maze)
    {
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                if (maze[y, x] is MazeCellType.Wall) continue;
                maze[y, x] = MazeCellType.Road;
            }
        }
    }
}