using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.ServiceReference2;

namespace WebClient.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Medicine
        public ActionResult GetMedicines()
        {
            Service1Client client = new Service1Client();
            List<MedicineContract> list = client.GetMedicines().ToList();
            ViewBag.List = list;
            return View();
        }
        public ActionResult GetMedicine(int id)
        {
            return View();
        }
        public ActionResult InsertMedicine()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertMedicine(MedicineContract med)
        {
            Service1Client client = new Service1Client();
            client.AddMedicine(med);
            return View();
        }
        public ActionResult UpdateMedicine()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateMedicine(MedicineContract med)
        {
            Service1Client client = new Service1Client();
            client.UpdateMedicine(med);
            return View();
        }
        public ActionResult DeleteMedicine()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteMedicine(int id)
        {
            Service1Client client = new Service1Client();
            client.DeleteMedicine(id);
            return View();
        }
        public ActionResult BuyMedicine()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BuyMedicine(MedicineContract med)
        {
            Service1Client client = new Service1Client();
            client.BuyMedicine(med);
            return View();
        }

        public ActionResult GetHistory()
        {
            Service1Client client = new Service1Client();
            List<PurchaseContract> list = client.GetHistory().ToList();
            ViewBag.List = list;
            return View();
        }

    }
}