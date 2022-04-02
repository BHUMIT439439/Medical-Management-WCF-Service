using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MedicalStoreWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        void AddMedicine(MedicineContract medicineContract);

        [OperationContract]
        List<MedicineContract> GetMedicines();

        [OperationContract]
        MedicineContract GetMedicine(int MedicineId);

        [OperationContract]
        void UpdateMedicine(MedicineContract medicineContract);

        [OperationContract]
        void DeleteMedicine(int MedicineId);

        [OperationContract]
        void BuyMedicine(MedicineContract medicineContract);

        [OperationContract]
        List<PurchaseContract> GetHistory();

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class MedicineContract
    {
        [DataMember]
        public int MedicineID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public int stock { get; set; }

        [DataMember]
        public int price { get; set; }
    }

    [DataContract]
    public class PurchaseContract
    {
        [DataMember]
        public int PurchaseId { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string MedicineName { get; set; }

        [DataMember]
        public string MedicineCompanyName { get; set; }

        [DataMember]
        public int MedicinePrice { get; set; }

        [DataMember]
        public int MedicineQuantity { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Amount { get; set; }
    }
}
