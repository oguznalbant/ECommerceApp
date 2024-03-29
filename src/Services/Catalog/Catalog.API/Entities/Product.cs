﻿using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId(IdGenerator = typeof(ObjectIDGenerator))]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } // it would be generated by mongo 

        [BsonElement("Name")] // if you want to choose name for column different from entity prop 
        public string Name { get; set; }
        
        public string Category { get; set; }
        
        public string Summary { get; set; }
        
        public string Description { get; set; }
        
        public string ImageFile { get; set; }
        
        public decimal Price { get; set; }
    }
}
