using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaudisoftTask.Areas.UserRole.Models;
using SaudisoftTask.Models;
using System.Data.Entity.SqlServer;
using System.Globalization;
using PagedList;
using SaudisoftTask.ViewModels;

namespace SaudisoftTask.Areas.UserRole.Controllers
{
    public class UsersController : Controller
    {
        private DbConnection db = new DbConnection();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user.Name==null || user.Password == null)
            {
                return View();
            }

            var _loginUser = db.Users.SingleOrDefault(U => U.Name == user.Name && U.Password == user.Password);
            if(_loginUser == null)
            {
                return View();
            }
            if(_loginUser.RoleId ==1)
            {
                Session["UserId"] = _loginUser.ID;
                return RedirectToAction("Index", _loginUser);
            }
            else
            {
                Session["UserId"] = _loginUser.ID;
                return RedirectToAction("EmployeeIndex");
            }
        }
        // GET: User/Users
        public ActionResult Index()
        {
            var UserRole = CheckUser();
            if (UserRole == 1)
            {
                var users = db.Users.Include(u => u.Role);
                return View(users.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: User/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Users/Create
        public ActionResult Create()
        {
            var UserRole = CheckUser();
            if (UserRole == 0)
            {
                return RedirectToAction("Login");
            }
            if (UserRole == 0 || UserRole != 1)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.UserRoles, "ID", "Role");
                return View();
            }
            
        }

        // POST: User/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleId,Name,Password,CheckInTime,CheckOutTime")] User user)
        {
            var UserRole = CheckUser();
            if(UserRole ==1)
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RoleId = new SelectList(db.UserRoles, "ID", "Role", user.RoleId);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpGet]
        public ActionResult EmployeeIndex()
        {
            var UserRole = CheckUser();
            if(UserRole == 2)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        // GET: User/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "ID", "Role", user.RoleId);
            return View(user);
        }

        // POST: User/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleId,Name,Password,CheckInTime,CheckOutTime")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "ID", "Role", user.RoleId);
            return View(user);
        }

        // GET: User/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int CheckUser()
        {
            if (Session["UserId"] == null)
            {
                Session.Clear();
                return 0;
            }
            else
            {
                int _userId = Convert.ToInt32(Session["UserId"]);
                var _loginUser = db.Users.SingleOrDefault(U => U.ID == _userId);
                if(_loginUser == null)
                {
                    Session.Clear();
                    return 0;
                }
                return _loginUser.RoleId;
            }  
        }
        [HttpPost]
        public ActionResult CheckIn(int UserId,string CheckinComment)
        {
            var _Date = DateTime.Now.ToString("yyyy-MM-dd");
            var _userRecord = db.LogedinUsers.SingleOrDefault(U => U.userId == UserId && U.Day == _Date);
            if (_userRecord == null)
            {
                var _logedinUser = new LogedinUser()
                {
                    userId = UserId,
                    Day = _Date,
                    CheckInTime = DateTime.Now.ToString("hh:mm tt"),
                    CheckOutTime = null,
                    CheckInComment= CheckinComment
                };
                db.LogedinUsers.Add(_logedinUser);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public ActionResult CheckOut(int UserId,string CheckoutComment)
        {
            var _Date = DateTime.Now.ToString("yyyy-MM-dd");
            var _userRecord = db.LogedinUsers.SingleOrDefault(U => U.userId == UserId && U.Day == _Date && U.CheckOutTime==null);
            if (_userRecord == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _userRecord.CheckOutTime = DateTime.Now.ToString("hh:mm tt");
                _userRecord.CheckOutComment = CheckoutComment;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            //Report();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Report()
        {
            var UserRole = CheckUser();
            if (UserRole == 1)
            {
                var _reoprtVM = new ReportVM();
                return View(_reoprtVM);
            }
            else
            {
                return RedirectToAction("Login");

            }
        }
        [HttpPost]
        public ActionResult Report(ReportVM _report)
        {
            
            var UserRole = CheckUser();
            if (UserRole == 1&&_report.Report.DateFrom !=null && _report.Report.DateTo != null)
            {
                var Delay = (from r in db.LogedinUsers
                             where DbFunctions.CreateTime(SqlFunctions.DatePart("hh", r.CheckInTime),
                                   SqlFunctions.DatePart("mi", r.CheckInTime),
                                   SqlFunctions.DatePart("ss", r.CheckInTime)) > DbFunctions.CreateTime(SqlFunctions.DatePart("hh", r.user.CheckInTime),
                                   SqlFunctions.DatePart("mi", r.user.CheckInTime),
                                   SqlFunctions.DatePart("ss", r.user.CheckInTime))
                             group r by r.user.Name into g
                             select new
                             {
                                 Name = g.Key,
                                 delay = g.Count(),
                                 attendance = 0
                             }).ToList();

                var Attendance = db.LogedinUsers
                  .Select(z => new { z.user.Name, z.Day })
                  .GroupBy(x => new { x.Name })
                  .Select(g => new
                  {
                      Name = g.Key.Name,
                      delay = 0,
                      attendance = g.Count()
                  }).ToList();

                var _total = Delay.Union(Attendance);

                var _final = _total.Select(e => new { e.Name, e.delay, e.attendance }).GroupBy(e => e.Name).Select(g => new report
                {
                    Name = g.Key,
                    Del = g.Sum(x => x.delay),
                    _Attendance = g.Sum(x => x.attendance)
                }).ToList();

                var ReportVM = new ReportVM
                {
                    Reports = _final,
                };
                return View(ReportVM);
            }
            else if(UserRole == 1&&(_report.Report.DateFrom == null || _report.Report.DateTo == null))
            {
                var _reoprtVM = new ReportVM();
                return View(_reoprtVM);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
    }
}
