using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Lesson11practice
{
    class Program
    {
        static void Main(string[] args)
        {
            SakilaContext context = new SakilaContext();
            Actor[] actors = context.Actor.ToArray();

            //Create some data
            MyData data1 = new MyData("Bill", 21, "blue", false, "basketball");
            MyData data2 = new MyData("Mary", 25, "blue", true, "baseball");
            MyData data3 = new MyData("John", 30, "orange", true, "baseball");
            MyData data4 = new MyData("Marcus", 45, "red", false, "football");
            MyData data5 = new MyData("Kelly", 12, "green", true, "tennis");

            //Add data to list
            List<MyData> myList = new List<MyData>();
            myList.Add(data1);
            myList.Add(data2);
            myList.Add(data3);
            myList.Add(data4);
            myList.Add(data5);

            //using LINQ to work with data
            //get data for those who like baseball
            var baseballFans = myList.Where(x => x.favoriteSport == "baseball");
            foreach(var item in baseballFans)
            {
                Console.WriteLine(item.name + " is a baseball fan.");
            }

            //who is under 21
            var under21 = myList.Where(x => x.age < 21);
            foreach(var item in under21)
            {
                Console.WriteLine(item.name + " is under 21.");
            }

            //who has a pet
            var whoHasPet = myList.Where(x => x.hasPet == true);
            foreach(var item in whoHasPet)
            {
                Console.WriteLine(item.name + "has a pet.");
            }
            
            //LINQ example
            List<Actor> linqList = (from a in actors
                                    where a.last_name.StartsWith("B")
                                    select a).ToList();
            foreach(Actor a in linqList)
            {
                Console.WriteLine(a.actor_id + " " + a.first_name + " " + a.last_name);
            }

            //building html
            StringBuilder htmlBuilder = new StringBuilder();

            string openingHtml = "<body> \n" + "<h1> List of Actors </h1> \n" + "<ul>";

            htmlBuilder.Append(openingHtml);

            foreach (Actor a in actors)
            {
                htmlBuilder.Append("<li>" + a.first_name + " " + a.last_name + "</li>");
            }

            string closingHtml = "</ul> \n" + "</body>";

            htmlBuilder.Append(closingHtml);

            string fileName = "C:\\Users\\tmorr\\OneDrive\\Desktop\\C#Labs\\actors.html";
            File.WriteAllText(fileName, htmlBuilder.ToString());

            foreach (Actor a in actors)
            {
                Console.WriteLine(a.first_name + " " + a.last_name);
            }

            //Create an actor
            Console.WriteLine("Creating a new actor. Enter FirstName");
            string first_name = Console.ReadLine();

            Console.WriteLine("Creating a new actor. Enter LastName");
            string last_name = Console.ReadLine();

            Actor newActorRecord = new Actor(first_name, last_name);

            context.Actor.Add(newActorRecord);
            context.SaveChanges();

            Actor myActor = context.Actor.Find(32);
            Console.WriteLine(myActor.first_name + " " + myActor.last_name);

            myActor.first_name = "GENE";
            myActor.last_update = DateTime.Now;
            context.Actor.Update(myActor);
            context.SaveChanges();
        }
    }
}
