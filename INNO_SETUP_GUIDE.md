# 📦 Hướng dẫn đóng gói App WinForms bằng Inno Setup (.NET self-contained)

Tài liệu này hướng dẫn cách **đóng gói ứng dụng WinForms (.NET 6/7/8)**  
thành **bộ cài đặt Setup.exe** bằng **Inno Setup**, áp dụng cho app WinForms sử dụng `yt-dlp.exe`.

---

## 🎯 Mục tiêu

- App cài đặt được trên mọi máy Windows 64-bit  
- Không cần cài .NET runtime  
- Installer gọn, ổn định, chuyên nghiệp  
- Dễ build lại và phát hành version mới  

---

## 🧩 Tổng quan quy trình

Build App (.NET self-contained)  
→ Thu thập file publish  
→ Viết script Inno Setup (.iss)  
→ Compile → Setup.exe  

---

## 1️⃣ Chuẩn bị môi trường

### Yêu cầu
- Windows 10 / 11  
- Visual Studio hoặc dotnet CLI  
- WinForms .NET 6 / 7 / 8  
- Inno Setup (FREE)

### Cài Inno Setup
https://jrsoftware.org/isinfo.php

---

## 2️⃣ Build app .NET self-contained

Chạy lệnh sau trong thư mục project:

```
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false
```

> Không dùng `PublishSingleFile=true` để tránh lỗi khi gọi `yt-dlp.exe`.

---

## 3️⃣ Thư mục publish

Sau khi build xong:

```
bin\Release\net6.0-windows\win-x64\publish\
 ├── YourApp.exe
 ├── yt-dlp.exe
 ├── *.dll
 └── *.json
```

Copy toàn bộ vào thư mục `dist`:

```
dist\
 ├── YourApp.exe
 ├── yt-dlp.exe
 └── (các file khác)
```

---

## 4️⃣ Tạo script Inno Setup

Tạo file `installer.iss`

```
[Setup]
AppName=Your App Name
AppVersion=1.0.0
DefaultDirName={pf}\YourApp
DefaultGroupName=Your App Name
OutputDir=output
OutputBaseFilename=YourApp_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
DisableProgramGroupPage=yes

[Files]
Source: "dist\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Your App Name"; Filename: "{app}\YourApp.exe"
Name: "{commondesktop}\Your App Name"; Filename: "{app}\YourApp.exe"

[Run]
Filename: "{app}\YourApp.exe"; Description: "Launch Your App"; Flags: nowait postinstall skipifsilent
```

---

## 5️⃣ Compile installer

- Mở Inno Setup Compiler  
- Open `installer.iss`  
- Bấm **Compile**

Kết quả:

```
output\YourApp_Setup.exe
```

---

## 6️⃣ Kiểm tra sau cài đặt

- App được cài vào `Program Files`  
- Có shortcut Desktop & Start Menu  
- App chạy không cần cài .NET  

---

## 7️⃣ Lưu ý quan trọng

- `yt-dlp.exe` phải nằm cùng thư mục với app exe  
- Trong code chỉ gọi:

```
Process.Start("yt-dlp.exe", args);
```

- App chưa ký số có thể bị SmartScreen cảnh báo (bình thường)

---

## 🏁 Tổng kết

- Inno Setup: FREE, ổn định  
- .NET self-contained: chạy mọi máy  
- Phù hợp app WinForms nội bộ & phát hành nhỏ lẻ  

---

📌 Khuyến nghị dùng Inno Setup cho toàn bộ app WinForms dùng tool ngoài.
