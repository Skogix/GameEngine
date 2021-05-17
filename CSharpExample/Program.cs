using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Engine;
using static Engine.cAPI.Engine;

namespace CSharpExample
{
    interface IRunSystem { void Run(cAPI.Engine engine); }
    interface IListenSystem { void Init(cAPI.Engine engine); }
    
    /// <summary>
    /// En entity är bara ett id som components kopplas till
    /// typ en container for olika data/components
    /// </summary>
    public record Entity { public int Id; }
    /// 
    /// En component är bara dum readonly-data som fästs till en entity
    ///
    public record PositionComponent
    {
        public int X;
        public int Y;
    }
    /// 
    /// Events är också dum readonly-data som skickas som events
    /// 
    public record KeyPressedEvent
    {
        public ConsoleKey Key; // key som pressades
        public Engine.Domain.Entity Entity; // vilken entity som "pressade" keyn
    }
    public record MovedEvent
    {
        public Domain.Entity Entity; // vilken entity som moveade
        public PositionComponent NewPosition; // positionen den moveade till
    }
    /// 
    /// Systems är logik som drivs av t.ex
    ///     Run - Kors varje update och använder filter<ComponentType1, ComponentType2> for att
    ///         hämta entities som har alla cType1 och cType2
    ///             t.ex: RenderSystem hämtar alla som har Position och Graphic for att rendera
    ///                   GetInput hämtar entities som har typ Moveable, Player
    ///     OnEvent/Listen - Som ett vanligt event, Engine.Listen<EventType>(Action/Func som utfors onEvent)
    ///             t.ex: OnAttackedEvent och WeaponEntity har IceComponent så gor extra skada beronde på defenders resist
    ///
    
    /// <summary>
    /// Hämtar input från user for alla entities som har en positionComponent
    /// om jag hade orkat skriva mer kod så hade man kollat efter positionComponent och playerComponent
    /// sen skickas ett keyPressedEvent med vad som pressades och vilken entity som "pressade"
    /// </summary>
    public class GetInputSystem : IRunSystem 
    {
        public void Run(cAPI.Engine engine)
        {
            foreach (var (pos, ent) in Filter1<PositionComponent>())
            {
                var keyPressed = Console.ReadKey(true).Key;
                Post(new KeyPressedEvent() {Key = keyPressed, Entity = ent});
            }
        }
    }
    /// <summary>
    /// Lyssnar på keyPressedEvent
    /// Får en key och matchar den med arrowkeys, säger åt engine att uppdatera entityns position
    /// och skickar ett move-event
    /// </summary>
    public class OnKeyPressedEvent : IListenSystem
    {
        private Action<KeyPressedEvent> handler = e =>
        {
            var currentPos = cAPI.Engine.Get<PositionComponent>(e.Entity);
            PositionComponent newPos = e.Key switch
            {
                ConsoleKey.UpArrow => new PositionComponent(){X = currentPos.X, Y = currentPos.Y - 1},
                ConsoleKey.DownArrow => new PositionComponent(){X = currentPos.X, Y = currentPos.Y + 1},
                ConsoleKey.LeftArrow => new PositionComponent(){X = currentPos.X - 1, Y = currentPos.Y},
                ConsoleKey.RightArrow => new PositionComponent(){X = currentPos.X + 1, Y = currentPos.Y},
                _ => null
            };
            cAPI.Engine.SetComponent(newPos, e.Entity);
            cAPI.Engine.Post(new MovedEvent({Entity = e.Entity, NewPosition = newPos});
        };
        public void Init(cAPI.Engine engine) => Listen<KeyPressedEvent>(handler);
    }
    
    internal class Program
    {
        private static void Main(string[] args)
        {
            var engine = new cAPI.Engine(); // skapa engine-object pga c#
            engine.AddSystem(new GetInputSystem()); // lägg till run-event
            var player = engine.CreateEntity(); // skapa en ny entity
            player.AddComponent(new PositionComponent() {X = 4, Y = 4}); // lägg till component till entity
            engine.Run(); // kor alla runsystems och lite annat krafs
        }
    }
}