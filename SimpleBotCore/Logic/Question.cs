using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBotCore.Logic
{
    [BsonIgnoreExtraElements]
    public class Question
    {
        public string Description { get; set; }
        public int Position { get; set; }
    }
}
