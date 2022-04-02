using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSON_Students_Serialization
{
    class Program
    {
        public const int TIOTION = 10000;
        public static void AddNewStudent()
        {
            string name = "";
            int age = 0;
            Console.WriteLine("Set New Student's Name:");
            name = Console.ReadLine();
            Console.WriteLine("Set New Student's Age:");
            age = int.Parse(Console.ReadLine());
            User addStudent = new User(name, age);
            List<User> users = new List<User>();
            // -->
            foreach (User user in users)
            {
                if (addStudent.Name.Trim().ToLower() == user.Name.ToLower())
                {
                    Console.WriteLine("The Name already exist");
                    return;
                }
            }
            users.Add(addStudent);
            Console.WriteLine("The Add Operation Is Done.");
            UpdateAndSerialize(users);
        }
        //
        public static void RemoveExistStudent(string studentName)
        {
            bool removeStatus = false;
            List<User> users = Deserialize();
            foreach (User user in users)
            {
                if (user.Name.Trim().ToLower() == studentName.ToLower())
                {
                    removeStatus = users.Remove(user);
                    UpdateAndSerialize(users);
                    break;
                }
            }
            if (removeStatus)
                Console.WriteLine("Remove Operation Is Done!");
            else
                Console.WriteLine("Remove Operation Is Failure");
        }
        //
        public static void Print()
        {
            List<User> users = Deserialize();
            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"Student ID [{i + 1}] : {users[i]}");
        }
        //
        public static void TuitionFeeReporting()
        {
            List<User> users = Deserialize();
            var discount = (int age) => { if (age < 25) return (TIOTION.ToString()); else return ((TIOTION / 10 * 9).ToString()); };
            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"{users[i]} need to pay {discount(users[i].Age)}");
        }
        //
        public static List<User> Deserialize()
        {
            string jsonString = File.ReadAllText("students.json");
            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);
            return users;
        }
        //
        public static void UpdateAndSerialize(List<User> users)
        {
            string jsonStr = JsonSerializer.Serialize(users);
            File.WriteAllText("students.json", jsonStr);
        }
        //
        static void Main(string[] args)
        {
            bool breakFlag = false;
            string userActionReadLine;
            string userNameReadLine = "";
            int userAgeReadLine;
            string readText;
            string fileName = "students.json";
            string jsonString;
            string name = "";
            int age = 0;
            //
            const int TUITIONFEE = 10000;
            //
            try
            {
                while (!breakFlag)
                {
                    Console.WriteLine(
                        "\nWelcom To Students List App\n" +
                        "What action would you like to take?\n" +
                        "(a) - Add new student\n" +
                        "(d) - Remove exist student\n" +
                        "(p) - Print students list\n" +
                        "(t) - Print students tuition report\n" +
                        "(q) - Quit & Close The App");
                    userActionReadLine = Console.ReadLine().ToLower();
                    //                
                    switch (userActionReadLine)
                    {
                        case "a":
                            AddNewStudent();
                            break;
                        //
                        case "d":
                            RemoveExistStudent(userNameReadLine);
                            break;
                        //
                        case "p":
                            Print();
                            break;
                        //
                        case "t":
                            TuitionFeeReporting();
                            break;
                        //
                        case "q":
                            breakFlag = true;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Operation Fail!");
            }
        }
    }
}