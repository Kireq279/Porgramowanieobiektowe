using System;

class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest cwiczenie 1");

        zwierze[] tab = new zwierze[3];

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"\nPodaj informacje o zwierzeciu {i + 1}:");

            Console.Write("Imie: ");
            string imie = Console.ReadLine();

            Console.Write("Gatunek: ");
            string gatunek = Console.ReadLine();

            Console.Write("Liczba nog: ");
            int nogi = int.Parse(Console.ReadLine());

            tab[i] = new zwierze(imie, gatunek, nogi);
        }

        zwierze klon = new zwierze(tab[1]);
        klon.Nazwa = "KlonowaneZwierze";

        Console.WriteLine("\n--- Dane wszystkich zwierząt ---");

        foreach (zwierze z in tab)
        {
            Wypisz(z);
        }

        Console.WriteLine("\n--- Dane klona ---");
        Wypisz(klon);

        Console.WriteLine($"\nLiczba stworzonych zwierząt: {zwierze.getliczbaZwierzat()}");
    }

    public static void Wypisz(zwierze z)
    {
        Console.WriteLine($"Imie: {z.Nazwa}, Gatunek: {z.getgatunek()}, Nogi: {z.getilenog()}");
        z.daj_glos();
    }
}

class zwierze
{
    private string imie;
    private string gatunek;
    private int ilenog;

    private static int liczbaZwierzat = 0;

    public string Nazwa
    {
        get { return imie; }
        set { imie = value; }
    }

    public string getgatunek()
    {
        return gatunek;
    }

    public int getilenog()
    {
        return ilenog;
    }

    public static int getliczbaZwierzat()
    {
        return liczbaZwierzat;
    }

    public zwierze()
    {
        imie = "Rex";
        gatunek = "Pies";
        ilenog = 4;
        liczbaZwierzat++;
    }

    public zwierze(string imie, string gatunek, int ilenog)
    {
        this.imie = imie;
        this.gatunek = gatunek;
        this.ilenog = ilenog;
        liczbaZwierzat++;
    }

    public zwierze(zwierze inne)
    {
        this.imie = inne.imie;
        this.gatunek = inne.gatunek;
        this.ilenog = inne.ilenog;
        liczbaZwierzat++;
    }

    public void daj_glos()
    {
        switch (gatunek.ToLower())
        {
            case "kot":
                Console.WriteLine("Miau!");
                break;
            case "pies":
                Console.WriteLine("Hau hau!");
                break;
            case "krowa":
                Console.WriteLine("Muuu!");
                break;
            default:
                Console.WriteLine("Brak przypisanego dźwięku.");
                break;
        }
    }

    ~zwierze()
    {
    }
}
