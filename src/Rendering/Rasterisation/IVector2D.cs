namespace TextureJinn.Rendering.Rasterisation
{
    public interface IVector2D<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public void Mul(T multiplier);
        public void Mul(IVector2D<T> multiplier);
    }
}