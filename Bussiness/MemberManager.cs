using Data.Context;
using Data.Entities.ExFit;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace Business
{
    public class MemberManager : ManagerBase
    {
        public async Task<string> UploadFile(IFormFile file, string uploadFolder)
        {
            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), uploadFolder, imageName);

                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                return $"/{uploadFolder}/{imageName}";
            }

            return $"/{uploadFolder}/AvatarNull.png";
        }
        public async Task SendSmsMember(Member Member, string SmsText)
        {
            Company Company = Db.Company.Single(x => x.CompanyId == Member.CompanyId);
            if (Company.PackageType > 0)
            {
                List<Member> Members = new List<Member>();
                Members.Add(Member);
                new SmsManager().SmsSender(Company.Name, SmsText, Members);
            }
        }
        public Member CheckMemberEntering(Member member)
        {
            Member _User = Db.Member.SingleOrDefault(x => x.Mail == member.Mail && x.Password == member.Password);

            if (_User != null)
                return _User;
            member.MemberId = 0;
            return member;
        }
        public int CheckEmailAndPhone(string Email, string Phone)
        {
            return Db.Member.Where(x => x.Mail == Email || x.Phone == Phone).Count();
        }
        public int Count(int CompanyId)
        {
            return Db.Member.Where(x => x.CompanyId == CompanyId).Count();
        }
        public async Task Delete(int id, bool Del = false)
        {
            if (Del == true)
            {
                Db.Task.RemoveRange(Db.Task.Where(x => x.MemberId == id).ToList());
                Db.Member.Remove(Db.Member.Single(x => x.MemberId == id));
            }
            else
            {
                Member Member = Db.Member.Single(x => x.MemberId == id);
                string CompanyName = Db.Company.Single(x => x.CompanyId == Member.CompanyId).Name;
                Member.Block = 1;
                Member.PackageId = 0;
                Member.Gift = 0;
                Member.Price = 0;
                Db.Member.Update(Member);
                string Message = "Üyeliğiniz " + DateTime.Now.ToShortDateString() + " itibarıyla Pasif Durumdadır. Daha fazla bilgi için Exfit Yönetim Panelinden Üye Girişi Yapabilirsiniz.";
                SendSmsMember(Member, Message);
                new MailManager().Send(CompanyName, Message, Member);
            }
            Db.SaveChanges();
        }
        public async Task<int> Save(Member Member)
        {
            int Count = 0;
            Package Package = new Package();

            if (Member.PackageId != 0)
                Package = Db.Packages.Single(x => x.PackageId == Member.PackageId);
            else
                Package = new Package { Month = 0, Price = 0 };

            if (Member.MemberId != 0)
            {
                Member.RegistrationTime = Member.RegistrationDate.AddMonths(Package.Month + Member.Gift);
                if (Member.Block == 1)
                    Member.RegistrationDate = DateTime.Now;
                Member.Block = 0;
                Db.Update(Member);
            }
            else
            {
                Count = new MemberManager().CheckEmailAndPhone(Member.Mail, Member.Phone);
                if (Count == 0)
                {
                    Company Company = Db.Company.Single(x => x.CompanyId == Member.CompanyId);
                    Member.RegistrationDate = DateTime.Now;
                    Member.RegistrationTime = DateTime.Now.AddMonths(Package.Month + Member.Gift);
                    Member.Password = DateTime.Now.Year + (Db.Member.Where(x => x.CompanyId == Member.CompanyId).Count() + 1).ToString();
                    Member.Block = 1;
                    Db.Add(Member);
                    string Message = "Hoşgeldin! Üyeliğiniz " + DateTime.Now.ToShortDateString() + " İtibarı İle Başlamıştır. Kalan Gününüz: " + 0 + " Daha fazla bilgi için Exfit İle Üye Girişi Yapabilirsiniz. Mail:" + Member.Mail + " Şifre: " + Member.Password;
                    SendSmsMember(Member, Message);
                    new MailManager().Send(Company.Name, Message, Member);
                }
            }
            Db.SaveChanges();

            return Count;
        }
        public async Task SaveMeazurements(MemberMeazurement MemberMeazurement)
        {
            MemberMeazurement.WhichMonth = Db.MemberMeazurement.Count(x => x.MemberId == MemberMeazurement.MemberId) + 1;
            Db.MemberMeazurement.Update(MemberMeazurement);
            Db.SaveChanges();
        }
        public async Task DeleteMeazurements(int id)
        {
            Db.MemberMeazurement.Remove(Db.MemberMeazurement.Single(x => x.MemberMeazurementId == id));
            Db.SaveChanges();
        }
        public async Task<List<MemberMeazurement>> GetMeazurements(int id)
        {
            return Db.MemberMeazurement.Where(x => x.MemberId == id).ToList();
        }
        public int GetIncome(int CompanyId)
        {
            return Db.Income.Where(x => x.CompanyId == CompanyId && x.Year == DateTime.Now.Year).Sum(x => x.Value);
        }
        public double[] GetWeightsArray(int id)
        {
            int[] Array = Db.MemberMeazurement.Where(x => x.MemberId == id).Select(x => x.Weight).ToArray();

            double[] Weights = new double[Array.Count()];
            double[] WeightsAndCurve = new double[12];
            if (Array.Count() > 0)
            {
                for (int i = 0; i < Array.Count(); i++)
                {
                    Weights[i] = Convert.ToDouble(Array[i]);
                    WeightsAndCurve[i] = Weights[i];
                }
                LinearCurve linearCurve = new LinearCurve();
                if (Weights.Count() > 3)
                {
                    double[] Lcurve = linearCurve.Curve(id, 12 - Weights.Count());

                    int counter = 0;
                    int Total = (Weights.Count() + Lcurve.Count());
                    for (int i = Weights.Count(); i < Total; i++)
                    {
                        WeightsAndCurve[i] = Lcurve[counter];
                        counter++;
                    }
                }
            }
            return WeightsAndCurve;
        }
        public int[] GetThisYearRegistry(int CompanyId)
        {
            int[] Months = new int[12];
            var Members = Db.Member.Where(x => x.RegistrationDate.Year == DateTime.Now.Year && x.CompanyId == CompanyId);

            foreach (var item in Members)
            {
                Months[item.RegistrationDate.Month - 1] += 1;
            }

            return Months;
        }
        public async Task PasiveMemberAuto()
        {
            List<Member> Members = Db.Member.Where(x => x.RegistrationTime < DateTime.Now && x.Block == 0).ToList();
            foreach (var item in Members)
            {
                await Delete(item.MemberId);
            }
        }
    }
    public class LinearCurve : ManagerBase
    {
        private Double a, b, Xi_Avg, Yi_Avg, SSA = 0;
        private Double[] Xi, Yi, Yi_Tilda;
        private int n;
        void Avg_Y()
        {
            Double Counter = 0;
            for (int i = 0; i < n; i++)
            {
                Counter += Yi[i];
            }
            Yi_Avg = (Counter / n);
        }
        void Avg_X()
        {
            Double Counter = 0;
            for (int i = 0; i < n; i++)
            {
                Counter += Xi[i];
            }
            Xi_Avg = (Counter / n);
        }
        void Method_a()
        {
            a = Yi_Avg - (b * Xi_Avg);
        }
        void Method_b()
        {
            Avg_X();
            Avg_Y();
            Double Nominator = 0;
            Double Denominator = 0;
            for (int i = 0; i < n; i++)
            {
                Nominator += (Xi[i] - Xi_Avg) * (Yi[i] - Yi_Avg);
                double sum = (Xi[i] - Xi_Avg);
                Denominator += (Double)Math.Pow(sum, 2);
            }
            b = Nominator / Denominator;
        }
        void Method_SSA()
        {
            for (int i = 0; i < n; i++)
            {
                SSA += (Double)Math.Pow((Yi[i] - a - (b * Xi[i])), 2);
            }
        }
        public Double[] Curve(int ID, int Total)
        {
            if (Total >= 9)
            {
                Yi_Tilda = new Double[Total];
                return Yi_Tilda;
            }

            int[] Array = Db.MemberMeazurement.Where(x => x.MemberId == ID).Select(x => x.Weight).ToArray();
            n = Array.Count();
            Yi_Tilda = new Double[Total];
            Xi = new Double[12];
            Yi = new Double[n];
            if (n > 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    Xi[j] = j + 1;
                }
                for (int i = 0; i < n; i++)
                {
                    Yi[i] = Convert.ToDouble(Array[i]);
                }
            }

            Method_b();
            Method_a();
            Method_SSA();
            for (int i = 0; i < Total; i++)
            {
                Yi_Tilda[i] = Yi_Avg - (b * Xi_Avg) + (b * Xi[i]);
            }
            return Yi_Tilda;
        }
    }
    public class QuadraticCurve : ManagerBase
    {
        Double[] Xi, Yi, Yi_Tilda, Z;
        Double a, b, c, d;
        int n;
        private void Method_B()
        {
            Double T = 0;
            Double Xi2 = 0;
            for (int i = 0; i < n; i++)
            {
                T += Xi[i] * Math.Log10(Yi[i]);
                Z[i] = Math.Log10(Yi[i]);
                Xi2 += Math.Pow(Xi[i], 2);
            }

            b = (n * T - Xi.Sum() * Z.Sum()) / (n * Xi2 - Math.Pow(Xi.Sum(), 2));
        }
        private void Method_A()
        {
            a = (Z.Sum() / Z.Length) - b * (Xi.Sum() / Xi.Length);
            c = Math.Pow(10, a);
            d = Math.Pow(10, b);
        }
        public Double[] Curve(int MemberId, int Total)
        {
            int[] Array = Db.MemberMeazurement.Where(x => x.MemberId == MemberId).Select(x => x.Weight).ToArray();

            n = Array.Count();
            Yi_Tilda = new Double[Total];
            Xi = new Double[12];
            Yi = new Double[n];
            Z = new Double[n];
            if (n > 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    Xi[j] = j + 1;
                }
                for (int i = 0; i < n; i++)
                {
                    Yi[i] = Convert.ToDouble(Array[i]);
                }
            }
            Method_B();
            Method_A();
            for (int i = 0; i < Total; i++)
            {
                Yi_Tilda[i] = c * Math.Pow(d, i + (12 - Total));
            }
            return Yi_Tilda;
        }
    }
}