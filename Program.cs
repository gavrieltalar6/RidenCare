using System;

namespace RideNCare;

class Program
{
    public static int jumlahUser = 0;
    public static int pilihKendaraan = 0;
    public static int userAktif = -1;
    public static int pilihBengkel = 0;
    public static int pilihSlot = 0;
    public static int indexBengkel = 0;
    public static int[] bengkelBerSlot = new int[20];
    public static int jumlahBengkelBerSlot = 0;
    public static User[] pengguna = new User[20];

    public class User
    {
        public string Username;
        public string Email;
        public string Password;
        public bool Bengkel;
        public int jumlahKendaraan = 0;
        public Kendaraan[] myKendaraan = new Kendaraan[20];
        public InformasiBengkel infoBengkel;

        public User(string username, string email, string password, bool bengkel)
        {
            Username = username;
            Email = email;
            Password = password;
            Bengkel = bengkel;
            infoBengkel = new InformasiBengkel();

            infoBengkel.jadwalBooking = new JadwalBooking[20];

            for (int j = 0; j < infoBengkel.jadwalBooking.Length; j++)
            {
                infoBengkel.jadwalBooking[j] = new JadwalBooking();
            }
        }
    }

    public class InformasiBengkel
    {
        public string namaBengkel = "";
        public string alamatBengkel = "";
        public string kontakBengkel = "";
        public int bookingDibuat = 0;
        public bool adaYangSudahDiambil = false;
        public JadwalBooking[] jadwalBooking;
    }

    public class JadwalBooking
    {
        public DateTime jamHariBooking = default;
        public int durasiService = 0;
        public bool sudahDiambil = false;
    }

    public class Kendaraan
    {
        public string plat;
        public string merk;
        public string tipe;
        public int tahun;
        public int odometer;
        public int riwayatKe;

        public RiwayatService[] riwayat;

        public Kendaraan()
        {
            plat = "m";
            merk = "m";
            tipe = "m";
            riwayatKe = 0;
            riwayat = new RiwayatService[100];

            for (int i = 0; i < 100; i++)
            {
                riwayat[i] = new RiwayatService();
            }
        }
    }

    public class RiwayatService
    {
        public DateTime tanggal;
        public string lokasi;
        public string jenis;
        public int biaya;
        public int odometer;
    }

    public static int inputIndex = 0;
    public static string inputEmail = "m";
    public static string inputPassword = "m";
    public static int inputMenu = 0;

    static void Main(string[] args)
    {
        UserIndexPage();
    }

    static void UserIndexPage()
    {
        Console.WriteLine("=== Selamat datang di aplikasi Ride n Care! ===");
        Console.WriteLine("Pilih opsi di bawah ini untuk masuk");
        Console.WriteLine("1. Buat akun");
        Console.WriteLine("2. Masuk");
        Console.WriteLine("3. Keluar");
        Console.Write("Ketik disini: ");

        try
        {
            inputIndex = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        switch (inputIndex)
        {
            case 1:
                BuatAkun();
                return;

            case 2:
                Masuk();
                return;

            case 3:
                Environment.Exit(0);
                return;
        }

        Console.WriteLine("Input tidak valid. ketik apapun untuk kembali.");
        Console.ReadKey();
        Console.WriteLine();
        UserIndexPage();
        return;
    }

    static void BuatAkun()
    {
        Console.WriteLine("Silahkan Buat Akun");
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        Console.Write("Apakah sedang membuat akun bengkel? (y/n) ");
        string bengkelInput = (Console.ReadLine() ?? "").ToLower();
        bool bengkel = false;
        if (bengkelInput == "y")
        {
            bengkel = true;
        }

        pengguna[jumlahUser] = new User(username, email, password, bengkel);

        Console.WriteLine("Selamat akun anda berhasil di buat!");
        jumlahUser++;
        UserIndexPage();
        return;
    }

    static void Masuk()
    {
        bool berhasilLogin = false;
        bool loginBengkel = false;
        Console.WriteLine("Selamat datang kembali!");
        Console.Write("Email: ");
        inputEmail = Console.ReadLine();

        Console.Write("Password: ");
        inputPassword = Console.ReadLine();

        int i = 0;

        if (jumlahUser > 0)
        {
            do
            {
                if (inputEmail == pengguna[i].Email && inputPassword == pengguna[i].Password)
                {
                    berhasilLogin = true;
                    userAktif = i;
                    loginBengkel = pengguna[i].Bengkel;
                    break;
                }
                i++;
            }
            while (i < jumlahUser);
        }

        if (berhasilLogin)
        {
            Console.WriteLine("Login berhasil!");

            if (loginBengkel)
            {
                BengkelMenu();
            }
            else
            {
                UserMenu();
            }
            return;
        }
        else
        {
            Console.WriteLine("Login gagal! Email atau password salah/belum terdaftar.");
            Console.WriteLine("Ketik apapun untuk kembali ke Index!");
            Console.ReadKey();
            Console.WriteLine();
            UserIndexPage();
            return;
        }
    }

    static void UserMenu()
    {
        Console.WriteLine("1. Tambah Kendaraan ");
        Console.WriteLine("2. Lihat Kendaraan saya ");
        Console.WriteLine("3. Cari Bengkel & Booking ");
        Console.WriteLine("4. Lihat Booking Saya ");
        Console.WriteLine("5. Tambah Riwayat Servis ");
        Console.WriteLine("6. Lihat Riwayat Service ");
        Console.WriteLine("7. Logout ");
        Console.Write("Ketik disini: ");

        try
        {
            inputMenu = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        switch (inputMenu)
        {
            case 1:
                TambahKendaraan();
                break;

            case 2:
                LihatKendaraan();
                break;

            case 3:
                CariBookBengkel();
                break;

            case 4:
                LihatBooking();
                break;

            case 5:
                TambahRiwayat();
                break;

            case 6:
                LihatRiwayat();
                break;

            case 7:
                Console.WriteLine("Selamat tinggal!");
                UserIndexPage();
                break;

            default:
                Console.WriteLine("Input tidak valid.");
                UserMenu();
                break;
        }
    }

    static void TambahKendaraan()
    {
        if (pengguna[userAktif].jumlahKendaraan >= pengguna[userAktif].myKendaraan.Length)
        {
            Console.WriteLine("Kapasitas kendaraan penuh, silakan kembali ke menu");
            UserMenu();
            return;
        }

        int idx = pengguna[userAktif].jumlahKendaraan;
        pengguna[userAktif].myKendaraan[idx] = new Kendaraan();

        Console.Write("Plat: ");
        pengguna[userAktif].myKendaraan[idx].plat = Console.ReadLine();

        Console.Write("Merk: ");
        pengguna[userAktif].myKendaraan[idx].merk = Console.ReadLine();

        Console.Write("Tipe: ");
        pengguna[userAktif].myKendaraan[idx].tipe = Console.ReadLine();

        Console.Write("Tahun pembuatan: ");
        try
        {
            pengguna[userAktif].myKendaraan[idx].tahun = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        Console.Write("Odometer saat ini: ");
        try
        {
            pengguna[userAktif].myKendaraan[idx].odometer = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        Console.WriteLine("Kendaraan berhasil ditambahkan!");
        Console.WriteLine("[" + pengguna[userAktif].myKendaraan[idx].plat + "] " + pengguna[userAktif].myKendaraan[idx].merk + " " + pengguna[userAktif].myKendaraan[idx].tipe + " " + pengguna[userAktif].myKendaraan[idx].tahun + " || Odometer: " + pengguna[userAktif].myKendaraan[idx].odometer + " ");

        pengguna[userAktif].jumlahKendaraan++;

        Console.WriteLine("Mengembalikan ke Menu...");
        UserMenu();
        return;
    }

    static void LihatKendaraan()
    {
        if (pengguna[userAktif].jumlahKendaraan == 0)
        {
            Console.WriteLine("Belum ada kendaraan terdaftar.");
            Console.WriteLine("Mengembalikan ke Menu...");
            UserMenu();
            return;
        }

        Console.WriteLine("=== Daftar Kendaraan ===");

        int i = 0;
        do
        {
            Console.WriteLine((i + 1) + ". [" + pengguna[userAktif].myKendaraan[i].plat + "] " +
                pengguna[userAktif].myKendaraan[i].merk + " " +
                pengguna[userAktif].myKendaraan[i].tipe + " " +
                pengguna[userAktif].myKendaraan[i].tahun +
                " || Odometer: " + pengguna[userAktif].myKendaraan[i].odometer + " ");
            Console.WriteLine();
            i++;
        }
        while (i < pengguna[userAktif].jumlahKendaraan);

        Console.WriteLine("Ketik apapun untuk kembali ke Menu!");
        Console.ReadKey();
        Console.WriteLine();
        UserMenu();
        return;
    }

    static void CariBookBengkel()
    {
        jumlahBengkelBerSlot = 0;
        bool adaBengkel = false;

        Console.WriteLine("Daftar bengkel terdekat yang tersedia: ");

        int i = 0;
        if (jumlahUser > 0)
        {
            do
            {
                if (pengguna[i].Bengkel)
                {
                    Console.WriteLine("Nama Bengkel  : " + pengguna[i].infoBengkel.namaBengkel);
                    Console.WriteLine("Alamat  : " + pengguna[i].infoBengkel.alamatBengkel);
                    Console.WriteLine("Kontak  : " + pengguna[i].infoBengkel.kontakBengkel);
                    Console.WriteLine();

                    bengkelBerSlot[jumlahBengkelBerSlot] = i;
                    jumlahBengkelBerSlot++;
                    adaBengkel = true;
                }
                i++;
            }
            while (i < jumlahUser);
        }

        if (!adaBengkel)
        {
            Console.WriteLine("Tidak ada bengkel yang buka");
            Console.WriteLine("Mengembalikan ke Menu...");
            UserMenu();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("=== Pilih Bengkel ===");

        for (int x = 0; x < jumlahBengkelBerSlot; x++)
        {
            indexBengkel = bengkelBerSlot[x];
            Console.WriteLine((x + 1) + ". " + pengguna[indexBengkel].infoBengkel.namaBengkel);
        }

        Console.Write("Pilih bengkel untuk dibooking: ");
        try
        {
            pilihBengkel = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
            UserMenu();
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
            UserMenu();
            return;
        }

        if (pilihBengkel < 1 || pilihBengkel > jumlahBengkelBerSlot)
        {
            Console.WriteLine("Pilihan bengkel tidak valid.");
            UserMenu();
            return;
        }

        indexBengkel = bengkelBerSlot[pilihBengkel - 1];

        Console.WriteLine();
        Console.WriteLine("Slot tersedia di bengkel " + pengguna[indexBengkel].infoBengkel.namaBengkel);

        for (int j = 0; j < pengguna[indexBengkel].infoBengkel.bookingDibuat; j++)
        {
            Console.Write((j + 1) + ". ");

            if (pengguna[indexBengkel].infoBengkel.jadwalBooking[j].sudahDiambil == true)
            {
                Console.Write("[TIDAK TERSEDIA] ");
            }

            Console.WriteLine(
                "Tanggal : " + pengguna[indexBengkel].infoBengkel.jadwalBooking[j].jamHariBooking +
                ", Durasi: " + pengguna[indexBengkel].infoBengkel.jadwalBooking[j].durasiService
            );
        }

        Console.Write("Pilih slot untuk dibooking: ");
        try
        {
            pilihSlot = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
            UserMenu();
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
            UserMenu();
            return;
        }

        if (pilihSlot < 1 || pilihSlot > pengguna[indexBengkel].infoBengkel.bookingDibuat)
        {
            Console.WriteLine("Pilihan slot tidak valid.");
            UserMenu();
            return;
        }

        if (pengguna[indexBengkel].infoBengkel.jadwalBooking[pilihSlot - 1].sudahDiambil)
        {
            Console.WriteLine("Slot sudah dibooking oleh pengguna lain.");
            UserMenu();
            return;
        }

        pengguna[indexBengkel].infoBengkel.jadwalBooking[pilihSlot - 1].sudahDiambil = true;

        Console.WriteLine("Berhasil melakukan booking slot bengkel!");
        Console.WriteLine(
            "Tanggal : " +
            pengguna[indexBengkel].infoBengkel.jadwalBooking[pilihSlot - 1].jamHariBooking +
            ", Durasi: " +
            pengguna[indexBengkel].infoBengkel.jadwalBooking[pilihSlot - 1].durasiService
        );

        Console.WriteLine("Mengembalikan ke Menu...");
        UserMenu();
        return;
    }

    static void LihatBooking()
    {
        bool adaBooking = false;

        Console.WriteLine("=== Booking Saya ===");

        for (int i = 0; i < jumlahUser; i++)
        {
            if (pengguna[i].Bengkel)
            {
                for (int j = 0; j < pengguna[i].infoBengkel.bookingDibuat; j++)
                {
                    if (pengguna[i].infoBengkel.jadwalBooking[j].sudahDiambil)
                    {
                        Console.WriteLine("Bengkel : " + pengguna[i].infoBengkel.namaBengkel);
                        Console.WriteLine("Tanggal : " + pengguna[i].infoBengkel.jadwalBooking[j].jamHariBooking);
                        Console.WriteLine("Durasi  : " + pengguna[i].infoBengkel.jadwalBooking[j].durasiService);
                        Console.WriteLine();
                        adaBooking = true;
                    }
                }
            }
        }

        if (!adaBooking)
        {
            Console.WriteLine("Belum ada booking.");
        }

        Console.WriteLine("Ketik apapun untuk kembali ke Menu!");
        Console.ReadKey();
        Console.WriteLine();
        UserMenu();
        return;
    }

    static void TambahRiwayat()
    {
        if (pengguna[userAktif].jumlahKendaraan == 0)
        {
            Console.WriteLine("Belum ada kendaraan terdaftar.");
            Console.WriteLine("Mengembalikan ke Menu...");
            UserMenu();
            return;
        }

        Console.WriteLine("Kendaraan anda:");
        for (int i = 0; i < pengguna[userAktif].jumlahKendaraan; i++)
        {
            Console.WriteLine();
            Console.WriteLine((i + 1) + ". [" + pengguna[userAktif].myKendaraan[i].plat + "] " + pengguna[userAktif].myKendaraan[i].merk + " " + pengguna[userAktif].myKendaraan[i].tipe + " " + pengguna[userAktif].myKendaraan[i].tahun + " || Odometer: " + pengguna[userAktif].myKendaraan[i].odometer);
        }
        Console.Write("Pilih nomor kendaraan yang mau ditambahkan riwayatnya: ");
        try
        {
            pilihKendaraan = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        if (pilihKendaraan < 1 || pilihKendaraan > pengguna[userAktif].jumlahKendaraan)
        {
            Console.WriteLine("Nomor kendaraan tidak valid.");
            TambahRiwayat();
            return;
        }

        int k = pilihKendaraan - 1;

        if (pengguna[userAktif].myKendaraan[k].riwayatKe >= pengguna[userAktif].myKendaraan[k].riwayat.Length)
        {
            Console.WriteLine("Riwayat penuh!");
            UserMenu();
            return;
        }

        int r = pengguna[userAktif].myKendaraan[k].riwayatKe;
        pengguna[userAktif].myKendaraan[k].riwayat[r] = new RiwayatService();

        Console.Write("Tanggal (DD/MM/YYYY): ");
        try
        {
            pengguna[userAktif].myKendaraan[k].riwayat[r].tanggal = DateTime.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }

        Console.Write("Lokasi: ");
        pengguna[userAktif].myKendaraan[k].riwayat[r].lokasi = Console.ReadLine();

        Console.Write("Jenis servis: ");
        pengguna[userAktif].myKendaraan[k].riwayat[r].jenis = Console.ReadLine();

        Console.Write("Biaya: ");
        try
        {
            pengguna[userAktif].myKendaraan[k].riwayat[r].biaya = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        Console.Write("Odometer Terakhir: ");
        try
        {
            pengguna[userAktif].myKendaraan[k].riwayat[r].odometer = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        Console.WriteLine("Riwayat berhasil ditambahkan!");
        Console.WriteLine("[" + pengguna[userAktif].myKendaraan[k].riwayat[r].tanggal + "] Lokasi: " + pengguna[userAktif].myKendaraan[k].riwayat[r].lokasi + " || Jenis servis: " + pengguna[userAktif].myKendaraan[k].riwayat[r].jenis + " || Biaya: Rp." + pengguna[userAktif].myKendaraan[k].riwayat[r].biaya + " || Odometer saat servis: " + pengguna[userAktif].myKendaraan[k].riwayat[r].odometer);

        pengguna[userAktif].myKendaraan[k].riwayatKe++;

        Console.WriteLine("Mengembalikan ke menu...");
        UserMenu();
        return;
    }

    static void LihatRiwayat()
    {
        if (pengguna[userAktif].jumlahKendaraan == 0)
        {
            Console.WriteLine("Belum ada kendaraan terdaftar.");
            Console.WriteLine("Mengembalikan ke Menu...");
            UserMenu();
            return;
        }

        Console.WriteLine("Kendaraan anda:");
        for (int i = 0; i < pengguna[userAktif].jumlahKendaraan; i++)
        {
            Console.WriteLine();
            Console.WriteLine((i + 1) + ". [" + pengguna[userAktif].myKendaraan[i].plat + "] " + pengguna[userAktif].myKendaraan[i].merk + " " + pengguna[userAktif].myKendaraan[i].tipe + " " + pengguna[userAktif].myKendaraan[i].tahun + " || Odometer: " + pengguna[userAktif].myKendaraan[i].odometer);
        }
        Console.Write("Pilih nomor kendaraan untuk dicek riwayatnya: ");
        try
        {
            pilihKendaraan = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        if (pilihKendaraan < 1 || pilihKendaraan > pengguna[userAktif].jumlahKendaraan)
        {
            Console.WriteLine("Nomor kendaraan tidak valid.");
            LihatRiwayat();
            return;
        }

        int k = pilihKendaraan - 1;

        Console.WriteLine("List Riwayat: ");

        if (pengguna[userAktif].myKendaraan[k].riwayatKe == 0)
        {
            Console.WriteLine("Belum ada riwayat servis.");
        }
        else
        {
            int i = 0;
            do
            {
                Console.WriteLine(
                    "[" + pengguna[userAktif].myKendaraan[k].riwayat[i].tanggal + "] " +
                    "Lokasi: " + pengguna[userAktif].myKendaraan[k].riwayat[i].lokasi +
                    " || Jenis servis: " + pengguna[userAktif].myKendaraan[k].riwayat[i].jenis +
                    " || Biaya: Rp." + pengguna[userAktif].myKendaraan[k].riwayat[i].biaya
                );
                i++;
            }
            while (i < pengguna[userAktif].myKendaraan[k].riwayatKe);
        }

        Console.WriteLine("Ketik apapun untuk kembali ke Menu!");
        Console.ReadKey();
        Console.WriteLine();
        UserMenu();
        return;
    }

    static void BengkelMenu()
    {
        Console.WriteLine("1. Setting Profil Bengkel ");
        Console.WriteLine("2. Atur Slot Booking");
        Console.WriteLine("3. Lihat Booking Masuk");
        Console.WriteLine("4. Logout");

        try
        {
            inputMenu = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        switch (inputMenu)
        {
            case 1:
                ProfilBengkel();
                break;

            case 2:
                SetBooking();
                break;

            case 3:
                LihatBookingan();
                break;

            case 4:
                Console.WriteLine("Selamat tinggal!");
                UserIndexPage();
                break;

            default:
                Console.WriteLine("Input tidak valid.");
                BengkelMenu();
                break;
        }
    }

    static void ProfilBengkel()
    {
        Console.WriteLine("Pengaturan Profil Bengkel");
        Console.WriteLine("1. Ubah nama bengkel ");
        Console.WriteLine("2. Ubah alamat bengkel");
        Console.WriteLine("3. Ubah informasi kontak bengkel");
        Console.WriteLine("4. Kembali ke Menu");

        try
        {
            inputMenu = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        switch (inputMenu)
        {
            case 1:
                Console.Write("Masukkan nama bengkel anda: ");
                pengguna[userAktif].infoBengkel.namaBengkel = Console.ReadLine();
                Console.WriteLine("Nama bengkel berhasil dirubah!");
                Console.WriteLine("Nama baru : " + pengguna[userAktif].infoBengkel.namaBengkel);
                Console.WriteLine("Mengembalikan ke menu...");
                BengkelMenu();
                return;

            case 2:
                Console.Write("Masukkan alamat bengkel anda: ");
                pengguna[userAktif].infoBengkel.alamatBengkel = Console.ReadLine();
                Console.WriteLine("Alamat bengkel berhasil dirubah!");
                Console.WriteLine("Alamat baru : " + pengguna[userAktif].infoBengkel.alamatBengkel);
                Console.WriteLine("Mengembalikan ke menu...");
                BengkelMenu();
                return;

            case 3:
                Console.Write("Masukkan informasi kontak bengkel anda: ");
                pengguna[userAktif].infoBengkel.kontakBengkel = Console.ReadLine();
                Console.WriteLine("Informasi kontak bengkel berhasil dirubah!");
                Console.WriteLine("Informasi kontak baru : " + pengguna[userAktif].infoBengkel.kontakBengkel);
                Console.WriteLine("Mengembalikan ke menu...");
                BengkelMenu();
                return;

            case 4:
                Console.WriteLine("Mengembalikan ke Menu...");
                BengkelMenu();
                return;

            default:
                Console.WriteLine("Input tidak valid.");
                ProfilBengkel();
                return;
        }
    }

    static void SetBooking()
    {
        Console.WriteLine("Masukkan tanggal dan jam slot yang tersedia");
        Console.Write("DD/MM/YYYY hh:mm :");
        try
        {
            pengguna[userAktif].infoBengkel.jadwalBooking[pengguna[userAktif].infoBengkel.bookingDibuat].jamHariBooking = DateTime.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }

        Console.WriteLine();
        Console.Write("Masukkan durasi (menit) slot yang tersedia pada jam tersebut: ");
        try
        {
            pengguna[userAktif].infoBengkel.jadwalBooking[pengguna[userAktif].infoBengkel.bookingDibuat].durasiService = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Input invalid.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Angka kebesaran");
        }

        Console.WriteLine("Slot jadwal berhasil ditambahkan!");

        pengguna[userAktif].infoBengkel.bookingDibuat++;
        Console.WriteLine("Mengembalikan ke menu...");
        BengkelMenu();
        return;
    }

    static void LihatBookingan()
    {
        if (pengguna[userAktif].infoBengkel.bookingDibuat == 0)
        {
            Console.WriteLine("Belum ada jadwal yang terbooking oleh pengguna.");
            Console.WriteLine("Mengembalikan ke menu...");
            BengkelMenu();
            return;
        }
        else
        {
            pengguna[userAktif].infoBengkel.adaYangSudahDiambil = false;
            for (int i = 0; i < pengguna[userAktif].infoBengkel.bookingDibuat; i++)
            {
                if (pengguna[userAktif].infoBengkel.jadwalBooking[i].sudahDiambil)
                {
                    pengguna[userAktif].infoBengkel.adaYangSudahDiambil = true;
                }
            }
            if (pengguna[userAktif].infoBengkel.adaYangSudahDiambil)
            {
                Console.WriteLine("Daftar slot jadwal yang sudah dibooking:");

                for (int i = 0; i < pengguna[userAktif].infoBengkel.bookingDibuat; i++)
                {
                    if (pengguna[userAktif].infoBengkel.jadwalBooking[i].sudahDiambil)
                    {
                        Console.WriteLine("Tanggal: " + pengguna[userAktif].infoBengkel.jadwalBooking[i].jamHariBooking);
                        Console.WriteLine("Durasi: " + pengguna[userAktif].infoBengkel.jadwalBooking[i].durasiService);
                    }
                }

                Console.WriteLine("Ketik apapun untuk kembali ke Menu!");
                Console.ReadKey();
                Console.WriteLine();
                BengkelMenu();
                return;
            }
            else
            {
                Console.WriteLine("Belum ada jadwal yang terbooking oleh pengguna.");
                Console.WriteLine("Mengembalikan ke menu...");
                BengkelMenu();
                return;
            }
        }
    }
}
