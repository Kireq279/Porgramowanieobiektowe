using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using System.Linq;

public class Student
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public List<int> Oceny { get; set; }
}

class Program
{
    static void Main()
    {
        //Uruchamiaj dowolne funkcje testowo:
        WriteUserInputToFile();
        ReadFileLineByLine();
        AppendUserInputToFile();
        SaveStudentsToJson();
        ReadStudentsFromJson();
        SaveStudentsToXml();
        ReadStudentsFromXml();
        ReadCsvFile();
        ReadCsvAndCalculateAverages();
        FilterIrisCsv();
    }

    // -------------------------------
    // ZADANIE 2
    // -------------------------------
    static void WriteUserInputToFile()
    {
        string filePath = "dane.txt";
        Console.WriteLine("Podaj po kolei teksty (wpisz 'STOP' aby zakoñczyæ):");

        using StreamWriter sw = new StreamWriter(filePath);

        while (true)
        {
            string input = Console.ReadLine();
            if (input.ToUpper() == "STOP")
                break;

            sw.WriteLine(input);
        }

        Console.WriteLine("Zapisano do pliku dane.txt");
    }

    // -------------------------------
    // ZADANIE 3
    // -------------------------------
    static void ReadFileLineByLine()
    {
        string filePath = "dane.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Plik nie istnieje!");
            return;
        }

        foreach (string line in File.ReadLines(filePath))
            Console.WriteLine(line);
    }

    // -------------------------------
    // ZADANIE 4
    // -------------------------------
    static void AppendUserInputToFile()
    {
        string filePath = "dane.txt";
        Console.WriteLine("Podaj tekst do dopisania (STOP aby zakoñczyæ):");

        using StreamWriter sw = new StreamWriter(filePath, append: true);

        while (true)
        {
            string input = Console.ReadLine();
            if (input.ToUpper() == "STOP")
                break;

            sw.WriteLine(input);
        }

        Console.WriteLine("Dopisano dane do pliku.");
    }

    // -------------------------------
    // ZADANIE 6 — JSON zapis
    // -------------------------------
    static void SaveStudentsToJson()
    {
        var students = new List<Student>
        {
            new Student { Imie = "Jan", Nazwisko = "Kowalski", Oceny = new List<int>{5,4,5}},
            new Student { Imie = "Anna", Nazwisko = "Nowak", Oceny = new List<int>{3,4,4}},
            new Student { Imie = "Piotr", Nazwisko = "Zielinski", Oceny = new List<int>{5,5,5}}
        };

        string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText("students.json", json);

        Console.WriteLine("Zapisano students.json");
    }

    // -------------------------------
    // ZADANIE 7 — JSON odczyt
    // -------------------------------
    static void ReadStudentsFromJson()
    {
        string filePath = "students.json";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Brak pliku JSON!");
            return;
        }

        string json = File.ReadAllText(filePath);

        List<Student> students = JsonSerializer.Deserialize<List<Student>>(json);

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Imie} {s.Nazwisko} — Oceny: {string.Join(",", s.Oceny)}");
        }
    }

    // -------------------------------
    // ZADANIE 8 — XML zapis
    // -------------------------------
    static void SaveStudentsToXml()
    {
        var students = new List<Student>
        {
            new Student { Imie = "Jan", Nazwisko = "Kowalski", Oceny = new List<int>{5,4,3}},
            new Student { Imie = "Ewa", Nazwisko = "Maj", Oceny = new List<int>{4,3,5}}
        };

        XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

        using FileStream fs = new FileStream("students.xml", FileMode.Create);
        serializer.Serialize(fs, students);

        Console.WriteLine("Zapisano students.xml");
    }

    // -------------------------------
    // ZADANIE 9 — XML odczyt
    // -------------------------------
    static void ReadStudentsFromXml()
    {
        string filePath = "students.xml";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Brak pliku XML!");
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

        using FileStream fs = new FileStream(filePath, FileMode.Open);

        var students = (List<Student>)serializer.Deserialize(fs);

        foreach (var s in students)
            Console.WriteLine($"{s.Imie} {s.Nazwisko} — Oceny: {string.Join(",", s.Oceny)}");
    }

    // -------------------------------
    // ZADANIE 10 — CSV odczyt
    // -------------------------------
    static void ReadCsvFile()
    {
        string path = "iris.csv";

        if (!File.Exists(path))
        {
            Console.WriteLine("Brak iris.csv!");
            return;
        }

        foreach (string line in File.ReadLines(path))
            Console.WriteLine(line);
    }

    // -------------------------------
    // ZADANIE 11 — CSV + œrednie wartoœci
    // -------------------------------
    static void ReadCsvAndCalculateAverages()
    {
        string path = "iris.csv";

        if (!File.Exists(path))
        {
            Console.WriteLine("Brak iris.csv!");
            return;
        }

        var lines = File.ReadAllLines(path);
        var header = lines[0].Split(',');

        int colCount = header.Length;

        double[] sums = new double[colCount];
        int rowCount = 0;

        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            rowCount++;

            for (int j = 0; j < colCount; j++)
            {
                if (double.TryParse(parts[j], out double val))
                    sums[j] += val;
            }
        }

        Console.WriteLine("ŒREDNIE KOLUMN:");
        for (int i = 0; i < colCount; i++)
        {
            if (double.TryParse(lines[1].Split(',')[i], out _))
                Console.WriteLine($"{header[i]} = {sums[i] / rowCount}");
        }
    }

    // -------------------------------
    // ZADANIE 12 — filtrowanie CSV
    // -------------------------------
    static void FilterIrisCsv()
    {
        string path = "iris.csv";

        if (!File.Exists(path))
        {
            Console.WriteLine("Brak iris.csv!");
            return;
        }

        var lines = File.ReadAllLines(path).ToList();
        var header = lines[0].Split(',');

        int colSepLen = header.ToList().IndexOf("sepal length");
        int colSepWid = header.ToList().IndexOf("sepal width");
        int colClass = header.ToList().IndexOf("class");

        var filtered = new List<string>
        {
            "sepal length,sepal width,class"
        };

        for (int i = 1; i < lines.Count; i++)
        {
            var parts = lines[i].Split(',');

            double sepalLength = double.Parse(parts[colSepLen]);

            if (sepalLength < 5)
            {
                filtered.Add($"{parts[colSepLen]},{parts[colSepWid]},{parts[colClass]}");
            }
        }

        File.WriteAllLines("iris_filtered.csv", filtered);

        Console.WriteLine("Zapisano iris_filtered.csv");
    }
}
