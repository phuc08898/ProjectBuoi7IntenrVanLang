<script setup>
import { reactive } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

// Định nghĩa các state để quản lý input của form
const state = reactive({
  username: "",
  password: "",
  role: "", // Trường role thêm vào
  termsAccepted: false, // Điều khoản chấp nhận
});

const router = useRouter(); // Để chuyển hướng sau khi đăng ký thành công

// Hàm xử lý đăng ký
async function handleSignUp() {
  try {
    const response = await axios.post(
      "https://localhost:7078/api/Auth/register",
      {
        username: state.username,
        password: state.password,
        role: state.role,
      }
    );

    if (response.status === 200) {
      alert(response.data.message); // Thông báo khi thành công
      router.push({ name: "login" }); // Chuyển hướng tới trang đăng nhập sau khi đăng ký thành công
    } else {
      alert(response.data.message); // Thông báo nếu có lỗi
    }
  } catch (error) {
    console.error("Lỗi khi đăng ký:", error);
  }
}
</script>

<template>
  <div class="p-4 w-100 flex-grow-1 d-flex align-items-center">
    <div class="w-100">
      <!-- Header -->
      <div class="text-center mb-5">
        <p class="mb-3">
          <i class="fa fa-2x fa-circle-notch text-primary-light"></i>
        </p>
        <h1 class="fw-bold mb-2">Tạo tài khoản</h1>
        <p class="fw-medium text-muted">Đăng ký dễ dàng trong một bước</p>
      </div>

      <!-- Form đăng ký -->
      <div class="row g-0 justify-content-center">
        <div class="col-sm-8 col-xl-4">
          <form @submit.prevent="handleSignUp">
            <!-- Username -->
            <div class="mb-4">
              <input
                type="text"
                class="form-control form-control-lg form-control-alt py-3"
                v-model="state.username"
                placeholder="Tên đăng nhập"
                required
              />
            </div>

            <!-- Password -->
            <div class="mb-4">
              <input
                type="password"
                class="form-control form-control-lg form-control-alt py-3"
                v-model="state.password"
                placeholder="Mật khẩu"
                required
              />
            </div>

            <!-- Role -->
            <div class="mb-4">
              <select
                class="form-control form-control-lg form-control-alt py-3"
                v-model="state.role"
                required
              >
                <option value="">Chọn vai trò</option>
                <option value="Admin">Quản trị viên</option>
                <option value="User">Người dùng</option>
              </select>
            </div>

            <!-- Điều khoản sử dụng -->
            <div class="mb-4">
              <div
                class="d-md-flex align-items-md-center justify-content-md-between"
              >
                <div class="form-check">
                  <input
                    class="form-check-input"
                    type="checkbox"
                    id="signup-terms"
                    v-model="state.termsAccepted"
                  />
                  <label class="form-check-label" for="signup-terms">
                    Tôi đồng ý với Điều khoản & Chính sách
                  </label>
                </div>
              </div>
            </div>

            <!-- Nút đăng ký -->
            <div class="text-center">
              <button
                type="submit"
                class="btn btn-lg btn-alt-success"
                :disabled="!state.termsAccepted || !state.role"
              >
                <i class="fa fa-fw fa-plus me-1 opacity-50"></i> Đăng ký
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Thêm các style tùy chỉnh nếu cần */
</style>
