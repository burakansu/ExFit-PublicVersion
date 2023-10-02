using Task = Data.Entities.ExFit.Task;

namespace Business
{
    public class TaskManager : ManagerBase
    {
        public async Task<bool> Build(int companyId, int descType, int memberId, int userId)
        {
            string[] descriptions = {
            "Yeni Üye Kayıt Edildi",
            "Üye Güncellendi",
            "Üye Kontratı Sonlandı!",
            "Üye Kontratı Yenilendi",
            "Yeni Çalışan İşe Alındı",
            "Yeni Egzersiz Programı",
            "Yeni Diyet Programı",
            "Üye Kaydı Silindi",
            "Üye Kaydedilemedi Zaten Kayıtlı Email Veya Telefon",
            "Personel Kaydedilemedi Zaten Kayıtlı Email Veya Telefon",
            "Personel Güncellendi",
            "Personel İşten Çıkarıldı"
            };

            if (descType < 0 || descType >= descriptions.Length)
                throw new ArgumentOutOfRangeException(nameof(descType), "Geçersiz descType değeri.");

            Db.Task.Add(new Task
            {
                Description = descriptions[descType],
                CreateDate = DateTime.Now,
                UserId = userId,
                MemberId = (memberId == 0) ? Db.Member.First().MemberId : memberId,
                CompanyId = companyId
            });

            try
            {
                Db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
