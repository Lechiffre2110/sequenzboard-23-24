using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

public class Data
    {
        private string connectionUrl = "mongodb://localhost:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+1.6.1";
        private string dbName = "SequenzboardDB";
        private string collectionName = "Sequenzen";

        public List<SequenceModel> GetAllSequences()
        {
            //Establish db connection
            var client = new MongoClient(connectionUrl);
            var db = client.GetDatabase(dbName);
            var sequenceCollection = db.GetCollection<SequenceModel>(collectionName);

            var results = sequenceCollection.Find(_ => true);

            List<SequenceModel> allSequences = new List<SequenceModel>();

            foreach (var result in results.ToList())
            {
                allSequences.Add(result);
            }

            return allSequences;
        }

        public void SaveSequence(string name, string sequence)
        {
            Debug.Log("SaveSequence: " + name + " " + sequence);
            try {
                //Establish db connection
                var client = new MongoClient(connectionUrl);
                var db = client.GetDatabase(dbName);
                var sequenceCollection = db.GetCollection<SequenceModel>(collectionName);

                var document = new SequenceModel
                {
                    name = name,
                    sequence = sequence
                };

                sequenceCollection.InsertOne(document);
                Debug.Log("Saved sequence: " + name + " " + sequence);
            } catch (Exception e) {
                Debug.Log("Error saving sequence: " + e);
            }
        }

        public void DeleteSequence(string name)
        {
            //Establish db connection
            var client = new MongoClient(connectionUrl);
            var db = client.GetDatabase(dbName);
            var sequenceCollection = db.GetCollection<SequenceModel>(collectionName);

            var filter = Builders<SequenceModel>.Filter.Eq("name", name);

            sequenceCollection.DeleteOne(filter);
        }
    }
    

        
        
