namespace CSharp
{
    public readonly struct Renderable
    {
        public char Glyph { get; }
        public Renderable(char glyph)
        {
            Glyph = glyph;
        }
    }
}