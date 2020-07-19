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
    }
}