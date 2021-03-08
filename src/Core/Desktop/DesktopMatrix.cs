using System;

namespace Core.Desktop
{
    /// <summary>
    ///     Creates and manages a virtual desktop matrix.
    /// </summary>
    public class DesktopMatrix
    {
        /// <summary>
        ///     Create a desktop matrix by defining the size of the matrix and creating/removing any necessary desktops.
        /// </summary>
        /// <param name="rows">The height of the matrix.</param>
        /// <param name="columns">The width of the matrix.</param>
        public DesktopMatrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            DesktopManager.MatchCount(Rows * Columns);
        }

        public int Rows { get; }
        public int Columns { get; }
        private IntPtr CapturedView { get; set; }

        /// <summary>
        ///     Get the current desktop's position in the matrix/grid.
        /// </summary>
        /// <returns>The point corresponding to the current desktop.</returns>
        public Point GetCurrentPosition()
        {
            return IndexToPoint(DesktopManager.CurrentDesktop());
        }

        /// <summary>
        ///     Pin the given window to all desktops.
        /// </summary>
        /// <param name="handle">The handle of the window to pin.</param>
        public void PinWindow(IntPtr handle)
        {
            DesktopManager.PinWindow(handle);
        }

        /// <summary>
        ///     Unpin the given window from all desktops.
        /// </summary>
        /// <param name="handle">The handle of the window to unpin.</param>
        public void UnpinWindow(IntPtr handle)
        {
            DesktopManager.UnpinWindow(handle);
        }

        /// <summary>
        ///     Converts a point to a flat index.
        /// </summary>
        /// <param name="point">A 2D point.</param>
        /// <returns>An index to an array.</returns>
        private int PointToIndex(Point point)
        {
            return point.X * Rows + point.Y;
        }

        /// <summary>
        ///     Converts a flat index to a point in the matrix/grid.
        /// </summary>
        /// <param name="i">An index to an array.</param>
        /// <returns>A 2D point.</returns>
        private Point IndexToPoint(int i)
        {
            return new(i / Rows, i % Rows);
        }

        #region BasicMovement

        /// <summary>
        ///     Moves to the given desktop.
        /// </summary>
        /// <param name="point">The point in the grid to go to.</param>
        public void GoTo(Point point)
        {
            DesktopManager.GoTo(PointToIndex(point));
        }

        /// <summary>
        ///     Moves to the desktop above the current one.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveUp()
        {
            var p = GetCurrentPosition();
            p.Y = Math.Max(0, p.Y - 1);
            GoTo(p);
        }

        /// <summary>
        ///     Moves to the desktop below the current one.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveDown()
        {
            var p = GetCurrentPosition();
            p.Y = Math.Min(Rows - 1, p.Y + 1);
            GoTo(p);
        }

        /// <summary>
        ///     Moves to the desktop to the left of the current one.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveLeft()
        {
            var p = GetCurrentPosition();
            p.X = Math.Max(0, p.X - 1);
            GoTo(p);
        }

        /// <summary>
        ///     Moves to the desktop to right of the current one.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveRight()
        {
            var p = GetCurrentPosition();
            p.X = Math.Min(Columns - 1, p.X + 1);
            GoTo(p);
        }

        #endregion

        #region CapturedWindow

        /// <summary>
        ///     Moves to the desktop above the current one with the captured window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveCapturedWindowUp()
        {
            var p = GetCurrentPosition();
            p.Y = Math.Max(0, p.Y - 1);
            MoveCapturedWindowTo(p);
        }

        /// <summary>
        ///     Moves to the desktop below the current one with the captured window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveCapturedWindowDown()
        {
            var p = GetCurrentPosition();
            p.Y = Math.Min(Rows - 1, p.Y + 1);
            MoveCapturedWindowTo(p);
        }

        /// <summary>
        ///     Moves to the desktop to the left of the current one with the captured window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveCapturedWindowLeft()
        {
            var p = GetCurrentPosition();
            p.X = Math.Max(0, p.X - 1);
            MoveCapturedWindowTo(p);
        }

        /// <summary>
        ///     Moves to the desktop to the right of the current one with the captured window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveCapturedWindowRight()
        {
            var p = GetCurrentPosition();
            p.X = Math.Min(Columns - 1, p.X + 1);
            MoveCapturedWindowTo(p);
        }

        /// <summary>
        ///     Moves to the given desktop with the captured window.
        /// </summary>
        /// <param name="point">The point in the grid to go to.</param>
        private void MoveCapturedWindowTo(Point point)
        {
            DesktopManager.MoveViewToDesktop(CapturedView, PointToIndex(point));
            GoTo(point);
        }

        /// <summary>
        ///     Captures/stores the current foreground window's reference for future use.
        /// </summary>
        public void CaptureForegroundWindow()
        {
            CapturedView = DesktopManager.GetForegroundView();
        }

        #endregion

        #region ForegroundWindows

        /// <summary>
        ///     Moves to the desktop above the current one with the current foreground window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveForegroundWindowUp()
        {
            var p = GetCurrentPosition();
            p.Y = Math.Max(0, p.Y - 1);
            MoveForegroundWindowTo(p);
        }

        /// <summary>
        ///     Moves to the desktop below the current one with the current foreground window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveForegroundWindowDown()
        {
            var p = GetCurrentPosition();
            p.Y = Math.Min(Rows - 1, p.Y + 1);
            MoveForegroundWindowTo(p);
        }

        /// <summary>
        ///     Moves to the desktop to the left of the current one with the current foreground window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveForegroundWindowLeft()
        {
            var p = GetCurrentPosition();
            p.X = Math.Max(0, p.X - 1);
            MoveForegroundWindowTo(p);
        }

        /// <summary>
        ///     Moves to the desktop to the right of the current one with the current foreground window.
        ///     If that desktop does not exist then the current desktop remains active.
        /// </summary>
        public void MoveForegroundWindowRight()
        {
            var p = GetCurrentPosition();
            p.X = Math.Min(Columns - 1, p.X + 1);
            MoveForegroundWindowTo(p);
        }

        /// <summary>
        ///     Moves to the given desktop with the current foreground window.
        /// </summary>
        /// <param name="point">The point in the grid to go to.</param>
        private void MoveForegroundWindowTo(Point point)
        {
            DesktopManager.MoveForegroundWindowToDesktop(PointToIndex(point));
            GoTo(point);
        }

        #endregion
    }

    /// <summary>
    ///     A point in 2D space.
    /// </summary>
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }
}