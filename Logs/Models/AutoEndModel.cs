using System;
using MongoDB.Bson;

namespace Logs.Models
{
    public class AutoEndModel
    {
        public int ReservationID { get; set; }
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public int ClientID { get; set; }
        public string DisplayName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }

        public static AutoEndModel Create(BsonDocument doc)
        {
            return new AutoEndModel()
            {
                ReservationID = doc["ReservationID"].AsInt32,
                ResourceID = doc["ResourceID"].AsInt32,
                ResourceName = doc["ResourceName"].AsString,
                ClientID = doc["ClientID"].AsInt32,
                DisplayName = doc["DisplayName"].AsString,
                Timestamp = doc["Timestamp"].ToUniversalTime().ToLocalTime(),
                Action = doc["Action"].AsString
            };

        }
    }
}