namespace Box;

using System;

public class Box
{
    public Box(double length, double width, double height)
    {
        if (length <= 0) throw new ArgumentException("Length cannot be zero or negative.");
        if (width <= 0) throw new ArgumentException("Width cannot be zero or negative.");
        if (height <= 0) throw new ArgumentException("Height cannot be zero or negative.");
        
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public double Length { get; }
    public double Width { get; }
    public double Height { get; }

    public double SurfaceArea() => this.LateralSurfaceArea() + 2 * this.Length * this.Width;
    
    public double LateralSurfaceArea() => 2 * (this.Length * this.Height + this.Width * this.Height);

    public double Volume() => this.Length * this.Width * this.Height;
}