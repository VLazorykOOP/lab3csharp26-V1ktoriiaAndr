using Xunit;
using System;
using System.IO;
using System.Linq;

public class PointTests
{
    #region Конструктори

    [Fact]
    public void DefaultConstructor_SetsZeroValues()
    {
        var point = new Point();
        
        Assert.Equal(0, point.X);
        Assert.Equal(0, point.Y);
        Assert.Equal(0, point.Color);
    }

    [Fact]
    public void ParameterizedConstructor_SetsCorrectValues()
    {
        var point = new Point(5, -3, 7);
        
        Assert.Equal(5, point.X);
        Assert.Equal(-3, point.Y);
        Assert.Equal(7, point.Color);
    }

    #endregion

    #region Властивості

    [Fact]
    public void X_Property_GetAndSet_WorksCorrectly()
    {
        var point = new Point();
        
        point.X = 10;
        
        Assert.Equal(10, point.X);
    }

    [Fact]
    public void Y_Property_GetAndSet_WorksCorrectly()
    {
        var point = new Point();
        
        point.Y = -15;
        
        Assert.Equal(-15, point.Y);
    }

    [Fact]
    public void Color_Property_ReadOnly_ReturnsCorrectValue()
    {
        var point = new Point(0, 0, 42);
        
        Assert.Equal(42, point.Color);
    }

    #endregion

    #region Метод Distance

    [Fact]
    public void Distance_Origin_ReturnsZero()
    {
        var point = new Point(0, 0, 0);
        
        double distance = point.Distance();
        
        Assert.Equal(0, distance, 5);
    }

    [Fact]
    public void Distance_Point3_4_Returns5()
    {
        var point = new Point(3, 4, 0);
        
        double distance = point.Distance();
        
        Assert.Equal(5.0, distance, 5);
    }

    [Fact]
    public void Distance_NegativeCoordinates_ReturnsCorrectDistance()
    {
        var point = new Point(-3, -4, 0);
        
        double distance = point.Distance();
        
        Assert.Equal(5.0, distance, 5);
    }

    [Fact]
    public void Distance_Point1_1_ReturnsSqrt2()
    {
        var point = new Point(1, 1, 0);
        
        double distance = point.Distance();
        
        Assert.Equal(Math.Sqrt(2), distance, 5);
    }

    #endregion

    #region Метод Move

    [Fact]
    public void Move_PositiveVector_UpdatesCoordinates()
    {
        var point = new Point(2, 3, 0);
        
        point.Move(5, 7);
        
        Assert.Equal(7, point.X);
        Assert.Equal(10, point.Y);
    }

    [Fact]
    public void Move_NegativeVector_UpdatesCoordinates()
    {
        var point = new Point(10, 10, 0);
        
        point.Move(-3, -4);
        
        Assert.Equal(7, point.X);
        Assert.Equal(6, point.Y);
    }

    [Fact]
    public void Move_ZeroVector_KeepsCoordinates()
    {
        var point = new Point(5, 5, 0);
        
        point.Move(0, 0);
        
        Assert.Equal(5, point.X);
        Assert.Equal(5, point.Y);
    }

    #endregion

    #region Сортування

    [Fact]
    public void SortByColor_Ascending_WorksCorrectly()
    {
        var points = new Point[]
        {
            new Point(0, 0, 5),
            new Point(0, 0, 1),
            new Point(0, 0, 3),
            new Point(0, 0, 2)
        };

        var sorted = points.OrderBy(p => p.Color).ToArray();

        Assert.Equal(1, sorted[0].Color);
        Assert.Equal(2, sorted[1].Color);
        Assert.Equal(3, sorted[2].Color);
        Assert.Equal(5, sorted[3].Color);
    }

    [Fact]
    public void SortByDistance_Ascending_WorksCorrectly()
    {
        var points = new Point[]
        {
            new Point(3, 4, 0),   // distance = 5
            new Point(0, 0, 0),   // distance = 0
            new Point(1, 0, 0),   // distance = 1
            new Point(6, 8, 0)    // distance = 10
        };

        var sorted = points.OrderBy(p => p.Distance()).ToArray();

        Assert.Equal(0, sorted[0].Distance(), 5);
        Assert.Equal(1, sorted[1].Distance(), 5);
        Assert.Equal(5, sorted[2].Distance(), 5);
        Assert.Equal(10, sorted[3].Distance(), 5);
    }

    #endregion

    #region Переміщення точок далі середньої відстані

    [Fact]
    public void MovePointsBeyondAverageDistance_WorksCorrectly()
    {
        var points = new Point[]
        {
            new Point(0, 0, 0),     // distance = 0
            new Point(3, 4, 0),     // distance = 5
            new Point(6, 8, 0)      // distance = 10
        };

        double avgDist = (0 + 5 + 10) / 3.0; // 5.0
        
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].Distance() > avgDist)
                points[i].Move(1, 1);
        }

        Assert.Equal(0, points[0].X);
        Assert.Equal(0, points[0].Y);
        
        Assert.Equal(3, points[1].X);
        Assert.Equal(4, points[1].Y);
        
        Assert.Equal(7, points[2].X);
        Assert.Equal(9, points[2].Y);
    }

    #endregion
}

public class PersonTests
{
    #region Person

    [Fact]
    public void Person_Constructor_SetsCorrectValues()
    {
        var person = new Person("Іваненко Іван", 45);
        
        Assert.Equal("Іваненко Іван", person.Name);
        Assert.Equal(45, person.Age);
    }

    [Fact]
    public void Person_Name_Property_GetAndSet_WorksCorrectly()
    {
        var person = new Person("Test", 20);
        
        person.Name = "New Name";
        
        Assert.Equal("New Name", person.Name);
    }

    [Fact]
    public void Person_Age_Property_GetAndSet_WorksCorrectly()
    {
        var person = new Person("Test", 20);
        
        person.Age = 25;
        
        Assert.Equal(25, person.Age);
    }

    [Fact]
    public void Person_GetTypeOrder_ReturnsZero()
    {
        var person = new Person("Test", 30);
        
        int order = person.GetTypeOrder();
        
        Assert.Equal(0, order);
    }

    [Fact]
    public void Person_Show_WritesToConsole()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        
        var person = new Person("Іваненко Іван", 45);
        person.Show();
        
        string output = sw.ToString();
        Assert.Contains("Іваненко Іван", output);
        Assert.Contains("45", output);
    }

    #endregion

    #region Student

    [Fact]
    public void Student_Constructor_SetsCorrectValues()
    {
        var student = new Student("Петренко Петро", 20, "КН-31", 4.5);
        
        Assert.Equal("Петренко Петро", student.Name);
        Assert.Equal(20, student.Age);
        Assert.Equal("КН-31", student.Group);
        Assert.Equal(4.5, student.GPA, 2);
    }

    [Fact]
    public void Student_Group_Property_GetAndSet_WorksCorrectly()
    {
        var student = new Student("Test", 20, "КН-31", 4.0);
        
        student.Group = "КН-32";
        
        Assert.Equal("КН-32", student.Group);
    }

    [Fact]
    public void Student_GPA_Property_GetAndSet_WorksCorrectly()
    {
        var student = new Student("Test", 20, "КН-31", 4.0);
        
        student.GPA = 3.8;
        
        Assert.Equal(3.8, student.GPA, 2);
    }

    [Fact]
    public void Student_GetTypeOrder_ReturnsOne()
    {
        var student = new Student("Test", 20, "КН-31", 4.0);
        
        int order = student.GetTypeOrder();
        
        Assert.Equal(1, order);
    }

    [Fact]
    public void Student_Show_WritesToConsole()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        
        var student = new Student("Петренко Петро", 20, "КН-31", 4.5);
        student.Show();
        
        string output = sw.ToString();
        Assert.Contains("Студент", output);
        Assert.Contains("Петренко Петро", output);
        Assert.Contains("КН-31", output);
    }

    #endregion

    #region Teacher

    [Fact]
    public void Teacher_Constructor_SetsCorrectValues()
    {
        var teacher = new Teacher("Коваленко Микола", 50, "C#", 25);
        
        Assert.Equal("Коваленко Микола", teacher.Name);
        Assert.Equal(50, teacher.Age);
        Assert.Equal("C#", teacher.Subject);
        Assert.Equal(25, teacher.Experience);
    }

    [Fact]
    public void Teacher_Subject_Property_GetAndSet_WorksCorrectly()
    {
        var teacher = new Teacher("Test", 40, "Math", 10);
        
        teacher.Subject = "Physics";
        
        Assert.Equal("Physics", teacher.Subject);
    }

    [Fact]
    public void Teacher_Experience_Property_GetAndSet_WorksCorrectly()
    {
        var teacher = new Teacher("Test", 40, "Math", 10);
        
        teacher.Experience = 15;
        
        Assert.Equal(15, teacher.Experience);
    }

    [Fact]
    public void Teacher_GetTypeOrder_ReturnsTwo()
    {
        var teacher = new Teacher("Test", 40, "Math", 10);
        
        int order = teacher.GetTypeOrder();
        
        Assert.Equal(2, order);
    }

    [Fact]
    public void Teacher_Show_WritesToConsole()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        
        var teacher = new Teacher("Коваленко Микола", 50, "C#", 25);
        teacher.Show();
        
        string output = sw.ToString();
        Assert.Contains("Викладач", output);
        Assert.Contains("Коваленко Микола", output);
        Assert.Contains("C#", output);
    }

    #endregion

    #region DepartmentHead

    [Fact]
    public void DepartmentHead_Constructor_SetsCorrectValues()
    {
        var head = new DepartmentHead("Шевченко Василь", 55, "ООП", 30, "Кафедра інформатики");
        
        Assert.Equal("Шевченко Василь", head.Name);
        Assert.Equal(55, head.Age);
        Assert.Equal("ООП", head.Subject);
        Assert.Equal(30, head.Experience);
        Assert.Equal("Кафедра інформатики", head.Department);
    }

    [Fact]
    public void DepartmentHead_Department_Property_GetAndSet_WorksCorrectly()
    {
        var head = new DepartmentHead("Test", 50, "OOП", 20, "Old Dept");
        
        head.Department = "New Dept";
        
        Assert.Equal("New Dept", head.Department);
    }

    [Fact]
    public void DepartmentHead_GetTypeOrder_ReturnsThree()
    {
        var head = new DepartmentHead("Test", 50, "ООП", 20, "Dept");
        
        int order = head.GetTypeOrder();
        
        Assert.Equal(3, order);
    }

    [Fact]
    public void DepartmentHead_Show_WritesToConsole()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        
        var head = new DepartmentHead("Шевченко Василь", 55, "ООП", 30, "Кафедра інформатики");
        head.Show();
        
        string output = sw.ToString();
        Assert.Contains("Завідувач кафедри", output);
        Assert.Contains("Шевченко Василь", output);
        Assert.Contains("Кафедра інформатики", output);
    }

    #endregion

    #region Сортування за типами

    [Fact]
    public void SortByType_CorrectOrder()
    {
        Person[] people = new Person[]
        {
            new Teacher("Test", 40, "Math", 10),
            new Student("Test", 20, "КН-31", 4.0),
            new Person("Test", 30),
            new DepartmentHead("Test", 50, "ООП", 20, "Dept")
        };

        var sorted = people.OrderBy(p => p.GetTypeOrder()).ToArray();

        Assert.IsType<Person>(sorted[0]);
        Assert.IsType<Student>(sorted[1]);
        Assert.IsType<Teacher>(sorted[2]);
        Assert.IsType<DepartmentHead>(sorted[3]);
    }

    [Fact]
    public void SortByAge_CorrectOrder()
    {
        Person[] people = new Person[]
        {
            new Person("Old", 60),
            new Student("Young", 18, "КН-31", 4.0),
            new Teacher("Middle", 40, "Math", 15)
        };

        var sorted = people.OrderBy(p => p.Age).ToArray();

        Assert.Equal(18, sorted[0].Age);
        Assert.Equal(40, sorted[1].Age);
        Assert.Equal(60, sorted[2].Age);
    }

    #endregion
}