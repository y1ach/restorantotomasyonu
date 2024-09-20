using System;
namespace RestaurantOtomasyonu
{
    internal class Program
    {
        static Masa[] masalar = new Masa[5];
        static Menu menu = new Menu();

        static void Main(string[] args)
        {

            for (int i = 0; i < masalar.Length; i++)
            {
                masalar[i] = new Masa();
            }
            while (true)
            {
                Console.WriteLine("\n***** RESTORANT OTOMASYONU *****");
                Console.WriteLine("1- Sipariş Al");
                Console.WriteLine("2- Hesap Al");
                Console.WriteLine("3- Menü Düzenle");
                Console.WriteLine("4- Çıkış");
                Console.Write("Seçiminiz: ");
                int secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        SiparisAl();
                        break;
                    case 2:
                        HesapAl();
                        break;
                    case 3:
                        Console.WriteLine("Menü düzenleme bölümü geliştirme aşamasında anlayışınız için teşekkürler :)");
                        break;
                    case 4:
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                        break;
                }
            }
        }
        static void SiparisAl()
        {
            int bosMasa = IlkBosMasaBul();
            if (bosMasa == -1)
            {
                Console.WriteLine("Tüm masalar dolu!");
                return;
            }

            Console.Write("Kaç kişisiniz? ");
            int kisiSayisi = int.Parse(Console.ReadLine());
            masalar[bosMasa].KisiSayisi = kisiSayisi;

            for (int i = 0; i < kisiSayisi; i++)
            {
                bool devam = true;
                while (devam)
                {
                    menu.MenuYazdir();
                    Console.Write("Yemek seçin (numara girin): ");
                    int yemekSecimi = int.Parse(Console.ReadLine()) - 1;

                    if (yemekSecimi >= 0 && yemekSecimi < menu.Yemekler.Length)
                    {
                        int fiyat = menu.YemekFiyatiniAl(yemekSecimi);
                        masalar[bosMasa].SiparisEkle(fiyat);
                        Console.WriteLine($"{menu.Yemekler[yemekSecimi]} sipariş edildi.");
                    }

                    Console.Write("Başka bir arzunuz var mı? (Evet/Hayır): ");
                    string cevap = Console.ReadLine().ToLower();
                    devam = (cevap == "evet");
                }
            }
            Console.WriteLine($"Sipariş alındı! Masa {bosMasa + 1}");
        }

        static void HesapAl()
        {
            Console.Write("Hesap almak istediğiniz masa numarası: ");
            int masaNo = int.Parse(Console.ReadLine()) - 1;

            if (masalar[masaNo].KisiSayisi > 0)
            {
                Console.WriteLine($"Masa {masaNo + 1} için toplam hesap: {masalar[masaNo].Hesap} TL");
                masalar[masaNo].MasayiTemizle();
            }
            else
            {
                Console.WriteLine("Bu masa boş.");
            }
        }

        static int IlkBosMasaBul()
        {
            for (int i = 0; i < masalar.Length; i++)
            {
                if (masalar[i].KisiSayisi == 0)
                    return i;
            }
            return -1;
        }
    }
    class Masa
    {
        public int KisiSayisi { get; set; }
        public int Hesap { get; set; }

        public Masa()
        {
            KisiSayisi = 0;
            Hesap = 0;
        }

        public void SiparisEkle(int fiyat)
        {
            Hesap += fiyat;
        }

        public void MasayiTemizle()
        {
            KisiSayisi = 0;
            Hesap = 0;
        }
    }
    class Menu
    {
        public string[] Yemekler = { "Kebap", "Pizza", "Makarna", "Salata", "Tatlı" };
        public int[] Fiyatlar = { 50, 45, 30, 20, 15 };

        public void MenuYazdir()
        {
            Console.WriteLine("\n***** MENÜ *****");
            for (int i = 0; i < Yemekler.Length; i++)
            {
                Console.WriteLine($"{i + 1}- {Yemekler[i]} - {Fiyatlar[i]} TL");
            }
        }

        public int YemekFiyatiniAl(int yemekSecimi)
        {
            return Fiyatlar[yemekSecimi];
        }
    }
}
