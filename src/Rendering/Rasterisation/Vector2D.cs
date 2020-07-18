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
    }
}