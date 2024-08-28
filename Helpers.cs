using System.Collections;
using System.Collections.Generic;

public static class Helpers
{
    public static int ManhattanDistance(int x1, int x2, int y1, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    public static bool IsWithinCircle(int sourceX, int sourceY, int targetX, int targetY, float radius)
    {
        // Calculate the squared distance between the source and target coordinates
        float dx = targetX - sourceX;
        float dy = targetY - sourceY;
        float distanceSquared = dx * dx + dy * dy;

        // Compare the squared distance with the squared radius
        float radiusSquared = radius * radius;

        return distanceSquared <= radiusSquared;
    }
}
