using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhaDat.Models
{
    public class Nha
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("dientich")]
        public double DienTich { get; set; }

        [BsonElement("sophong")]
        public int SoPhong { get; set; }

        [BsonElement("khoangcach")]
        public double KhoangCach { get; set; }

    }
}