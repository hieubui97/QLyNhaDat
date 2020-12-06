using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.DAO
{
    public class DiemSV_DAO
    {
        private MongoClient mongoClient;
        private IMongoCollection<DiemSV> diemsvCollection;

        public DiemSV_DAO()
        {
            mongoClient = new MongoClient("mongodb://localhost:27017");
            var dataBase = mongoClient.GetDatabase("QLyDiemSinhVien");
            diemsvCollection = dataBase.GetCollection<DiemSV>("DiemSV");
        }

        public List<DiemSV> FindAll()
        {
            var diemsv = diemsvCollection.AsQueryable<DiemSV>().ToList();
            return diemsv;
        }

        public void Create(DiemSV diemSV)
        {
            diemsvCollection.InsertOne(diemSV);
        }

        public DiemSV GetById(string id)
        {
            var result = diemsvCollection.AsQueryable<DiemSV>().SingleOrDefault(x => x.Id == ObjectId.Parse(id));

            return result;
        }

        public void Update(string id, DiemSV diemSV)
        {
            diemsvCollection.UpdateOne(
                Builders<DiemSV>.Filter.Eq("_id", new ObjectId(id)),
                Builders<DiemSV>.Update.Set("masv", diemSV.MaSV)
                .Set("diemky1", diemSV.DiemKy1)
                .Set("diemky2", diemSV.DiemKy2)
                );
        }

        public void Delete(string id)
        {
            diemsvCollection.DeleteOne(Builders<DiemSV>.Filter.Eq("_id", ObjectId.Parse(id)));
        }

        public List<DiemSV> Search(bool ketQua)
        {
            var diemsv = diemsvCollection.AsQueryable<DiemSV>().Where(x => x.KetQua == ketQua).ToList();
            return diemsv;
        }

        public void InsertData()
        {
            int masv = 1;
            string path = @"C:\Users\Hieu Bui\source\repos\QuanLySinhVien\QuanLySinhVien\Data\data.txt";
            string[] lines = File.ReadAllLines(path);

            for(int  i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] words = line.Split(',');

                var diemSV = new DiemSV()
                {
                    MaSV = masv.ToString(),
                    DiemKy1 = float.Parse(words[0]),
                    DiemKy2 = float.Parse(words[1]),
                    KetQua = Convert.ToBoolean(Convert.ToInt32(words[2]))
                };

                masv++;
                diemsvCollection.InsertOne(diemSV);
            }
        }
    }
}