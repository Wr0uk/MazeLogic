namespace MazeLogic.MazeGenerator;

internal class RoadBuilder
{
    private int _posX = 0, _posY = 0;
    
    internal void BuildRoad(ref MazeCellType[,] maze)
    {
        MoveToSide(ref maze, PickRandomSide(ref maze));
    }

    private void MoveToSide(ref MazeCellType[,] maze,Sides side)
    {
        switch (side)
        {
            case Sides.Up:
                _posY -= 2;
                if (maze[_posY,_posX] is MazeCellType.Wall) maze[_posY + 1, _posX] = MazeCellType.Road;
                break;
            case Sides.Down:
                _posY += 2;
                if (maze[_posY,_posX] is MazeCellType.Wall) maze[_posY - 1, _posX] = MazeCellType.Road;
                break;
            case Sides.Right:
                _posX += 2;
                if (maze[_posY,_posX] is MazeCellType.Wall) maze[_posY, _posX-1] = MazeCellType.Road;
                break;
            case Sides.Left:
                _posX -= 2;
                if (maze[_posY,_posX] is MazeCellType.Wall) maze[_posY, _posX+1] = MazeCellType.Road;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
        maze[_posY, _posX] = MazeCellType.Road;
    }

    private Sides PickRandomSide(ref MazeCellType[,] maze)
    {
        Random random = new Random();
        List<Sides> pickFrom = AvaliableSides(ref maze);
        return pickFrom[random.Next(0,pickFrom.Count)];
    }

    private List<Sides> AvaliableSides(ref MazeCellType[,] maze)
    {
        List<Sides> sidesList = new List<Sides>();
        if (_posY!=0) sidesList.Add(Sides.Up);
        if (_posY!=maze.GetLength(0)-1) sidesList.Add(Sides.Down);
        if (_posX!=0) sidesList.Add(Sides.Left);
        if (_posX!= maze.GetLength(1)-1) sidesList.Add(Sides.Right);
        return sidesList;
    }

    private enum Sides : byte
    {
        Up,
        Down,
        Left,
        Right
    }
}