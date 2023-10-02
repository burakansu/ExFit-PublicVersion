using Business;
using Core.Helper;
using Data.Context;
using Data.Custom;
using Data.Entities.Base;
using Data.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Data.Entities.ExFit;

namespace ExFit.Controllers
{
    public class _BaseController : Controller
    {
        private Db _db;
        protected DbIncludeHelper _includeHelper;
        public CookieHelper cookieHelper { get; set; }
        private readonly IConfiguration _config;
        protected IWebHostEnvironment _webHostEnv { get; set; }
        protected IHttpContextAccessor _httpContextAccessor { get; set; }
        public MailManager MailM { get; set; }
        public MemberManager MemberM { get; set; }
        public SmsManager SmsM { get; set; }
        public TaskManager TaskM { get; set; }
        public UserManager UserM { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ControllerName = context.RouteData.Values["controller"].ToString();
            var ActionName = context.RouteData.Values["action"].ToString();

            if (ControllerName != "LogIn")
            {
                if (CurrentUser != null)
                {
                    if (ID == 0)
                        context.Result = LocalRedirect("/LogIn/SignIn");

                    else
                        base.OnActionExecuting(context);
                }

                else
                    context.Result = LocalRedirect("/LogIn/SignIn");
            }
        }

        public _BaseController(IConfiguration config, IWebHostEnvironment webHostEnv, IHttpContextAccessor httpContextAccessor)
        {
            cookieHelper = new CookieHelper();
            _db = new Db();
            _config = config;
            _webHostEnv = webHostEnv;
            _httpContextAccessor = httpContextAccessor;
            _includeHelper = new DbIncludeHelper();
            MailM = new MailManager();
            MemberM = new MemberManager();
            SmsM = new SmsManager();
            TaskM = new TaskManager();
            UserM = new UserManager();
        }


        public Db Db
        {
            get
            {
                return (_db == null) ? new Db() : _db;
            }
        }
        public int ID
        {
            get
            {
                return (int)HttpContext.Session.GetInt32("ID");
            }
        }
        public User CurrentUser
        {
            get
            {
                return Db.User.Find(ID);
            }
        }
        public string EnvironmentV
        {
            get
            {
                return _config.GetValue<string>("Environment");
            }
        }
        public async Task<List<dynamic>> RestrictionControlActionList(string pre = "Development", string nameSpace = "ExFit.Controllers")
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace == nameSpace)
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        ReturnType = x.ReturnType.Name,
                        Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
                        Attribute = x.GetCustomAttributes().FirstOrDefault(f => f.GetType().Name == pre + "Attribute"),
                        NameSpace = nameSpace
                    })
                    .Where(x => x.Attributes.Contains(pre))
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList<dynamic>();
        }
    }

    public class BaseController<T> : _BaseController where T : BaseEntity
    {
        public IHttpContextAccessor _http { get; set; }
        public HttpRequest _request { get; set; }
        public DbSet<T> dbset;
        public GenericRepository<T> repo = new GenericRepository<T>();
        private int CompanyId;
        private int UserId;
        public MailManager MailM { get; set; }
        public MemberManager MemberM { get; set; }
        public SmsManager SmsM { get; set; }
        public TaskManager TaskM { get; set; }
        public UserManager UserM { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ControllerName = context.RouteData.Values["controller"].ToString();
            var ActionName = context.RouteData.Values["action"].ToString();

            if (ControllerName != "LogIn")
            {
                if (CurrentUser != null)
                {
                    if (ID == 0)
                        context.Result = LocalRedirect("/LogIn/SignIn");

                    else
                        base.OnActionExecuting(context);
                }

                else
                    context.Result = LocalRedirect("/LogIn/SignIn");
            }
        }

        public BaseController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, webHostEnvironment, httpcontext)
        {
            _http = httpcontext;
            _request = _http.HttpContext.Request;
            dbset = Db.Set<T>();
            MailM = new MailManager();
            MemberM = new MemberManager();
            SmsM = new SmsManager();
            TaskM = new TaskManager();
            UserM = new UserManager();
        }

        public BaseController(IConfiguration config, IWebHostEnvironment webHostEnv, IHttpContextAccessor httpContextAccessor) : base(config, webHostEnv, httpContextAccessor)
        {
        }

        // Single Tasks
        public virtual async Task<JsonResult> Save(T form)
        {
            var response = new Response<List<string>>();
            CompanyId = CurrentUser.CompanyId;
            UserId = CurrentUser.UserId;

            switch (form)
            {
                case Diet obj:
                    await TaskM.Build(CompanyId, 6, 0, UserId);
                    break;
                case Excersize obj:
                    await TaskM.Build(CompanyId, 5, 0, UserId);
                    break;

                case Member obj:
                    if (obj.tempId > 0)
                        await TaskM.Build(CompanyId, 0, obj.tempId, UserId);

                    await TaskM.Build(CompanyId, 1, obj.tempId, UserId);
                    break;

                case User obj:
                    if (obj.tempId > 0)
                        await TaskM.Build(CompanyId, 4, 0, UserId);

                    await TaskM.Build(CompanyId, 10, 0, UserId);
                    break;
            }
            if (form.tempId == 0)
            {
                form.CreateDate = DateTime.Now;
                form.Active = true;
                form.Deleted = false;
                dbset.Add(form);
                await Db.SaveChangesAsync();
                response.Success = true;
                response.Description = "Kayıt edildi.";
            }
            else
            {
                T entity = null;

                var model = dbset.AsQueryable();
                var dbInclude = _includeHelper.GetField(typeof(T).Name);

                if (dbInclude != null)
                {
                    foreach (var include in dbInclude)
                    {
                        model = model.Include(include);
                    }
                    entity = model.FirstOrDefault(o => EF.Property<int>(o, Db.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0].Name) == form.tempId);
                }
                else
                    entity = dbset.Find(form.tempId);


                if (entity != null)
                {
                    var propList = entity.GetType().GetProperties().Where(prop => !prop.IsDefined(typeof(NotMappedAttribute), false)).ToList();
                    foreach (var prop in propList)
                    {
                        if (form.Include.Contains(prop.Name))
                            prop.SetValue(entity, form.GetType().GetProperty(prop.Name).GetValue(form, null));
                    }
                    entity.UpdateDate = DateTime.Now;

                    var relatedPropList = entity.GetType().GetProperties().Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)).ToList();
                    foreach (var relatedProp in relatedPropList)
                    {
                        var relatedData = form.GetType().GetProperty(relatedProp.Name).GetValue(form, null);
                        if (relatedData != null)
                        {
                            var _relatedEntityList = JsonConvert.SerializeObject(relatedData, new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
                            var elementType = relatedProp.PropertyType.GetGenericArguments()[0];
                            var listType = typeof(List<>).MakeGenericType(elementType);
                            var relatedEntityList = (ICollection)JsonConvert.DeserializeObject(_relatedEntityList, listType);

                            relatedProp.PropertyType.GetMethod("Clear").Invoke(relatedEntityList, null);

                            foreach (var data in (IEnumerable)relatedData)
                            {
                                relatedEntityList.GetType().GetMethod("Add").Invoke(relatedEntityList, new[] { data });
                            }

                            relatedProp.SetValue(entity, relatedEntityList);
                        }
                    }
                    await Db.SaveChangesAsync();
                    response.Success = true;
                    response.Description = "Güncellendi.";
                }
                else
                {
                    response.Success = false;
                    response.Description = "kayıt bulunamadı.";
                }
            }

            return Json(response);
        }
        public virtual async Task<JsonResult> Active(int id)
        {
            var response = new Response();
            CompanyId = CurrentUser.CompanyId;
            UserId = CurrentUser.UserId;
            var model = dbset.Find(id);
            switch (model)
            {
                case Member obj:
                    await TaskM.Build(CompanyId, 3, obj.MemberId, UserId);
                    break;
            }
            if (model != null)
            {
                var status = model.Active == true ? false : true;
                var title = model.Active == true ? "Pasif" : "Aktif";
                model.Active = status;

                if (status == true)
                    model.Deleted = false;

                await Db.SaveChangesAsync();
                response.Success = true;
                response.Description = "kayıt " + title + " edildi";
                return Json(response);
            }
            response.Success = false;
            response.Description = "kayıt bulunamadı";
            return Json(response);
        }
        public virtual async Task<JsonResult> Delete(int id)
        {
            var response = new Response();
            CompanyId = CurrentUser.CompanyId;
            UserId = CurrentUser.UserId;
            var model = dbset.Find(id);
            switch (model)
            {
                case Member obj:
                    await TaskM.Build(CompanyId, 7, obj.MemberId, UserId);
                    break;

                case User obj:
                    await TaskM.Build(CompanyId, 11, 0, UserId);
                    break;
            }
            if (model != null)
            {
                model.Active = false;
                model.Deleted = true;
                await Db.SaveChangesAsync();
                response.Success = true;
                response.Description = "kayıt silindi";
                return Json(response);
            }

            response.Success = false;
            response.Description = "kayıt bulunamadı";
            return Json(response);
        }

        // Multiple Tasks
        public virtual async Task<JsonResult> MultiDelete(List<int> selectedValues)
        {
            var response = new Response();
            int count = 0;
            if (selectedValues.Count() > 0)
            {
                foreach (var item in selectedValues)
                {
                    try
                    {
                        var entity = repo.Get(item);
                        entity.Deleted = true;
                        entity.Active = false;
                        repo.Update(entity);
                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                response.Success = true;
                response.Description = $"{count} adet kayıt silindi. başarısız {selectedValues.Count() - count}";
            }
            else
            {
                response.Success = false;
                response.Description = "en az bir adet kayıt seçmeniz gerekiyor.";
            }
            return Json(response);

        }
        public virtual async Task<JsonResult> MultiActive(List<int> selectedValues)
        {
            var response = new Response();
            int count = 0;
            if (selectedValues.Count() > 0)
            {
                foreach (var item in selectedValues)
                {
                    try
                    {
                        var entity = repo.Get(item);
                        entity.Deleted = false;
                        entity.Active = true;
                        repo.Update(entity);
                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                response.Success = true;
                response.Description = $"{count} adet kayıt aktifleştirildi. başarısız {selectedValues.Count() - count}";
            }
            else
            {
                response.Success = false;
                response.Description = "en az bir adet kayıt seçmeniz gerekiyor.";
            }

            return Json(response);
        }
        public virtual async Task<JsonResult> MultiPassive(List<int> selectedValues)
        {
            var response = new Response();
            int count = 0;
            if (selectedValues.Count() > 0)
            {
                foreach (var item in selectedValues)
                {
                    try
                    {
                        var entity = repo.Get(item);
                        entity.Active = false;
                        repo.Update(entity);
                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                response.Success = true;
                response.Description = $"{count} adet kayıt pasifleştirildi. başarısız {selectedValues.Count() - count}";
            }
            else
            {
                response.Success = false;
                response.Description = "en az bir adet kayıt seçmeniz gerekiyor.";
            }

            return Json(response);
        }

        // Actions
        public virtual IActionResult Form(int id = 0, int relationId = 0)
        {
            T model = null;
            var list = dbset.AsQueryable();

            var dbInclude = _includeHelper.GetField(typeof(T).Name);

            if (dbInclude != null)
            {
                foreach (var include in dbInclude)
                {
                    list = list.Include(include);
                }
                model = list.FirstOrDefault(o => EF.Property<int>(o, Db.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0].Name) == id);
            }
            else
                model = dbset.Find(id);

            ViewBag.relationId = relationId;

            return PartialView(model);
        }
        public virtual IActionResult List()
        {
            return PartialView();
        }
        public virtual IActionResult GetList(int page = 1, int adet = 10)
        {
            return GetListModel(repo.GetAll(x => !x.Deleted), page, adet);
        }
        public IActionResult GetListModel(IQueryable<T> model, int page = 1, int adet = 10, List<string> Filter = null)
        {
            var searchText = Request.Query["searchText"].ToString();
            var orderBy = Request.Query["orderBy"].ToString();
            var orderWay = Request.Query["orderWay"].ToString();
            var active = (Request.Query["active"].ToString() == "1") ? false : true;

            page = (page < 1) ? 1 : page;

            var count = model.Count();
            var pager = new Pager(count, page, adet);
            pager.SearchText = searchText;

            if (!string.IsNullOrEmpty(orderBy))
            {
                var _orderWay = !string.IsNullOrEmpty(orderWay) ? orderWay : "Desc";
                model = model.OrderBy(orderBy + " " + _orderWay);
            }
            else
                model = model.OrderByDescending(x => x.CreateDate);

            model = model.Where(x => x.Active == active);

            model = model.Skip((page - 1) * adet).Take(adet);

            ViewBag.Pager = pager;
            ViewBag.Toplam = count;

            return PartialView(model.Where(x => !x.Deleted).ToList());
        }
    }
}