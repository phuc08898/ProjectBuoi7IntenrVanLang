
# Tài liệu Web API

## Tổng quan
Web API này cung cấp các chức năng xác thực người dùng, quản lý danh mục và quản lý bài viết. Nó tuân theo mô hình kiến trúc MVC và sử dụng JWT để xác thực và kiểm soát truy cập dựa trên vai trò.

## API Xác thực
- **POST /api/auth/login**: Xác thực người dùng và trả về JWT token.
  - **Nội dung yêu cầu**: `{ "username": "string", "password": "string" }`
  - **Phản hồi**: JWT token nếu xác thực thành công.
  - Data user: admin password admin1213 rold Admin
![image](https://github.com/user-attachments/assets/f1d67122-d9e9-408e-985a-0843c6f1a7a2)

- **POST /api/auth/logout**: Đăng xuất người dùng (hiện tại là placeholder).
- ![image](https://github.com/user-attachments/assets/c3745bcf-ba5a-4c6e-80ac-6e6aeec58c17)


## API Danh mục
- **GET /api/category**: Lấy tất cả danh mục.
  - **Phản hồi**: Danh sách các danh mục.
  - ![image](https://github.com/user-attachments/assets/12564c72-0641-4fe9-940f-91a1477b87c4)


- **GET /api/category/{id}**: Lấy danh mục theo ID.
  - **Phản hồi**: Chi tiết danh mục.vì chưa có dữ liệu nên lấy Id sẽ trả về lỗi
  - ![image](https://github.com/user-attachments/assets/fbf77565-9bef-4203-98e4-9a8abb1d4074)


- **POST /api/category**: Tạo danh mục mới.
  - **Nội dung yêu cầu**: `{ "name": "string", "parentCategoryId": "int" }`
![image](https://github.com/user-attachments/assets/36c5d8a8-b729-4e5d-b2c9-0e8a47362476)

- **PUT /api/category/{id}**: Cập nhật danh mục hiện có.
  - **Nội dung yêu cầu**: `{ "name": "string", "parentCategoryId": "int" }` vì chưa có dữ liệu nên lấy sẽ báo lỗi không gửi phản hồi được
![image](https://github.com/user-attachments/assets/84d915fd-0955-4cd2-885b-e2e8b43e173d)

- **DELETE /api/category/{id}**: Xóa danh mục theo ID.
![image](https://github.com/user-attachments/assets/ecc0890c-478a-410b-a49b-6158e69f2510)

## API Bài viết
- **GET /api/article**: Lấy tất cả bài viết.
  - **Phản hồi**: Danh sách các bài viết. lấy ra dữ liệu bài viết. Vì chưa có dữ liệu nên lấy ra là một array rỗng
![image](https://github.com/user-attachments/assets/769bdf60-3405-4adc-8be1-4aedb94d7e93)

- **GET /api/article/{id}**: Lấy bài viết theo ID.
  - **Phản hồi**: Chi tiết bài viết.
  - ![image](https://github.com/user-attachments/assets/dc772b0f-d2f4-4d29-864b-9677ca3e6caa)


- **POST /api/article**: Tạo bài viết mới.
  - **Nội dung yêu cầu**: `{ "title": "string", "content": "string", "categoryId": "int" }`
  - **Vai trò được phép**: Admin, Editor
![image](https://github.com/user-attachments/assets/3a03ce13-5b99-4442-8252-3ddcfa9849cf)

- **PUT /api/article/{id}**: Cập nhật bài viết hiện có.
  - **Nội dung yêu cầu**: `{ "title": "string", "content": "string", "categoryId": "int" }`
  - **Vai trò được phép**: Admin, Editor
![image](https://github.com/user-attachments/assets/a17e8d82-b85e-430d-9271-5fdde51dfec2)

- **DELETE /api/article/{id}**: Xóa bài viết theo ID.
  - **Vai trò được phép**: Admin
![image](https://github.com/user-attachments/assets/80e2ddf9-0579-4746-8357-18898fb31638)

## Kiểm soát truy cập dựa trên vai trò
- **Admin**: Có thể thực hiện tất cả các thao tác CRUD trên danh mục và bài viết.
- **Editor**: Có thể tạo và cập nhật bài viết.
- **User**: Có thể xem bài viết và danh mục.

## Người thực hiện
- **Xác thực**: [Tên của bạn]
- **Quản lý danh mục**: [Tên của bạn]
- **Quản lý bài viết**: [Tên của bạn]
- **Tài liệu**: [Tên của bạn]

** POST/api/Article/import nhập thông tin từ file execl 
![image](https://github.com/user-attachments/assets/011ccdc5-bd10-485d-a28d-75d5d379a6bf)


*** GET/7078/api/Article/export xuất các thông tin bài viết về file execl
![image](https://github.com/user-attachments/assets/7641db12-80af-46f9-bae7-db1e54e2a77e)


*** api/Article/export-word/1 xuất thông tin bài viết ra file word 
![image](https://github.com/user-attachments/assets/a3136f4c-8395-4bea-874a-dd78df12afb6)



