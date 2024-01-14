using System.Collections.Generic;

namespace ProcGenTiles
{
	public class Pathfinding
	{
		HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();
		Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
		Map Map;

		public Pathfinding(Map map)
		{
			Map = map;
		}

		public void BFS((int x, int y) start)
		{
			HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();
			Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

			visited.Add(start);
			queue.Enqueue(start);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();

				// Add neighboring tiles to the queue if not visited for 4 dir pathfinding: diamonds
				AddNeighborToQueue(queue, visited, current.x - 1, current.y);
				AddNeighborToQueue(queue, visited, current.x + 1, current.y);
				AddNeighborToQueue(queue, visited, current.x, current.y - 1);
				AddNeighborToQueue(queue, visited, current.x, current.y + 1);
				
				//Thinking about running a Func<> through the params to determine what to do with the found tiles
				
			}
		}

		private void AddNeighborToQueue(Queue<(int x, int y)> queue, HashSet<(int x, int y)> visited, int x, int y)
		{
			if (IsValidTilePosition(x, y) && !visited.Contains((x, y)))
			{
				visited.Add((x, y));
				queue.Enqueue((x, y));
			}
		}

		private bool IsValidTilePosition(int x, int y)
		{
			return x >= 0 && x < Map.Tiles.GetLength(0) && y >= 0 && y < Map.Tiles.GetLength(1);
		}
	}
}