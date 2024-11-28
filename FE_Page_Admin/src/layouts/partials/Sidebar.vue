<template>
  <!-- User Sidebar -->
  <nav class="sidebar">
    <div class="content-header">
      <RouterLink
        :to="{ name: 'user-dashboard' }"
        class="fw-semibold text-dual"
      >
        <span class="smini-visible">
          <i class="fa fa-circle-notch text-primary"></i>
        </span>
        <span class="smini-hide fs-5 tracking-wider">User Panel</span>
      </RouterLink>
    </div>
    <div id="simplebar-sidebar" class="js-sidebar-scroll">
      <div class="content-side">
        <ul class="nav-main">
          <li class="nav-main-item">
            <RouterLink class="nav-main-link" :to="{ name: 'manage-accounts' }">
              <i class="nav-main-link-icon fa fa-user"></i>
              <span class="nav-main-link-name">Quản lý tài khoản</span>
            </RouterLink>
          </li>
          <li class="nav-main-item">
            <RouterLink class="nav-main-link" :to="{ name: 'manage-posts' }">
              <i class="nav-main-link-icon fa fa-file-alt"></i>
              <span class="nav-main-link-name">Quản lý bài viết</span>
            </RouterLink>
          </li>
          <li class="nav-main-item">
            <RouterLink
              class="nav-main-link"
              :to="{ name: 'manage-categories' }"
            >
              <i class="nav-main-link-icon fa fa-tags"></i>
              <span class="nav-main-link-name">Quản lý danh mục</span>
            </RouterLink>
          </li>
        </ul>
      </div>
    </div>
  </nav>
  <!-- END User Sidebar -->
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

// Router instance
const router = useRouter();
</script>

<style scoped>
.sidebar {
  max-width: 250px;
  background-color: #f8f9fa;
}
</style>
