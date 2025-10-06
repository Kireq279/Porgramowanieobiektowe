
class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest cwiczenie 1");

    }
}

class zwierze
{
    public string imie;
    public string gatunek;
    public int ilenog;
    public static int liczbaZwierzat = 0;

    public string getnazwa()
    {
        return imie;
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
    public void setnazwa(string imie)
    {
        this.imie = imie;
    }
    public void setgatunek(string gatunek)
    {
        this.gatunek = gatunek;
    }
    public void setilenog(int ilenog)
    {
        this.ilenog = ilenog;
    }
    public zwierze()
    {
        imie = "Rex";
        gatunek = "Pies";
        ilenog = 0;
        liczbaZwierzat++;
    }
    public zwierze(string imie, string gatunek, int ilenog)
    {
        this.imie = imie;
        this.gatunek = gatunek;
        this.ilenog = ilenog;
        liczbaZwierzat++;
    }
}