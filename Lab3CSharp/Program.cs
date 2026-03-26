using System;
using System.Linq;

public class Point
{
    protected int x, y;
    protected int color;

    public Point()
    {
        x = 0;
        y = 0;
        color = 0;
    }

    public Point(int x, int y, int color)
    {
        this.x = x;
        this.y = y;
        this.color = color;
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int Color
    {
        get { return color; }
    }

    public void Print()
    {
        Console.WriteLine($"  Точка: ({x}, {y}), колір: {color}");
    }

    public double Distance()
    {
        return Math.Sqrt(x * x + y * y);
    }

    public void Move(int x1, int y1)
    {
        x += x1;
        y += y1;
    }
}

public class Person
{
    protected string name;
    protected int age;

    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    public string Name { get { return name; } set { name = value; } }
    public int Age    { get { return age; }  set { age = value; } }

    public virtual void Show()
    {
        Console.WriteLine($"  Персона: {name}, вік: {age}");
    }

    public virtual int GetTypeOrder() => 0;
}

public class Student : Person
{
    private string group;
    private double gpa;

    public Student(string name, int age, string group, double gpa)
        : base(name, age)
    {
        this.group = group;
        this.gpa = gpa;
    }

    public string Group { get { return group; } set { group = value; } }
    public double GPA   { get { return gpa; }   set { gpa = value; } }

    public override void Show()
    {
        Console.WriteLine($"  Студент: {name}, вік: {age}, група: {group}, середній бал: {gpa:f2}");
    }

    public override int GetTypeOrder() => 1;
}

public class Teacher : Person
{
    private string subject;
    private int experience;

    public Teacher(string name, int age, string subject, int experience)
        : base(name, age)
    {
        this.subject = subject;
        this.experience = experience;
    }

    public string Subject  { get { return subject; }    set { subject = value; } }
    public int Experience  { get { return experience; } set { experience = value; } }

    public override void Show()
    {
        Console.WriteLine($"  Викладач: {name}, вік: {age}, предмет: {subject}, стаж: {experience} р.");
    }

    public override int GetTypeOrder() => 2;
}

public class DepartmentHead : Teacher
{
    private string department;

    public DepartmentHead(string name, int age, string subject, int experience, string department)
        : base(name, age, subject, experience)
    {
        this.department = department;
    }

    public string Department { get { return department; } set { department = value; } }

    public override void Show()
    {
        Console.WriteLine($"  Завідувач кафедри: {name}, вік: {age}, кафедра: {department}, предмет: {Subject}, стаж: {Experience} р.");
    }

    public override int GetTypeOrder() => 3;
}

public class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання (1 або 2):");
        Console.WriteLine("1 - Клас Point (масив точок)");
        Console.WriteLine("2 - Ієрархія класів (Персона, Студент, Викладач, Завкафедри)");

        Console.Write("\nВаш вибір: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1) Task1();
        else if (choice == 2) Task2();
        else Console.WriteLine("Невірний вибір!");
    }

    static void Task1()
    {
        Console.WriteLine("\n--- Завдання 1.1: Масив точок ---");

        Console.Write("Кількість точок: ");
        int n = int.Parse(Console.ReadLine());

        Point[] points = new Point[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nТочка {i + 1}:");
            Console.Write("  x = "); int x = int.Parse(Console.ReadLine());
            Console.Write("  y = "); int y = int.Parse(Console.ReadLine());
            Console.Write("  колір (число) = "); int c = int.Parse(Console.ReadLine());
            points[i] = new Point(x, y, c);
        }

        double totalDist = 0;
        for (int i = 0; i < n; i++)
            totalDist += points[i].Distance();
        double avgDist = totalDist / n;

        Console.WriteLine($"\nСередня відстань до центру: {avgDist:f2}");

        Console.WriteLine("\n=== Сортування за кольорами ===");
        var sortedByColor = points.OrderBy(p => p.Color).ToArray();
        foreach (var p in sortedByColor)
        {
            p.Print();
            Console.WriteLine($"  Відстань: {p.Distance():f2}");
        }

        Console.WriteLine("\n=== Сортування за відстанню ===");
        var sortedByDistance = points.OrderBy(p => p.Distance()).ToArray();
        foreach (var p in sortedByDistance)
        {
            p.Print();
            Console.WriteLine($"  Відстань: {p.Distance():f2}");
        }

        Console.Write("\nВведіть вектор переміщення x1: ");
        int vx = int.Parse(Console.ReadLine());
        Console.Write("Введіть вектор переміщення y1: ");
        int vy = int.Parse(Console.ReadLine());

        Console.WriteLine("\n=== Переміщення точок (далі середньої відстані) ===");
        for (int i = 0; i < n; i++)
        {
            if (points[i].Distance() > avgDist)
            {
                Console.Write($"Точка ({points[i].X}, {points[i].Y}) переміщена на ({vx}, {vy}) -> ");
                points[i].Move(vx, vy);
                points[i].Print();
            }
        }
    }

    static void Task2()
    {
        Console.WriteLine("\n--- Завдання 2.1: Ієрархія класів ---");

        Person[] people = new Person[]
        {
            new Person("Іваненко Іван", 45),
            new Student("Петренко Петро", 20, "КН-31", 4.5),
            new Student("Сидоренко Оксана", 19, "КН-31", 3.8),
            new Teacher("Коваленко Микола", 50, "C#", 25),
            new Teacher("Бондаренко Ірина", 40, "Математика", 15),
            new DepartmentHead("Шевченко Василь", 55, "ООП", 30, "Кафедра інформатики")
        };

        Console.WriteLine("\n=== Всі об'єкти (без сортування) ===");
        foreach (Person p in people)
            p.Show();

        Console.WriteLine("\n=== Сортування за типами об'єктів ===");
        var sortedByType = people.OrderBy(p => p.GetTypeOrder()).ThenBy(p => p.Name).ToArray();
        foreach (Person p in sortedByType)
            p.Show();

        Console.WriteLine("\n=== Сортування за віком (додатково) ===");
        var sortedByAge = people.OrderBy(p => p.Age).ToArray();
        foreach (Person p in sortedByAge)
            p.Show();
    }
}
