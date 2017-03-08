using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using System.Threading.Tasks;
using Logs.Models;

namespace Logs.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("autoend")]
        public async Task<ActionResult> AutoEnd(string search = null, int skip = 0, int limit = 10)
        {
            MongoClient mongo = new MongoClient(ConfigurationManager.AppSettings["MongoConnectionString"]);
            var db = mongo.GetDatabase("logs");
            var col = db.GetCollection<BsonDocument>("autoend");

            FilterDefinition<BsonDocument> filter;
            SortDefinition<BsonDocument> sort = Builders<BsonDocument>.Sort.Descending("Timestamp");

            if (string.IsNullOrEmpty(search))
                filter = FilterDefinition<BsonDocument>.Empty;
            else
                filter = Builders<BsonDocument>.Filter.Eq("Action", search);

            var query = col.Find(filter).Sort(sort).Skip(skip).Limit(limit);

            var docs = await query.ToListAsync();
            var model = docs.Select(AutoEndModel.Create).ToList();
            return View(model);
        }

        [Route("job")]
        public async Task<ActionResult> Job(string search = null, int skip = 0, int limit = 10)
        {
            MongoClient mongo = new MongoClient(ConfigurationManager.AppSettings["MongoConnectionString"]);
            var db = mongo.GetDatabase("logs");
            var col = db.GetCollection<BsonDocument>("job");

            FilterDefinition<BsonDocument> filter;
            SortDefinition<BsonDocument> sort = Builders<BsonDocument>.Sort.Descending("Timestamp");

            if (string.IsNullOrEmpty(search))
                filter = FilterDefinition<BsonDocument>.Empty;
            else
                filter = Builders<BsonDocument>.Filter.Regex("Path", new BsonRegularExpression(search));

            var query = col.Find(filter).Sort(sort).Skip(skip).Limit(limit);

            var docs = await query.ToListAsync();
            var model = docs.Select(JobModel.Create).ToList();
            return View(model);
        }
    }
}