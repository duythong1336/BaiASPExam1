using Microsoft.EntityFrameworkCore;
using NguyenMinhDuyThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenMinhDuyThong.Repository
{
    public class PhoneRepository

    {
        private readonly MainDbContext _context = null;
        public async Task<List<PhoneModels>> GetAllPhones()
        {
            var phones = new List<PhoneModels>();
            List<PhoneModels> allphones = await _context.Phones.ToListAsync();
            if (allphones?.Any() == true)
            {
                foreach (var phone in allphones)
                {
                    phones.Add(new PhoneModels()
                    {
                        IdPro = phone.IdPro,
                        NamePro = phone.NamePro,
                        Price = phone.Price

                    }); 
                }
            }
            return DataSource();
        }
        public PhoneModels GetPhoneByID(int id)
        {
            return DataSource().Where(x => x.IdPro == id).FirstOrDefault();
        }
        public List<PhoneModels> SearchPhone(String title)
        {
            return DataSource().Where(x => x.NamePro == title).ToList();
        }
        private List<PhoneModels> DataSource()
        {
            return new List<PhoneModels>()
            {
                new PhoneModels() {IdPro = 1, NamePro="Iphone12", Price = 24000000 , Description="Apple Inc. El iPhone 12 es un teléfono inteligente de gama alta con pantalla táctil producido por Apple, Inc. ..."},
                new PhoneModels() {IdPro = 2 , NamePro = "SamsungGalaxyNote 10", Price = 22000000, Description="Samsung Galaxy Note 10 is powered by a 1.9GHz octa-core Samsung Exynos 9825 processor that features 4 cores clocked at 1.9GHz, 2 cores clocked at 2.4GHz and 2 cores clocked at 2.7GHz"},
                new PhoneModels() {IdPro = 3, NamePro="Iphone XS max", Price=15400000,Description="The iPhone XS and iPhone XS Max are smartphones designed, developed and marketed by ... iPhone XS, XS Max, XR specs: Battery size"},
                new PhoneModels() {IdPro = 4, NamePro="Oppo Reno5",Price = 11200000,Description="Oppo Neo 5 (2015) Android smartphone. Announced Jun 2015. Features ... Disclaimer. We can not guarantee that the information on this page is 100% correct."},
                new PhoneModels() {IdPro = 5, NamePro="SamsungGalaxyNote 9", Price= 16400000,Description="The Samsung Galaxy Note 9 is an Android-based phablet designed, developed, produced and marketed by Samsung Electronics as part of the Samsung"},
                new PhoneModels() {IdPro = 6, NamePro="Iphone 11", Price=21000000, Description="The iPhone 11 has a 6.1 in (15.5 cm) IPS LCD, unlike the Pro models which have OLED displays. The resolution is 1792 × 828 pixels (1.5 megapixels at 326 ppi) with a maximum brightness of 625 nits and a 1400:1 contrast ratio. It supports Dolby Vision"}

            };
        }
    }
}
