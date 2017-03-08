using MongoDB.Bson;
using System;

namespace Logs.Models
{
    public class JobModel
    {
        public string Path { get; set; }
        public string Result { get; set; }
        public DateTime Timestamp { get; set; }

        public static JobModel Create(BsonDocument doc)
        {
            return new JobModel()
            {
                Path = doc["Path"].AsString,
                Result = doc["Result"].AsString,
                Timestamp = doc["Timestamp"].ToUniversalTime().ToLocalTime()
            };
        }
    }
}