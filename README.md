
# 🏗️ Vinç Kiralama Sistemi

Bu proje, iş makineleri ve vinç kiralama süreçlerini dijitalleştirmek, operasyonel karmaşıklığı azaltmak ve hem kiralayan hem de kiraya veren taraflar için güvenli bir platform oluşturmak amacıyla **MVC (Model-View-Controller)** mimarisiyle geliştirilmiştir.

---

## 📺 Proje Tanıtım Videosu

Projenin çalışma mantığını ve özelliklerini aşağıdaki videodan izleyebilirsiniz:

[![Proje Tanıtım Videosu](https://youtu.be/EG1JVAZNhL0)

---

## 🎯 Projenin Amacı

Geleneksel vinç kiralama süreçlerindeki telefon trafiği ve takip zorluğunu ortadan kaldırmayı hedefler.
* **Erişilebilirlik:** Müşterilerin ihtiyaç duydukları vinç özelliklerine (tonaj, bom uzunluğu vb.) 7/24 ulaşabilmesini sağlamak.
* **Verimlilik:** İş makinelerinin doluluk oranlarını dijital bir takvim üzerinden yöneterek boşta kalma süresini minimize etmek.
* **Şeffaflık:** Fiyatlandırma ve teknik özelliklerin kullanıcıya net bir şekilde sunulması.

---

## ⚙️ Çalışma Mantığı

Sistem, **N-Tier (Katmanlı Mimari)** prensiplerine uygun olarak üç ana temel üzerinde çalışır:

1.  **Talep Yönetimi (View):** Kullanıcı arayüzden bir vinç seçer ve tarih aralığı belirler. Bu istek Controller'a iletilir.
2.  **İş Mantığı (Controller & Business):** Seçilen tarihlerde vincin müsaitlik durumu veritabanından kontrol edilir. Eğer uygunsa kiralama işlemi onay sürecine alınır.
3.  **Veri Yönetimi (Model & Data):** Tüm araç bilgileri, kullanıcı kayıtları ve kiralama geçmişi MSSQL üzerinde ilişkisel bir yapıda tutulur. Entity Framework Core üzerinden güvenli bir şekilde yönetilir.



---


## 📸 Ekran Görüntüleri

### 1. Ana Sayfa ve Karşılama
<img width="1919" height="1138" alt="Ekran görüntüsü 2026-01-20 174509" src="https://github.com/user-attachments/assets/8a693292-02a9-458f-8432-7383da9b53d9" />
 

---

### 2. Admin Paneli
<img width="1919" height="472" alt="Ekran görüntüsü 2026-01-20 174653" src="https://github.com/user-attachments/assets/66c567d4-8aab-4e97-bafe-18aa5f827a3d" />



---

### 3. Kiralama Paneli
<img width="1919" height="1078" alt="Ekran görüntüsü 2026-01-20 174535" src="https://github.com/user-attachments/assets/0ab126fe-1671-482b-a774-7ee6386580c7" />

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:** .NET MVC
* **ORM:** Entity Framework Core
* **Database:** MSSQL
* **Frontend:** HTML5, CSS3, Bootstrap, JavaScript

---

## 🛠️ Kurulum ve Çalıştırma

Projeyi yerel ortamınızda test etmek için şu adımları izleyin:

1.  **Projeyi Klonlayın:** `git clone https://github.com/kullaniciadi/proje-adi.git`
2.  **Veritabanı Ayarı:** `appsettings.json` dosyasındaki `DefaultConnection` kısmına kendi SQL Server adresinizi yazın.
3.  **Migration:** Package Manager Console üzerinden `Update-Database` komutunu çalıştırarak tabloları oluşturun.
4.  **Çalıştır:** Visual Studio üzerinden `F5` ile projeyi ayağa kaldırın.

---

