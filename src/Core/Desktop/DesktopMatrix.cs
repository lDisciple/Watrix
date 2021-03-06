using System;
using Core.Desktop;

namespace Core.Hotkeys.Desktop
{
    /*
     * Top-left is (0,0)
     */
    public class DesktopMatrix
    {
        public int Rows { get; }
        public int Columns { get; }

        public DesktopMatrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            DesktopManager.MatchCount(Rows*Columns);
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
            p.Y = Math.Min(Rows-1, p.Y + 1);
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
            p.X = Math.Min(Columns-1, p.X + 1);
            GoTo(p);
        }

        public void MoveForegroundWindowUp()
        {
            Point p = GetCurrentPosition();
            p.Y = Math.Max(0, p.Y - 1);
            MoveForegroundWindowTo(p);
        }

        public void MoveForegroundWindowDown()
        {
            Point p = GetCurrentPosition();
            p.Y = Math.Min(Rows-1, p.Y + 1);
            MoveForegroundWindowTo(p);
        }

        public void MoveForegroundWindowLeft()
        {
            Point p = GetCurrentPosition();
            p.X = Math.Max(0, p.X - 1);
            MoveForegroundWindowTo(p);
        }

        public void MoveForegroundWindowRight()
        {
            Point p = GetCurrentPosition();
            p.X = Math.Min(Columns-1, p.X + 1);
            MoveForegroundWindowTo(p);
        }
        
        private void MoveForegroundWindowTo(Point p)
        {
            DesktopManager.MoveForegroundWindowToDesktop(PointToIndex(p));
            this.GoTo(p);
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
            return p.X * Rows + p.Y;
        }

        private Point IndexToPoint(int i)
        {
            return new Point(i / Rows, i % Rows);
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