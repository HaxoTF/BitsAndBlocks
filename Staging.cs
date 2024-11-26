using Microsoft.Xna.Framework;

namespace BitsAndBlocks;

public static class Stage
{
    public static float MoveTowards(float current, float target, float delta) {
        if (current > target) {
            current -= delta;
            if (current < target) { return target; }
            return current;
        }
        if (current < target) {
            current += delta;
            if (current > target) { return target; }
            return current;
        }
        return current;
    }

    public static Vector2 MoveTowardsVector2(Vector2 current, Vector2 target, float delta) {
        current.X = MoveTowards(current.X, target.X, delta);
        current.Y = MoveTowards(current.Y, current.Y, delta);
        return current;
    }

    public static Vector2 MoveTowardsSmoothVector2(Vector2 current, Vector2 target, float multiplier) {
        Vector2 difference = target - current;
        current += difference * multiplier;
        return current;
    }
}