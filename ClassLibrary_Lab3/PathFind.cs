
namespace ClassLibrary_Lab3
{
    public class PathFinder
    {
        private readonly Grid _grid;
        private readonly bool[,] _visited;
        private readonly int[,] _directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

        public PathFinder(Grid grid)
        {
            _grid = grid;
            _visited = new bool[grid.Size, grid.Size];
        }

        public bool FindPath()
        {
            var queue = new Queue<(int x, int y, List<(int x, int y)> path)>();
            queue.Enqueue((_grid.Start.x, _grid.Start.y, new List<(int x, int y)> { _grid.Start }));
            _visited[_grid.Start.x, _grid.Start.y] = true;

            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                if ((currentX, currentY) == _grid.End)
                {
                    foreach (var (x, y) in path)
                    {
                        if (_grid.Cells[x, y] == '.')
                            _grid.Cells[x, y] = '+';
                    }
                    _grid.Cells[_grid.Start.x, _grid.Start.y] = '@';
                    _grid.Cells[_grid.End.x, _grid.End.y] = 'X';
                    return true;
                }

                for (int i = 0; i < _directions.GetLength(0); i++)
                {
                    int newX = currentX + _directions[i, 0];
                    int newY = currentY + _directions[i, 1];

                    if (IsValidMove(newX, newY))
                    {
                        _visited[newX, newY] = true;
                        var newPath = new List<(int x, int y)>(path) { (newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                    }
                }
            }
            return false;
        }

        private bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < _grid.Size &&
                   y >= 0 && y < _grid.Size &&
                   !_visited[x, y] &&
                   _grid.Cells[x, y] != 'O';
        }
    }
}
