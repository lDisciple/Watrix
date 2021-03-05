using Core.Hotkeys.Desktop;

namespace Watrix.Messages
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    public class DesktopUpdateMessage
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public DesktopUpdateMessage(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public DesktopUpdateMessage(Point point, Direction direction)
        {
            X = point.X;
            Y = point.Y;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Direction)}: {Direction}";
        }
    }
}