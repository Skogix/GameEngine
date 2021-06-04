using System.Dynamic;
using Engine;

namespace CSharp
{
    public enum InputType
    {
        Up,
        Down,
        Left,
        Right,
    }
    public struct PrintGlyph : iOutput
    {
        public Position Position;
        public Renderable Renderable;
        public PrintGlyph(Position position, Renderable renderable)
        {
            Position = position;
            Renderable = renderable;
        }

        public void Handler(iOutput output)
        {
        }
    }

    public interface iOutput
    {
        void Handler(iOutput output);
    }
    public interface iOutputChannel
    {
        
    }
    public interface iInputChannel
    {
        InputType Get();
    }
    public class Init
    {
        private World.World _world = API.engineWorld;
        public Init(iInputChannel inputHandler, iOutputChannel outputHandler) 
        {
            
        }
    }
}