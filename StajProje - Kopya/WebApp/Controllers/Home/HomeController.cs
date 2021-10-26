using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DataManager.DataManagerSoapClient servis = new DataManager.DataManagerSoapClient();
            List<DataManager.Film> temp = servis.FilmListele();
            return View(temp);
        }
        public ActionResult FilmDetay(int filmid)
        {
            DataManager.DataManagerSoapClient servis = new DataManager.DataManagerSoapClient();
            DataManager.Film temp = servis.FilmDetayGetir(filmid);
            return View(temp);
        }
        
    }
}