using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfQuiz
{
    public class Issue
    {
        
        string? question;
        string? answer;
        
        public Issue(string question, string answer)
        {
            this.question = question;
            this.answer = answer;
        }

        [BsonId]
        [BsonIgnoreIfDefault]
        ObjectId _id;

        [BsonIgnoreIfNull]

        public int Number { get; set; }

        public string? Question { get => question; }

        [BsonIgnoreIfNull]
        public string? Answer { get => answer; set => answer = value; }
    }
}
