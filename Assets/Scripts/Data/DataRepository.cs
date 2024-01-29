using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

/*
public class DataRepository
{
    const string atlasConnectionString = "mongodb+srv://seqboard:seqboard@wraith.5mdrhgd.mongodb.net/?retryWrites=true&w=majority";
    private IMongoCollection<BsonDocument> _collection;
    private MongoClient _dbClient;

    public DataRepository()
    {
        _dbClient = new MongoClient(atlasConnectionString);
        _collection = _dbClient.GetDatabase("wraith").GetCollection<BsonDocument>("sequences");
    }

    public void SaveSequence(string name, string sequence)
    {
        var document = new BsonDocument
        {
            { "name", name },
            { "sequence", sequence }
        };
        _collection.InsertOne(document);
    }

    public string GetSequence(string name)
    {
        var sequence = _collection.Find(new BsonDocument("name", name));
        return sequence;
    }

    public void GetAllSequences()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateSequence()
    {
        throw new System.NotImplementedException();
    }

    public void DeleteSequence()
    {
        throw new System.NotImplementedException();
    }
}
*/