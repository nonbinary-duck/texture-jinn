using System;

namespace TextureJinn.Rendering.Rasterisation
{
    public class Vector2D : IVector2D<float>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2D()
        {
            X = 0f;
            Y = 0f;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2D d &&
                   X == d.X &&
                   Y == d.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public void Mul(float multiplier)
        {
            X *= multiplier;
            Y *= multiplier;
        }

        public void Mul(IVector2D<float> multiplier)
        {
            X *= multiplier.X;
            Y *= multiplier.Y;
        }
    }
}