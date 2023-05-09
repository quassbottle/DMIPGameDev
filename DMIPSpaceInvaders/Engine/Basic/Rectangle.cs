namespace DMIPSpaceInvaders.Engine.Basic
{
    public class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Point2D Location { get; set; }

        public int X
        {
            get => Location.X;
            set => Location.X = value;
        }
        
        public int Y
        {
            get => Location.Y;
            set => Location.Y = value;
        }
        
        public int Top => Location.Y;

        public int Bottom => Location.Y + Height;

        public int Left => Location.X;

        public int Right => Location.X + Width;

        public Rectangle(int width, int height, Point2D location)
        {
            this.Width = width;
            this.Height = height;
            this.Location = location;
        }

        public Rectangle(int width, int height, int x, int y) : this(width, height, new Point2D() {X = x, Y = y})
        {
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return rect.X < this.Right && this.X < rect.Right && rect.Y < this.Bottom && this.Y < rect.Bottom;
        }
    }
}