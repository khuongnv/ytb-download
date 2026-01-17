# YouTube Audio Downloader

Một ứng dụng WinForms đơn giản và mạnh mẽ để tải âm thanh từ danh sách phát YouTube bằng công cụ `yt-dlp`.

## Phiên bản
- **Version**: 1.0.0
- **Author**: khuongnv@live.com
- **Github**: https://github.com/khuongnv/ytb-download.git
## Tính năng
- [x] Tải danh sách phát (Playlist) hoặc Video đơn lẻ từ YouTube.
- [x] Tự động chuyển đổi sang định dạng âm thanh chất lượng cao (.m4a).
- [x] Hỗ trợ xem thông tin playlist trước khi tải.
- [x] Cho phép chọn lọc các bài hát cần tải trong danh sách.
- [x] Kiểm tra file đã tồn tại để tránh tải trùng lặp.
- [x] Giao diện WinForms cổ điển, ổn định và dễ sử dụng.
- [x] Lưu trạng thái phiên làm việc tự động.

## Yêu cầu hệ thống
- **Hệ điều hành**: Windows 10/11
- **Môi trường**: .NET 6.0 Runtime
- **Công cụ đi kèm**: `yt-dlp.exe` (được đặt trong thư mục `yt-bin`)

## Hướng dẫn sử dụng
1. **Nhập URL**: Dán link playlist hoặc video YouTube vào ô "Playlist URL".
2. **Lấy thông tin**: Nhấn nút "Load Info" để hiển thị danh sách các bài hát.
3. **Chọn thư mục**: Nhấn "Browse..." để chọn nơi lưu file nhạc.
4. **Tải xuống**: Chọn các bài hát mong muốn trong bảng và nhấn "START / RESUME DOWNLOAD".
5. **Tạm dừng**: Nhấn "PAUSE" nếu muốn dừng quá trình tải hiện tại.

## Cấu trúc thư mục
- `ytb-app/`: Mã nguồn ứng dụng C# WinForms.
- `yt-bin/`: Chứa tệp thực thi `yt-dlp.exe`.
- `appstate.json`: Lưu trữ cấu hình và danh sách bài hát gần nhất.

## Ghi chú
Ứng dụng sử dụng công cụ mã nguồn mở `yt-dlp`. Vui lòng đảm bảo rằng bạn có quyền truy cập hợp pháp vào các nội dung âm thanh mà bạn tải xuống.
