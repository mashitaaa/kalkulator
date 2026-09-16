# Kalkulator Scientific - PBKK Latihan 1

**Nama**: Safa Mashita

**NRP**: 5025241022

**Tugas**: PBKK Pertemuan 3

## Deskripsi
Aplikasi kalkulator scientific berbasis Windows Forms (.NET) dengan fitur operasi dasar, fungsi scientific (sin, cos, tan, sqrt, log, x^y), riwayat perhitungan, dan tampilan custom (rounded button & panel).

## Struktur Project
- `CalculatorApp.csproj` — file konfigurasi project (target framework, dependency, dll)
- `Program.cs` — titik masuk (entry point) aplikasi
- `Form1.cs` — logika/behavior aplikasi (menghubungkan UI dengan CalculatorEngine)
- `Form1.Designer.cs` — definisi tampilan (layout, tombol, warna)
- `CalculatorEngine.cs` — seluruh logika perhitungan, terpisah dari UI
- `RoundedButton.cs` — komponen tombol custom dengan sudut membulat
- `RoundedPanel.cs` — komponen panel custom dengan sudut membulat
