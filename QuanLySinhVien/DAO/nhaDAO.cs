using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using QuanLyNhaDat.Models;

namespace QuanLyNhaDat.DAO
{
    public class nhaDAO
    {
        private MongoClient mongoClient;
        private IMongoCollection<Nha> nhaCollection;

        public nhaDAO()
        {
            mongoClient = new MongoClient("mongodb://localhost:27017");
            var dataBase = mongoClient.GetDatabase("QLyNhaDat");
            nhaCollection = dataBase.GetCollection<Nha>("Nha");
        }

        public List<Nha> FindAll()
        {
            var diemsv = nhaCollection.AsQueryable<Nha>().ToList();
            return diemsv;
        }

        public void Create(Nha nha)
        {
            nhaCollection.InsertOne(nha);
        }

        public Nha GetById(string id)
        {
            var result = nhaCollection.AsQueryable<Nha>().SingleOrDefault(x => x.Id == ObjectId.Parse(id));

            return result;
        }

        public void Update(string id, Nha nha)
        {
            nhaCollection.UpdateOne(
                Builders<Nha>.Filter.Eq("_id", new ObjectId(id)),
                Builders<Nha>.Update.Set("dientich", nha.DienTich)
                .Set("sophong", nha.SoPhong)
                .Set("khoangcach", nha.KhoangCach)
                );
        }

        public void Delete(string id)
        {
            nhaCollection.DeleteOne(Builders<Nha>.Filter.Eq("_id", ObjectId.Parse(id)));
        }

        public List<Nha> Search(int soPhong)
        {
            var nha = nhaCollection.AsQueryable<Nha>().Where(x => x.SoPhong == soPhong).ToList();
            return nha;
        }

        //public void InsertData()
        //{
        //    string path = @"C:\Users\Hieu Bui\source\repos\QuanLySinhVien\QuanLySinhVien\Data\data.txt";
        //    string[] lines = File.ReadAllLines(path);

        //    for(int  i = 0; i < lines.Length; i++)
        //    {
        //        string line = lines[i];
        //        string[] words = line.Split(',');

        //        var nha = new Nha()
        //        {
        //            DienTich = Convert.ToDouble(words[0]),
        //            SoPhong = Convert.ToInt32(words[1]),
        //            KhoangCach = Convert.ToDouble(words[2])
        //        };

        //        nhaCollection.InsertOne(nha);
        //    }
        //}
    }
}