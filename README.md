Initialized empty Git repository in C:/Users/Admin/Source/Repos/webapi/.git/
# Tài liệu Web API

## Tổng quan
Web API này cung cấp các chức năng xác thực người dùng, quản lý danh mục và quản lý bài viết. Nó tuân theo mô hình kiến trúc MVC và sử dụng JWT để xác thực và kiểm soát truy cập dựa trên vai trò.

## API Xác thực
- **POST /api/auth/login**: Xác thực người dùng và trả về JWT token.
  - **Nội dung yêu cầu**: `{ "username": "string", "password": "string" }`
  - **Phản hồi**: JWT token nếu xác thực thành công.

- **POST /api/auth/logout**: Đăng xuất người dùng (hiện tại là placeholder).

## API Danh mục
- **GET /api/category**: Lấy tất cả danh mục.
  - **Phản hồi**: Danh sách các danh mục.

- **GET /api/category/{id}**: Lấy danh mục theo ID.
  - **Phản hồi**: Chi tiết danh mục.

- **POST /api/category**: Tạo danh mục mới.
  - **Nội dung yêu cầu**: `{ "name": "string", "parentCategoryId": "int" }`

- **PUT /api/category/{id}**: Cập nhật danh mục hiện có.
  - **Nội dung yêu cầu**: `{ "name": "string", "parentCategoryId": "int" }`

- **DELETE /api/category/{id}**: Xóa danh mục theo ID.

## API Bài viết
- **GET /api/article**: Lấy tất cả bài viết.
  - **Phản hồi**: Danh sách các bài viết.

- **GET /api/article/{id}**: Lấy bài viết theo ID.
  - **Phản hồi**: Chi tiết bài viết.

- **POST /api/article**: Tạo bài viết mới.
  - **Nội dung yêu cầu**: `{ "title": "string", "content": "string", "categoryId": "int" }`
  - **Vai trò được phép**: Admin, Editor

- **PUT /api/article/{id}**: Cập nhật bài viết hiện có.
  - **Nội dung yêu cầu**: `{ "title": "string", "content": "string", "categoryId": "int" }`
  - **Vai trò được phép**: Admin, Editor

- **DELETE /api/article/{id}**: Xóa bài viết theo ID.
  - **Vai trò được phép**: Admin

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
![image](https://github.com/user-attachments/assets/f1b232e6-dfc2-4bed-a6cf-ca3f52afa8c0)

*** GET/7078/api/Article/export xuất các thông tin người dùng về file execl
![image](https://github.com/user-attachments/assets/28320e8f-4619-4a30-a52c-a17f891552ee)

*** api/Article/export-word/1 xuất thông tin user ra file word 
![image](https://github.com/user-attachments/assets/10ec6891-11bb-4c4d-98fb-19c8be4fe147)

