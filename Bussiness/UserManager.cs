using Data.Entities.ExFit;

namespace Business
{
    public class UserManager : ManagerBase
    {
        private DateTime Now { get { return DateTime.Now; } }
        public User Check(User User)
        {
            User _User = Db.User.SingleOrDefault(x => x.Mail == User.Mail && x.Password == User.Password);

            if (_User != null)
            {
                DateTime dt = Db.Company.Single(x => x.CompanyId == _User.CompanyId).RegistrationTime;
                if (dt >= Now)
                    return _User;
            }
            User.UserId = 0;
            return User;
        }
        public int CheckEmail(string Email)
        {
            return Db.User.Where(x => x.Mail == Email).Count();
        }
        public int Save(User User, int first = 0)
        {
            int Count = 0;
            bool Update = false;
            string Message = "Hoşgeldin! Personel Kaydınız " + Now.ToShortDateString() + " İtibarı İle Başlamıştır. Daha fazla bilgi için Exfit İle Personel Girişi Yapabilirsiniz. Mail:" + User.Mail + " Şifre: " + User.Password;


            if (first == 0)
            {
                if (User.UserId != 0)
                {
                    Update = true;
                    Db.Update(User);
                }
                else
                {
                    Db.Add(User);
                    Company Company = Db.Company.Single(x => x.CompanyId == User.CompanyId);
                    new MailManager().Send(Company.Name, Message, null, User);
                }
            }
            else
            {
                Count = CheckEmail(User.Mail);
                if (Count == 0)
                {
                    var Company = new Company { Active = true, Deleted = false, CreateDate = Now, Name = User.IMG, RegistrationDate = Now, RegistrationTime = Now.AddYears(1) };
                    Db.Company.Add(Company);
                    User.CompanyId = Company.CompanyId;
                    User.Type = 1;
                    User.IMG = "/Personal/AvatarNull.png";
                    Db.User.Add(User);

                    if (first == 1)
                        Message = "Hoşgeldin! Spor Salonunuz " + Now.ToShortDateString() + " İtibarı İle Sisteme Kayıt Edilmiştir. Daha fazla bilgi için Exfit İle Personel Girişi Yapabilirsiniz.";

                    new MailManager().Send(Company.Name, Message, null, User);
                }
            }
            Db.SaveChanges();

            if (Update == true)
                return 2;

            return (Count == 0 && Update == false) ? 0 : 1;
        }
    }
}
