using System;
namespace LAb2

{



    class Program
    {
        static void Main(string[] args)
        {
            Zwierze pies = new Pies("Burek");
            Zwierze kot = new Kot("Mruczek");
            Zwierze waz = new Wąż("Kaa");

            Powiedzcos(kot);
            Powiedzcos(pies);
            Powiedzcos(waz);

            Pracownik piekarz = new Piekarz();
            piekarz.Pracuj();

            A obkietA = new A();
            B obkietB = new B();
            C obkietC = new C();


        }
        static void Powiedzcos(Zwierze z)
        {
            z.daj_głos();
            Console.WriteLine(z.GetType());
        }
    }
}
class Zwierze
{
    protected string nazwa;

    public Zwierze(string nazwa)
    {
        this.nazwa = nazwa;
    }
    public virtual void daj_głos()
    {
        Console.WriteLine("...");
    }
}
class Pies : Zwierze
{
    public Pies(string nazwa) : base(nazwa) { }
    public override void daj_głos()
    {
        Console.WriteLine($"{nazwa} ; Hau Hau");
    }
}
class Kot : Zwierze
{
    public Kot(string nazwa) : base(nazwa) { }
    public override void daj_głos()
    {
        Console.WriteLine($"{nazwa} ; Miau Miau");
    }
}
class Wąż : Zwierze
{
    public Wąż(string nazwa) : base(nazwa) { }
    public override void daj_głos()
    {
        Console.WriteLine($"{nazwa} ; Sssss Sssss");
    }
}
abstract class Pracownik
{
    public abstract void Pracuj();
}
class Piekarz : Pracownik
{
    public override void Pracuj()
    {
        Console.WriteLine("Trwa pieczenie...");
    }
}
class A
{
    public A()
    {
        Console.WriteLine("To jest konstruktor A");
    }
}
class B : A
{
    public B() : base()
    {
        Console.WriteLine("To jest konstruktor B");
    }
}
class C : B
{
    public C() : base()
    {
        Console.WriteLine("To jest konstruktor C");
    }
}