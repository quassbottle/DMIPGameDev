using DMIPSpaceInvaders.Engine.Basic;

namespace DMIPSpaceInvaders.Engine.Utils
{
    public class ScreenBufferInfo
    {
        public Point2D CursorPosition { get; set; }
        public short? Attributes { get; set; }
        public Rectangle Window { get; set; }
        public Point2D MaximumWindowSize { get; set; }
    }
}