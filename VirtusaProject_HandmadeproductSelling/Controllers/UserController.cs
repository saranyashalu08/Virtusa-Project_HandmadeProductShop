using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VirtusaProject_HandmadeproductSelling.Models;

namespace VirtusaProject_HandmadeproductSelling.Controllers
{
    public class UserController : Controller
    {
        Handmade_ProductEntities db = new Handmade_ProductEntities();

        // GET: User
        public ActionResult Index(int? page)
        {

            int pagesize = 7, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Categories.Where(m => m.Cat_status == 1).OrderByDescending(m => m.Cat_ID).ToList();
            IPagedList<Category> cate = list.ToPagedList(pageindex, pagesize);


            return View(cate);
        }
        public ActionResult Register()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            SelectListItem item0 = new SelectListItem() { Text = "Select Gender", Value = "Select Gender", Selected = false };
            SelectListItem item1 = new SelectListItem() { Text = "Female", Value = "Female", Selected = false };
            SelectListItem item2 = new SelectListItem() { Text = "Male", Value = "Male", Selected = false };
            SelectListItem item3 = new SelectListItem() { Text = "Transgender", Value = "Transgender", Selected = false };
            SelectListItem item4 = new SelectListItem() { Text = "I prefer not to say", Value = "I prefer not to say", Selected = false };
            Items.Add(item0);
            Items.Add(item1);
            Items.Add(item2);
            Items.Add(item3);
            Items.Add(item4);
            ViewBag.gender = Items;
            return View();
        }
        [HttpPost]
        public ActionResult Register(Registration registration)
        {
            if (registration == null)
            {
                Response.Write("Error");
            }
            else
            {
                Registration reg = new Registration();
                reg.f_name = registration.f_name;
                reg.l_name = registration.l_name;
                reg.email = registration.email;
                reg.gender = registration.gender;
                reg.phone = registration.phone;
                reg.c_password = registration.c_password;
                reg.Con_password = registration.Con_password;
                db.Registrations.Add(reg);
                db.SaveChanges();
                return RedirectToAction("Login");

            }


            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login(Registration registration)
        {

            Registration re = db.Registrations.Where(m => m.email == registration.email && m.c_password == registration.c_password).SingleOrDefault();
            if (re != null)
            {
                Session["custID"] = re.custID.ToString();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.error = "Invalid Username or Password";
            }

            return View();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            List<Category> li = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(li, "Cat_ID", "Cat_name");

          
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase P_image)
        {
            List<Category> li = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(li, "Cat_ID", "Cat_name");


            string path = Uploadimage(P_image);

            if (path.Equals("-1"))
            {
                ViewBag.error = "image could not be uploaded";

            }
            else

            {


                Product pr = new Product();
                pr.P_name = product.P_name;
                pr.P_image = path;
                pr.P_desc = product.P_desc;
                pr.P_price = product.P_price;
                pr.P_quantity = product.P_quantity;
                pr.add_pid = product.adduser_pid;


                pr.adduser_pid = Convert.ToInt32(Session["custID"].ToString());
                db.Products.Add(pr);
                db.SaveChanges();

                Response.Redirect("Index");
            }


            return View();


        }
        public ActionResult DisplayProduct(int ? id,int ? page)
        {
            TempData.Keep();
            int pagesize = 4, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(m => m.adduser_pid == id).OrderByDescending(m => m.P_ID).ToList();
            IPagedList<Product> cate = list.ToPagedList(pageindex, pagesize);


            return View(cate);
        }
        [HttpPost]
        public ActionResult DisplayAdd(int? id, int? page, string search)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(m=>m.P_name.Contains(search)).OrderByDescending(x => x.P_ID).ToList();
            IPagedList<Product> cate = list.ToPagedList(pageindex, pagesize);

            return View(cate);
        }


        public string Uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)

            {

                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))

                {

                    try

                    {



                        path = Path.Combine(Server.MapPath("~/Content/Upload"), random + Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "~/Content/Upload/" + random + Path.GetFileName(file.FileName);



                        ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception)

                    {

                        path = "-1";

                    }

                }

                else

                {

                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

                }

            }



            else

            {

                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";

            }







            return path;


        }
        public ActionResult DisplayDetails(int?id,int ?page)
        {
            DisplayDetails displayDetails = new DisplayDetails();
            Product product=db.Products.Where(m=>m.P_ID==id).SingleOrDefault();
            displayDetails.P_ID = product.P_ID;
            displayDetails.P_name = product.P_name;
            displayDetails.P_image = product.P_image;
            displayDetails.P_price = product.P_price;
            displayDetails.P_desc = product.P_desc;
            displayDetails.P_quantity = product.P_quantity;


            Category category = db.Categories.Where(m => m.Cat_ID == product.add_pid).SingleOrDefault();
            displayDetails.Cat_name = category.Cat_name;
            
            //Registration registration=db.Registrations.Where(m=>m.custID==product.adduser_pid).SingleOrDefault();

            //displayDetails.f_name=registration.f_name;



            return View(displayDetails);




        }
        public ActionResult Signout()
        {

            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index");
        }


    }
}