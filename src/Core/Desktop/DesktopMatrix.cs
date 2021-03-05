using System;
using Core.Desktop;

namespace Core.Hotkeys.Desktop
{
    /*
     * Top-left is (0,0)
     */
    public class DesktopMatrix
    {
        private readonly int _rows;
        private readonly int _columns;

        public DesktopMatrix(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            DesktopManager.MatchCount(_rows*_columns);
        }

        public void GoTo(Point p)
        {
            DesktopManager.GoTo(PointToIndex(p));
        }

        public void MoveUp()
        {
            Point p = GetCurrentPosition();
            p.Y = Math.Max(0, p.Y - 1);
            GoTo(p);
        }

        public void MoveDown()
        {
            Point p = GetCurrentPosition();
            p.Y = Math.Min(_rows-1, p.Y + 1);
            GoTo(p);
        }

        public void MoveLeft()
        {
            Point p = GetCurrentPosition();
            p.X = Math.Max(0, p.X - 1);
            GoTo(p);
        }

        public void MoveRight()
        {
            Point p = GetCurrentPosition();
            p.X = Math.Min(_columns-1, p.X + 1);
            GoTo(p);
        }

        public Point GetCurrentPosition()
        {
            return IndexToPoint(DesktopManager.CurrentDesktop());
        }

        public void PinWindow(IntPtr handle)
        {
            DesktopManager.PinWindow(handle);
        }
        
        public void UnpinWindow(IntPtr handle)
        {
            DesktopManager.UnpinWindow(handle);
        }

        private int PointToIndex(Point p)
        {
            return p.X * _rows + p.Y;
        }

        private Point IndexToPoint(int i)
        {
            return new Point(i / _rows, i % _rows);
        }
    }

    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }
}