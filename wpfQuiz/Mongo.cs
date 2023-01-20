using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfQuiz
{
    public class Mongo
    {
        public static void AddToDB(Issue issue)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("QuizBase");
            var collection = database.GetCollection<Issue>("Quiz");
            collection.InsertOne(issue);
        }

        public static void FindAll(List<Issue> issues)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("QuizBase");
            var collection = database.GetCollection<Issue>("Quiz");
            var list = collection.Find(x => true).ToList();
            foreach (var issue in list)
            {
                issues.Add(issue);
            }
        }
    }
}
