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

        /// <summary>
        /// Retrieves all sequences from the database.
        /// </summary>
        /// <returns>A list of SequenceModel objects representing all sequences.</returns>
        public List<SequenceModel> GetAllSequences()
        {
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

        /// <summary>
        /// Retrieves all sequence names from the database.
        /// </summary>
        /// <returns>A List of strings with the names of the sequences.</returns>
        public List<string> GetSequenceNames()
        {
            var client = new MongoClient(connectionUrl);
            var db = client.GetDatabase(dbName);
            var sequenceCollection = db.GetCollection<SequenceModel>(collectionName);

            var results = sequenceCollection.Find(_ => true);

            List<string> allSequences = new List<string>();

            foreach (var result in results.ToList())
            {
                allSequences.Add(result.name);
            }

            return allSequences;
        }
    
        /// <summary>
        /// Retrieves a sequence from the database by its name.
        /// </summary>
        /// <param name="name">The name of the sequence to retrieve.</param>
        /// <returns>A string representing the sequence.</returns>
        public string LoadSequence(string name)
        {
            var client = new MongoClient(connectionUrl);
            var db = client.GetDatabase(dbName);
            var sequenceCollection = db.GetCollection<SequenceModel>(collectionName);

            var filter = Builders<SequenceModel>.Filter.Eq("name", name);

            var result = sequenceCollection.Find(filter).FirstOrDefault();

            return result.sequence;
        }

        /// <summary>
        /// Saves a sequence to the database.
        /// </summary>
        /// <param name="name">The name of the sequence to save.</param>
        /// <param name="sequence">The sequence to save.</param>
        public void SaveSequence(string name, string sequence)
        {
            Debug.Log("SaveSequence: " + name + " " + sequence);
            try {
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

        /// <summary>
        /// Deletes a sequence from the database by its name.
        /// </summary>
        /// <param name="name">The name of the sequence to delete.</param>
        public void DeleteSequence(string name)
        {
            var client = new MongoClient(connectionUrl);
            var db = client.GetDatabase(dbName);
            var sequenceCollection = db.GetCollection<SequenceModel>(collectionName);

            var filter = Builders<SequenceModel>.Filter.Eq("name", name);

            sequenceCollection.DeleteOne(filter);
        }
    }
    

        
        
