using Microsoft.AspNetCore.Mvc;

namespace Core.Attributes
{
    public static class JsonAttribute
    {
        public static JsonResult Ok
        {
            get
            {
                return new JsonResult(new { Success = true });
            }
        }
        public static JsonResult Error
        {
            get
            {
                return new JsonResult(new { Success = false });
            }
        }
    }
}