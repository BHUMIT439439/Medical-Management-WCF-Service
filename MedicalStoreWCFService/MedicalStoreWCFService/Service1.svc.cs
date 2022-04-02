using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MedicalStoreWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void AddMedicine(MedicineContract medicineContract)
        {
            MedicalDBEntities db = new MedicalDBEntities();
            db.Medicines.Add(new Medicine { Name = medicineContract.Name , CompanyName = medicineContract.CompanyName , Stock = medicineContract.stock,Price= medicineContract.price});
            db.SaveChanges();
        }

        public void DeleteMedicine(int MedicineId)
        {
            MedicalDBEntities db = new MedicalDBEntities();
            var matchedMedicine = db.Medicines.FirstOrDefault(m => m.MedicineID == MedicineId);
            if(matchedMedicine != null)
            {
                db.Medicines.Remove(matchedMedicine);
                db.SaveChanges();
            }
        }

        public MedicineContract GetMedicine(int MedicineId)
        {
            MedicalDBEntities db = new MedicalDBEntities();
            var matchedMedicine = db.Medicines.FirstOrDefault(m => m.MedicineID == MedicineId);
            if(matchedMedicine != null)
            {
                return new MedicineContract
                {
                    MedicineID = matchedMedicine.MedicineID,
                    Name = matchedMedicine.Name, 
                    CompanyName = matchedMedicine.CompanyName, 
                    price = matchedMedicine.Price, 
                    stock = matchedMedicine.Stock 
                };

            }
            else
            {
                return null;
            }
        }

        public List<MedicineContract> GetMedicines()
        {
            MedicalDBEntities db = new MedicalDBEntities();
            return db.Medicines.Select(m => new MedicineContract { MedicineID=m.MedicineID,Name=m.Name,CompanyName=m.CompanyName,stock=m.Stock,price=m.Price }).ToList();
        }

        public void UpdateMedicine(MedicineContract medicineContract)
        {
            MedicalDBEntities db = new MedicalDBEntities();
            var existingMedicine = db.Medicines.Where(m => m.MedicineID == medicineContract.MedicineID).FirstOrDefault();
            if(existingMedicine != null)
            {
                existingMedicine.Name = medicineContract.Name;
                existingMedicine.CompanyName = medicineContract.CompanyName;
                existingMedicine.Stock = medicineContract.stock;
                existingMedicine.Price = medicineContract.price;

                db.SaveChanges();
            }
        }
        public void BuyMedicine(MedicineContract medicineContract)
        {
            MedicalDBEntities Mdb = new MedicalDBEntities();
            PurchaseDBEntities Pdb = new PurchaseDBEntities();

            var existingMedicine = Mdb.Medicines.Where(m => m.MedicineID == medicineContract.MedicineID).FirstOrDefault();
            
            if (existingMedicine != null)
            {
                int t = existingMedicine.Stock;
                existingMedicine.Stock = t - medicineContract.stock;

                Mdb.SaveChanges();
            }

            Purchase p = new Purchase();
            p.Id = medicineContract.MedicineID;
            p.MedicineName = medicineContract.Name;
            p.MedicineCompanyName = medicineContract.CompanyName;
            p.MedicinePrice = medicineContract.price;
            p.MedicineQuantity = medicineContract.stock;

            int total = p.MedicineQuantity * existingMedicine.Price;
            p.Amount = total;
            p.Date = DateTime.Now.Date;

            Pdb.Purchases.Add(p);
            Pdb.SaveChanges();

        }
        public List<PurchaseContract> GetHistory()
        {
            PurchaseDBEntities Pdb = new PurchaseDBEntities();
            return Pdb.Purchases.Select(p => new PurchaseContract { Id = p.Id, MedicineName=p.MedicineName, MedicineCompanyName=p.MedicineCompanyName, MedicinePrice = p.MedicinePrice, Amount=p.Amount, Date = p.Date, MedicineQuantity=p.MedicineQuantity}).ToList();
        }
    }
}
