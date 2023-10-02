using System.Reflection;

namespace Core.Helper
{
    public class DbIncludeHelper
    {
        //public List<string> Member()
        //{
        //    var list = new List<string>();
        //    list.Add("MemberRelation");
        //    return list;
        //}
       

        public List<string> GetField(string name)
        {
            try
            {
                MethodInfo mi = this.GetType().GetMethod(name);

                return (mi == null) ? null : (List<string>)(mi.Invoke(this, null));
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
