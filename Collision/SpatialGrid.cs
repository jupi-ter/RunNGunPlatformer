using System.Numerics;

namespace RatGame;

// rethink this later. this approach will slow down at >=1000 CollisionShapes.
// look into quadtrees.

public class SpatialGrid(int cellSize)
{
    private int cellSize = cellSize;
    private Dictionary<Vector2Int, List<CollisionShape>> grid = [];

    public void Insert(CollisionShape shape)
    {
        Vector2Int coords = GetCell(shape.Origin);
        if (!grid.ContainsKey(coords))
            grid[coords] = [];

        grid[coords].Add(shape);
    }

    public IEnumerable<List<CollisionShape>> GetAllCells()
    {
        return grid.Values;
    }

    public Vector2Int GetCell(Vector2 position)
    {
        return new Vector2Int((int)(position.X / cellSize), (int)(position.Y / cellSize));
    }

    public void Clear()
    {
        grid.Clear();
    }
}
