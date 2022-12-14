using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtusaProject_HandmadeproductSelling.Models;
using PagedList.Mvc;
using PagedList;
using System.Web.UI;

namespace VirtusaProject_HandmadeproductSelling.Controllers
{
    public class AdminController : Controller
    {
        Handmade_ProductEntities db = new Handmade_ProductEntities();

        [HttpGet]
        // GET: Admin
        public ActionResult ALogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ALogin(Admin admin)
        {
            Admin ad = db.Admins.Where(m => m.AdminName == admin.AdminName && m.A_Password == admin.A_Password).SingleOrDefault();
            if (ad != null)
            {
                Session["AdminID"] = ad.AdminID.ToString();
                return RedirectToAction("Category");

            }
            else
            {
                ViewBag.error = "Invalid Username or Password";
            }
            return View();
        }
        public ActionResult Category()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("ALogin");

            }
            return View();
        }
        [HttpPost]
        public ActionResult Category(Category category, HttpPostedFileBase Image)
        {
            Admin ad = new Admin();
            string path = Uploadimage(Image);
            if (path.Equals("-1"))
            {
                ViewBag.Error = "Image not uploaded";
            }
            else
            {
                Category cat = new Category();
                cat.Cat_name = category.Cat_name;
                cat.Cat_status = 1;
                cat.Cat_img = path;

                cat.add_id = Convert.ToInt32(Session["AdminID"].ToString());


                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("ViewCategory");

            }

            return View();
        }
        public ActionResult ViewCategory(int? page)
        {
            int pagesize = 7, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Categories.Where(m => m.Cat_status == 1).OrderByDescending(m => m.Cat_ID).ToList();
            IPagedList<Category> cate = list.ToPagedList(pageindex, pagesize);


            return View(cate);
        }
        public ActionResult AddProductAdmin()
        {
            List<Category> li = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(li, "Cat_ID", "Cat_name");



            return View();
        }
        [HttpPost]
        public ActionResult AddProductAdmin(Product product, HttpPostedFileBase P_image)
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
                pr.add_pid = product.addadmin_pid;


                pr.addadmin_pid = Convert.ToInt32(Session["AdminID"].ToString());
                db.Products.Add(pr);
                db.SaveChanges();

                Response.Redirect("DisplayProductAdmin");
            }


            return View();


        }
        public ActionResult DisplayProductAdmin(int? id, int? page)
        {
            TempData.Keep();
            int pagesize = 7, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(m => m.addadmin_pid == id).OrderByDescending(m => m.P_ID).ToList();
            IPagedList<Product> cate = list.ToPagedList(pageindex, pagesize);


            return View(cate);
        }
        [HttpPost]
        public ActionResult DisplayProductAdmin(int? id, int? page, string search)
        {
            int pagesize = 7, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(m => m.P_name.Contains(search)).OrderByDescending(m => m.P_ID).ToList();
            IPagedList<Product> cate = list.ToPagedList(pageindex, pagesize);

            return View(cate);
        }
        public ActionResult DisplayDetails(int? id, int? page)
        {
           
                DisplayDetails displayDetails = new DisplayDetails();
                Product product = db.Products.Where(m => m.P_ID == id).SingleOrDefault();
                displayDetails.P_ID = product.P_ID;
                displayDetails.P_name = product.P_name;
                displayDetails.P_image = product.P_image;
                displayDetails.P_price = product.P_price;
                displayDetails.P_desc = product.P_desc;
                displayDetails.P_quantity = product.P_quantity;


                Category category = db.Categories.Where(m => m.Cat_ID == product.add_pid).SingleOrDefault();
                displayDetails.Cat_name = category.Cat_name;

                //Registration registration = db.Registrations.Where(m => m.custID == product.adduser_pid).SingleOrDefault();

                //displayDetails.email = registration.email;
                //displayDetails.adduser_pid = registration.custID;




                return View(displayDetails);




           
          
        }
        public ActionResult Delete(int? id)
        {
            Product p = db.Products.Where(m => m.P_ID == id).SingleOrDefault();
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        

    }
}