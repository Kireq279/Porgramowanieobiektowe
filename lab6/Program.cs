using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}
public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}



public class Program
{
   
    public static void Main()
    
    
    {
        string connectionString =
        "Data Source=10.200.2.28;" + //"(LocalDb)\\MSSQLLocalDB;" - dla lokalnej bazy
        "Initial Catalog=studenci_71467;" + //USTAW SWÓJ NUMER!
       "Integrated Security=True;" +
        "Encrypt=True;" +
        "TrustServerCertificate=True";
        try
        {
            using SqlConnection connection = new
           SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą.");
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc);
        }
        static void ssdane(string connectionString)
        {
            string query = "SELECT Student_Id, Imie, Nazwisko FROM Student";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string imie = reader.GetString(1);
                string nazwisko = reader.GetString(2);

                Console.WriteLine($"{id} {imie} {nazwisko}");
            }
        }
        static void WypiszStudentaPoId(string connectionString, int studentId)
        {
            string query = "SELECT Imie, Nazwisko FROM Student WHERE Student_Id = @id";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", studentId);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string imie = reader.GetString(0);
                string nazwisko = reader.GetString(1);

                Console.WriteLine($"{imie} {nazwisko}");
            }
            else
            {
                Console.WriteLine("Nie znaleziono studenta o podanym ID.");
            }
        }
        static List<Student> PobierzStudentowZOcenami(string connectionString)
        {
            List<Student> studenci = new();

            string query = @"
        SELECT s.Student_Id, s.Imie, s.Nazwisko,
               o.Ocena_Id, o.Wartosc, o.Przedmiot
        FROM Student s
        LEFT JOIN Ocena o ON s.Student_Id = o.Student_Id
        ORDER BY s.Student_Id";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            Dictionary<int, Student> mapaStudentow = new();

            while (reader.Read())
            {
                int studentId = reader.GetInt32(0);

                if (!mapaStudentow.ContainsKey(studentId))
                {
                    mapaStudentow[studentId] = new Student
                    {
                        StudentId = studentId,
                        Imie = reader.GetString(1),
                        Nazwisko = reader.GetString(2)
                    };
                }

                // Jeśli student ma ocenę (LEFT JOIN)
                if (!reader.IsDBNull(3))
                {
                    mapaStudentow[studentId].Oceny.Add(new Ocena
                    {
                        OcenaId = reader.GetInt32(3),
                        Wartosc = reader.GetDouble(4),
                        Przedmiot = reader.GetString(5),
                        StudentId = studentId
                    });
                }
            }

            studenci.AddRange(mapaStudentow.Values);
            return studenci;
        }
        
        static void DodajOcene(string connectionString, Ocena ocena)
        {
            if (!CzyPoprawnaOcena(ocena.Wartosc))
            {
                Console.WriteLine("Błędna wartość oceny!");
                return;
            }

            string query = @"
        INSERT INTO Ocena (Wartosc, Przedmiot, Student_Id)
        VALUES (@wartosc, @przedmiot, @student_Id)";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@wartosc", ocena.Wartosc);
            command.Parameters.AddWithValue("@przedmiot", ocena.Przedmiot);
            command.Parameters.AddWithValue("@student_Id", ocena.StudentId);

            connection.Open();
            command.ExecuteNonQuery();

            Console.WriteLine("Ocena została dodana.");
        }
        static bool CzyPoprawnaOcena(double wartosc)
        {
            return wartosc >= 2.0 && wartosc <= 5.0;
        }

        static void UsunOcenyZGeografii(string connectionString)
        {
            string query = "DELETE FROM Ocena WHERE Przedmiot = 'Geografia'";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            int usuniete = command.ExecuteNonQuery();

            Console.WriteLine($"Usunięto {usuniete} ocen z Geografii.");
        }



        static void AktualizujOcene(string connectionString, int ocena_Id, double nowaWartosc)
        {
            if (!CzyPoprawnaOcena(nowaWartosc))
            {
                Console.WriteLine("Błędna wartość oceny!");
                return;
            }

            string query = @"
        UPDATE Ocena
        SET Wartosc = @wartosc
        WHERE Ocena_Id = @id";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@wartosc", nowaWartosc);
            command.Parameters.AddWithValue("@id", ocena_Id);

            connection.Open();
            int zmienione = command.ExecuteNonQuery();

            if (zmienione == 0)
                Console.WriteLine("Nie znaleziono oceny o podanym ID.");
            else
                Console.WriteLine("Ocena została zaktualizowana.");
        }
        static void DodajStudenta(string connectionString, Student student)
        {
            string query = @"
        INSERT INTO Student (Imie, Nazwisko)
        VALUES (@imie, @nazwisko);
        SELECT SCOPE_IDENTITY();";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@imie", student.Imie);
            command.Parameters.AddWithValue("@nazwisko", student.Nazwisko);

            connection.Open();

            
            int newStudentId = Convert.ToInt32(command.ExecuteScalar());
            student.StudentId = newStudentId;

            Console.WriteLine($"Dodano studenta: {student.Imie} {student.Nazwisko} (ID={newStudentId})");
        }




        ssdane(connectionString);
        WypiszStudentaPoId(connectionString, 3);
        List<Student> studenci = PobierzStudentowZOcenami(connectionString);    
        foreach (var student in studenci)
        {
            Console.WriteLine($"Student: {student.Imie} {student.Nazwisko}");
            foreach (var ocena in student.Oceny)
            {
                Console.WriteLine($"\tOcena: {ocena.Wartosc} z przedmiotu {ocena.Przedmiot}");
            }
        }

        Student nowyStudent = new Student
        {
            Imie = "Jan",
            Nazwisko = "Kowalski"
        };

        DodajStudenta(connectionString, nowyStudent);



        DodajOcene(connectionString, new Ocena
        {
            Wartosc = 4.5,
            Przedmiot = "Matematyka",
            StudentId = 1
        });

        AktualizujOcene(connectionString, 3, 5.0);

      
        UsunOcenyZGeografii(connectionString);
        
        
    


}
}
