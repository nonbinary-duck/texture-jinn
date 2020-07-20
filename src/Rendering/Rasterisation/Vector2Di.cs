using System;


namespace TextureJinn.Rendering.Rasterisation
{
    public class Vector2Di : IVector2D<int>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Di(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2Di(float x, float y)
        {
            X = (int)Math.Floor(x);
            Y = (int)Math.Floor(y);
        }

        public Vector2Di()
        {
            X = 0;
            Y = 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2Di di &&
                   X == di.X &&
                   Y == di.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public void Mul(int multiplier)
        {
            X *= multiplier;
            Y *= multiplier;
        }

        public void Mul(float multiplier)
        {
            X = (int)(multiplier * X);
            Y = (int)(multiplier * Y);
        }

        public void Mul(IVector2D<int> multiplier)
        {
            X *= multiplier.X;
            Y *= multiplier.Y;
        }

        public void Mul(IVector2D<float> multiplier)
        {
            X = (int)(multiplier.X * X);
            Y = (int)(multiplier.Y * Y);
        }
    }
}