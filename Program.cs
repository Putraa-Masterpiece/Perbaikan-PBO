

using System;
using System.Collections.Generic;

interface IPerpustakaanMini
{
    void ProsesPinjam(string namaPeminjam);
    void ProsesKembali(string namaPeminjam);
    bool SedangDipinjamOleh(string namaPeminjam);
}

abstract class KoleksiBacaan : IPerpustakaanMini
{
    public string NamaJudul { get; set; }
    public string NamaPenulis { get; set; }
    public int TahunRilis { get; set; }

    private List<string> daftarPeminjam = new List<string>();

    public KoleksiBacaan(string namaJudul, string namaPenulis, int tahunRilis)
    {
        NamaJudul = namaJudul;
        NamaPenulis = namaPenulis;
        TahunRilis = tahunRilis;
    }

    public abstract void InfoRingkas();

    public virtual void ProsesPinjam(string namaPeminjam)
    {
        if (daftarPeminjam.Count < 3)
        {
            daftarPeminjam.Add(namaPeminjam);
            Console.WriteLine($"{NamaJudul} berhasil dipinjam oleh {namaPeminjam}");
        }
        else
        {
            Console.WriteLine("Jumlah peminjam maksimum telah tercapai.");
        }
    }

    public void ProsesKembali(string namaPeminjam)
    {
        if (daftarPeminjam.Contains(namaPeminjam))
        {
            daftarPeminjam.Remove(namaPeminjam);
            Console.WriteLine($"{NamaJudul} dikembalikan oleh {namaPeminjam}");
        }
    }

    public bool SedangDipinjamOleh(string namaPeminjam)
    {
        return daftarPeminjam.Contains(namaPeminjam);
    }
}

class CeritaFiksi : KoleksiBacaan
{
    public CeritaFiksi(string namaJudul, string namaPenulis, int tahunRilis)
        : base(namaJudul, namaPenulis, tahunRilis) { }

    public override void InfoRingkas()
    {
        Console.WriteLine($"[Fiksi] {NamaJudul}, {NamaPenulis}, {TahunRilis}");
    }
}

class BacaanIlmiah : KoleksiBacaan
{
    public BacaanIlmiah(string namaJudul, string namaPenulis, int tahunRilis)
        : base(namaJudul, namaPenulis, tahunRilis) { }

    public override void InfoRingkas()
    {
        Console.WriteLine($"[Non-Fiksi] {NamaJudul}, {NamaPenulis}, {TahunRilis}");
    }
}

class TerbitanMajalah : KoleksiBacaan
{
    public TerbitanMajalah(string namaJudul, string namaPenulis, int tahunRilis)
        : base(namaJudul, namaPenulis, tahunRilis) { }

    public override void InfoRingkas()
    {
        Console.WriteLine($"[Majalah] {NamaJudul}, {NamaPenulis}, {TahunRilis}");
    }
}

class JalankanAplikasi
{
    static List<KoleksiBacaan> rakPerpustakaan = new List<KoleksiBacaan>();
    static string identitasPengguna = "Pengguna01";

    static void Main()
    {
        bool aktif = true;
        while (aktif)
        {
            Console.WriteLine("\nMenu:\n1. Input Buku\n2. Edit Buku\n3. Daftar Buku\n4. Pinjam\n5. Kembali\n6. Buku Dipinjam\n7. Keluar");
            Console.Write("Pilih: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1": TambahItem(); break;
                case "2": EditItem(); break;
                case "3": TampilkanDaftar(); break;
                case "4": ProsesPinjam(); break;
                case "5": ProsesKembali(); break;
                case "6": TampilkanDipinjam(); break;
                case "7": aktif = false; break;
                default: Console.WriteLine("Opsi tidak tersedia."); break;
            }
        }
    }

    static void TambahItem()
    {
        Console.Write("Kategori (1. Fiksi, 2. Non-Fiksi, 3. Majalah): ");
        string kategori = Console.ReadLine();
        Console.Write("Judul: "); string inputJudul = Console.ReadLine();
        Console.Write("Penulis: "); string inputPenulis = Console.ReadLine();
        Console.Write("Tahun: "); int inputTahun = int.Parse(Console.ReadLine());

        switch (kategori)
        {
            case "1": rakPerpustakaan.Add(new CeritaFiksi(inputJudul, inputPenulis, inputTahun)); break;
            case "2": rakPerpustakaan.Add(new BacaanIlmiah(inputJudul, inputPenulis, inputTahun)); break;
            case "3": rakPerpustakaan.Add(new TerbitanMajalah(inputJudul, inputPenulis, inputTahun)); break;
            default: Console.WriteLine("Kategori tidak dikenali."); break;
        }
    }

    static void EditItem()
    {
        Console.Write("Index buku: ");
        int indeks = int.Parse(Console.ReadLine());
        if (indeks >= 0 && indeks < rakPerpustakaan.Count)
        {
            Console.Write("Judul baru: "); rakPerpustakaan[indeks].NamaJudul = Console.ReadLine();
            Console.Write("Penulis baru: "); rakPerpustakaan[indeks].NamaPenulis = Console.ReadLine();
            Console.Write("Tahun baru: "); rakPerpustakaan[indeks].TahunRilis = int.Parse(Console.ReadLine());
        }
        else
        {
            Console.WriteLine("Index tidak valid.");
        }
    }

    static void TampilkanDaftar()
    {
        for (int no = 0; no < rakPerpustakaan.Count; no++)
        {
            Console.Write($"[{no}] "); rakPerpustakaan[no].InfoRingkas();
        }
    }

    static void ProsesPinjam()
    {
        Console.Write("Index buku: ");
        int nomor = int.Parse(Console.ReadLine());
        if (nomor >= 0 && nomor < rakPerpustakaan.Count)
        {
            rakPerpustakaan[nomor].ProsesPinjam(identitasPengguna);
        }
        else
        {
            Console.WriteLine("Index tidak valid.");
        }
    }

    static void ProsesKembali()
    {
        Console.Write("Index buku: ");
        int nomor = int.Parse(Console.ReadLine());
        if (nomor >= 0 && nomor < rakPerpustakaan.Count)
        {
            rakPerpustakaan[nomor].ProsesKembali(identitasPengguna);
        }
        else
        {
            Console.WriteLine("Index tidak valid.");
        }
    }

    static void TampilkanDipinjam()
    {
        for (int i = 0; i < rakPerpustakaan.Count; i++)
        {
            if (rakPerpustakaan[i].SedangDipinjamOleh(identitasPengguna))
            {
                Console.Write($"[{i}] "); rakPerpustakaan[i].InfoRingkas();
            }
        }
    }
}
