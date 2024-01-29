using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class SequenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string sequence { get; set; }
    }