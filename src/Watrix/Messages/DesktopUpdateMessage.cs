using Core.Desktop;

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
        public DesktopUpdateMessage(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
            WithWindow = false;
        }

        public DesktopUpdateMessage(int x, int y, Direction direction, bool withWindow)
        {
            X = x;
            Y = y;
            Direction = direction;
            WithWindow = withWindow;
        }

        public DesktopUpdateMessage(Point point, Direction direction)
        {
            X = point.X;
            Y = point.Y;
            Direction = direction;
            WithWindow = false;
        }

        public DesktopUpdateMessage(Point point, Direction direction, bool withWindow)
        {
            X = point.X;
            Y = point.Y;
            Direction = direction;
            WithWindow = withWindow;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public bool WithWindow { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Direction)}: {Direction}, {nameof(WithWindow)}: {WithWindow}";
        }
    }
}