using System;

public interface IModular
{
    double Modul();
}

public class LiczbaZespolona : ICloneable, IEquatable<LiczbaZespolona>, IModular
{
    private double rzeczywista;
    private double urojona;

    public double Rzeczywista
    {
        get => rzeczywista;
        set => rzeczywista = value;
    }

    public double Urojona
    {
        get => urojona;
        set => urojona = value;
    }

    public LiczbaZespolona(double rzeczywista, double urojona)
    {
        this.rzeczywista = rzeczywista;
        this.urojona = urojona;
    }

    public override string ToString()
    {
        if (urojona >= 0)
            return $"{rzeczywista} + {urojona}i";
        else
            return $"{rzeczywista} - {Math.Abs(urojona)}i";
    }

    public static LiczbaZespolona operator +(LiczbaZespolona a, LiczbaZespolona b)
    {
        return new LiczbaZespolona(a.rzeczywista + b.rzeczywista,
                                   a.urojona + b.urojona);
    }

    public static LiczbaZespolona operator -(LiczbaZespolona a, LiczbaZespolona b)
    {
        return new LiczbaZespolona(a.rzeczywista - b.rzeczywista,
                                   a.urojona - b.urojona);
    }

    public static LiczbaZespolona operator *(LiczbaZespolona a, LiczbaZespolona b)
    {
        double rzeczywistaCzesc = a.rzeczywista * b.rzeczywista - a.urojona * b.urojona;
        double urojonaCzesc = a.rzeczywista * b.urojona + a.urojona * b.rzeczywista;

        return new LiczbaZespolona(rzeczywistaCzesc, urojonaCzesc);
    }

    public object Clone()
    {
        return new LiczbaZespolona(this.rzeczywista, this.urojona);
    }

    public bool Equals(LiczbaZespolona inna)
    {
        if (inna == null) return false;
        return this.rzeczywista == inna.rzeczywista &&
               this.urojona == inna.urojona;
    }

    public override bool Equals(object obj)
    {
        if (obj is LiczbaZespolona z)
            return Equals(z);
        return false;
    }

    public override int GetHashCode()
    {
        return (rzeczywista, urojona).GetHashCode();
    }

    public static bool operator ==(LiczbaZespolona a, LiczbaZespolona b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(LiczbaZespolona a, LiczbaZespolona b)
    {
        return !(a == b);
    }

    public static LiczbaZespolona operator -(LiczbaZespolona a)
    {
        return new LiczbaZespolona(a.rzeczywista, -a.urojona);
    }

    public double Modul()
    {
        return Math.Sqrt(rzeczywista * rzeczywista + urojona * urojona);
    }
}

class Program
{
    public static void Main()
    {
        LiczbaZespolona z1 = new LiczbaZespolona(3, 4);
        LiczbaZespolona z2 = new LiczbaZespolona(2, -5);

        Console.WriteLine($"z1 = {z1}");
        Console.WriteLine($"z2 = {z2}");

        Console.WriteLine($"\nz1 + z2 = {z1 + z2}");
        Console.WriteLine($"z1 - z2 = {z1 - z2}");
        Console.WriteLine($"z1 * z2 = {z1 * z2}");

        LiczbaZespolona kopia = (LiczbaZespolona)z1.Clone();
        Console.WriteLine($"\nKopia z1: {kopia}");

        Console.WriteLine($"\nz1 == kopia ? {z1 == kopia}");
        Console.WriteLine($"z1 != z2 ? {z1 != z2}");

        Console.WriteLine($"\nSprzężenie z1: {-z1}");

        Console.WriteLine($"\nModuł z1 = {z1.Modul()}");
        Console.WriteLine($"Moduł z2 = {z2.Modul()}");
    }
}
