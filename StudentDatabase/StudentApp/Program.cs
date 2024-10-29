using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (var context = new SchoolContext())
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Add a new student
            var student = new Student
            {
                Name = "James White",
                Age = 27
            };

            context.Students.Add(student);
            context.SaveChanges();

            // Display all students
            var students = context.Students.ToList();
            foreach (var s in students)
            {
                Console.WriteLine($"Id: {s.Id}, Name: {s.Name}, Age: {s.Age}");
            }
        }
    }
}