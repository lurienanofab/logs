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
        public async Task<ActionResult> AutoEnd()
        {
            MongoClient mongo = new MongoClient(ConfigurationManager.AppSettings["MongoConnectionString"]);
            var db = mongo.GetDatabase("logs");
            var col = db.GetCollection<BsonDocument>("autoend");
            var docs = await col.Find(FilterDefinition<BsonDocument>.Empty).ToListAsync();
            var model = docs.Select(AutoEndModel.Create).ToList();
            return View(model);
        }
    }
}