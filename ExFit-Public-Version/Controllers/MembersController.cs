using Microsoft.AspNetCore.Mvc;
using Data.Entities.ExFit;
using Core.Attributes;

namespace ExFit.Controllers
{
    public class MembersController : BaseController<Member>
    {
        public MembersController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }

        // Actions
        public IActionResult AllMembers()
        {
            return View();
        }
        public IActionResult AllPassivedMembers()
        {
            return View();
        }
        public IActionResult OpenedMember(int id)
        {
            return View(repo.Get(id));
        }
        public IActionResult CheckInMember(int id)
        {
            return PartialView(repo.Get(id));
        }
        public IActionResult MemberAddMeazurements(int id)
        {
            ViewBag.MemberWeightArray = MemberM.GetWeightsArray(id);
            return View(repo.Get(id));
        }


        // Tasks
        [HttpPost]
        public async Task<JsonResult> Save(Member form)
        {
            form.IMG = await MemberM.UploadFile(form.FileAvatarIMG, "wwwroot/Member/ProfilePhotos");
            form.HealthReport = await MemberM.UploadFile(form.FileHealthReport, "wwwroot/Member/HealthReports");
            form.IdentityCard = await MemberM.UploadFile(form.FileIdentityCard, "wwwroot/Member/IdentityCards");

            int MemberId = form.MemberId;
            int Count = await MemberM.Save(form);

            if (Count > 0)
                await TaskM.Build(form.CompanyId, 8, form.MemberId, ID);

            else if (MemberId == 0)
                await TaskM.Build(form.CompanyId, 0, form.MemberId, ID);

            else
                await TaskM.Build(form.CompanyId, 1, form.MemberId, ID);

            return JsonAttribute.Ok;
        }
        [HttpPost]
        public async Task<JsonResult> SaveMemberMeazurements(MemberMeazurement form)
        {
            MemberM.SaveMeazurements(form);
            return JsonAttribute.Ok;
        }
        [HttpPost]
        public async Task<JsonResult> DeleteMemberMeazurements(int id)
        {
            MemberM.DeleteMeazurements(id);
            return JsonAttribute.Ok;
        }
        [HttpPost]
        public async Task<JsonResult> PassiveMember(int id)
        {
            MemberM.Delete(id);
            await TaskM.Build(CurrentUser.CompanyId, 2, id, ID);

            return JsonAttribute.Ok;
        }
        [HttpPost]
        public async Task<JsonResult> DeleteMember(int id)
        {
            MemberM.Delete(id, true);
            await TaskM.Build(CurrentUser.CompanyId, 7, id, ID);
            return JsonAttribute.Ok;
        }
        [HttpPost]
        public async Task<JsonResult> DeleteMemberExcersize(int id)
        {
            return await RemoveProp(id, 2);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteMemberDiet(int id)
        {
            return await RemoveProp(id, 1);
        }
        private async Task<JsonResult> RemoveProp(int id, int Type)
        {
            var Member = repo.Get(id);

            switch (Type)
            {
                case 1:
                    Member.ExcersizeId = 0;
                    break;
                case 2:
                    Member.DietId = 0;
                    break;
            }
            Db.Member.Update(Member);
            Db.SaveChangesAsync();

            return JsonAttribute.Ok;
        }
    }
}