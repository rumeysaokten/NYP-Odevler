using System;
using System.Collections.Generic;
using System.Text;

namespace AkilliEv
{
    // 1. Cihaz türlerini tutan enum
    enum CihazTuru
    {
        Lamba,
        Klima,
        GuvenlikSistemi
    }

    // 2. Cihaz sınıfı
    class Cihaz
    {
        // 3. Her cihazın Kimlik, Ad, Tur ve AcikMi bilgileri var
        public int Kimlik { get; set; }
        public string Ad { get; set; }
        public CihazTuru Tur { get; set; }
        public bool AcikMi { get; set; }

        // 4. Bilgiler constructor ile veriliyor, cihaz başta kapalı
        public Cihaz(int kimlik, string ad, CihazTuru tur)
        {
            Kimlik = kimlik;
            Ad = ad;
            Tur = tur;
            AcikMi = false;
        }

        // 5. Açma ve kapatma metotları
        public void Ac()
        {
            if (AcikMi == true)
            {
                Console.WriteLine(Ad + " zaten açık.");
            }
            else
            {
                AcikMi = true;
                Console.WriteLine(Ad + " açıldı.");
            }
        }

        public void Kapat()
        {
            if (AcikMi == false)
            {
                Console.WriteLine(Ad + " zaten kapalı.");
            }
            else
            {
                AcikMi = false;
                Console.WriteLine(Ad + " kapatıldı.");
            }
        }

        // 6. Cihaz bilgilerini ekrana yazdıran metot
        public void BilgileriGoster()
        {
            string durum;
            if (AcikMi)
            {
                durum = "Açık";
            }
            else
            {
                durum = "Kapalı";
            }

            Console.WriteLine("Kimlik: " + Kimlik + " | Ad: " + Ad + " | Tür: " + Tur + " | Durum: " + durum);
        }
    }

    class Program
    {
        // Kimliğe göre cihaz bulan metot, bulamazsa null döner
        static Cihaz CihazBul(List<Cihaz> cihazlar, int kimlik)
        {
            foreach (Cihaz c in cihazlar)
            {
                if (c.Kimlik == kimlik)
                {
                    return c;
                }
            }
            return null;
        }

        static void CihazlariListele(List<Cihaz> cihazlar)
        {
            if (cihazlar.Count == 0)
            {
                Console.WriteLine("Sistemde hiç cihaz yok.");
                return;
            }

            Console.WriteLine("--- Cihaz Listesi ---");
            foreach (Cihaz c in cihazlar)
            {
                c.BilgileriGoster();
            }
        }

        static void CihazEkle(List<Cihaz> cihazlar)
        {
            Console.Write("Cihaz kimliği: ");
            int kimlik;
            if (!int.TryParse(Console.ReadLine(), out kimlik))
            {
                Console.WriteLine("Hata: Kimlik sayı olmalıdır!");
                return;
            }

            // 9. Aynı kimlikle iki cihaz eklenemesin
            if (CihazBul(cihazlar, kimlik) != null)
            {
                Console.WriteLine("Hata: Bu kimliğe sahip bir cihaz zaten var!");
                return;
            }

            Console.Write("Cihaz adı: ");
            string ad = Console.ReadLine();

            Console.WriteLine("Cihaz türü seçiniz:");
            Console.WriteLine("1 - Lamba");
            Console.WriteLine("2 - Klima");
            Console.WriteLine("3 - Güvenlik Sistemi");
            Console.Write("Seçiminiz: ");
            string turSecim = Console.ReadLine();

            CihazTuru tur;
            if (turSecim == "1")
            {
                tur = CihazTuru.Lamba;
            }
            else if (turSecim == "2")
            {
                tur = CihazTuru.Klima;
            }
            else if (turSecim == "3")
            {
                tur = CihazTuru.GuvenlikSistemi;
            }
            else
            {
                Console.WriteLine("Hata: Geçersiz cihaz türü!");
                return;
            }

            Cihaz yeniCihaz = new Cihaz(kimlik, ad, tur);
            cihazlar.Add(yeniCihaz);
            Console.WriteLine("Cihaz eklendi.");
        }

        static void CihazAc(List<Cihaz> cihazlar)
        {
            Console.Write("Açılacak cihazın kimliği: ");
            int kimlik;
            if (!int.TryParse(Console.ReadLine(), out kimlik))
            {
                Console.WriteLine("Hata: Kimlik sayı olmalıdır!");
                return;
            }

            Cihaz cihaz = CihazBul(cihazlar, kimlik);

            // 10. Olmayan cihaz için hata mesajı
            if (cihaz == null)
            {
                Console.WriteLine("Hata: " + kimlik + " kimlikli bir cihaz bulunamadı!");
            }
            else
            {
                cihaz.Ac();
            }
        }

        static void CihazKapat(List<Cihaz> cihazlar)
        {
            Console.Write("Kapatılacak cihazın kimliği: ");
            int kimlik;
            if (!int.TryParse(Console.ReadLine(), out kimlik))
            {
                Console.WriteLine("Hata: Kimlik sayı olmalıdır!");
                return;
            }

            Cihaz cihaz = CihazBul(cihazlar, kimlik);

            if (cihaz == null)
            {
                Console.WriteLine("Hata: " + kimlik + " kimlikli bir cihaz bulunamadı!");
            }
            else
            {
                cihaz.Kapat();
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 7. Cihazları List<Cihaz> içinde saklıyoruz
            List<Cihaz> cihazlar = new List<Cihaz>();

            // Başlangıçta birkaç akıllı cihaz olsun
            cihazlar.Add(new Cihaz(1, "Salon Lambası", CihazTuru.Lamba));
            cihazlar.Add(new Cihaz(2, "Yatak Odası Kliması", CihazTuru.Klima));
            cihazlar.Add(new Cihaz(3, "Ev Güvenlik Sistemi", CihazTuru.GuvenlikSistemi));

            bool devam = true;

            // 8. Basit menü
            while (devam)
            {
                Console.WriteLine();
                Console.WriteLine("===== AKILLI EV MENÜSÜ =====");
                Console.WriteLine("1 - Cihazları listele");
                Console.WriteLine("2 - Yeni cihaz ekle");
                Console.WriteLine("3 - Cihaz aç");
                Console.WriteLine("4 - Cihaz kapat");
                Console.WriteLine("0 - Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();
                Console.WriteLine();

                switch (secim)
                {
                    case "1":
                        CihazlariListele(cihazlar);
                        break;
                    case "2":
                        CihazEkle(cihazlar);
                        break;
                    case "3":
                        CihazAc(cihazlar);
                        break;
                    case "4":
                        CihazKapat(cihazlar);
                        break;
                    case "0":
                        Console.WriteLine("Programdan çıkılıyor...");
                        devam = false;
                        break;
                    default:
                        Console.WriteLine("Hata: Geçersiz seçim, tekrar deneyin!");
                        break;
                }
            }
        }
    }
}
