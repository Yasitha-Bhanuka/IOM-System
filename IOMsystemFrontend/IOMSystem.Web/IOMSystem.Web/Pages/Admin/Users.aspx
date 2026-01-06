<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="Pages_Admin_Users" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">User Management</h3>
            </div>
            <div class="card-body" style="padding: 0;">
                <div class="table-responsive">
                    <table class="table" id="usersTable">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Username</th>
                                <th>Email</th>
                                <th>Role</th>
                                <th>Branch</th>
                                <th class="text-right">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="6" class="text-center">Loading...</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <script>
            document.addEventListener('DOMContentLoaded', loadUsers);

            async function loadUsers() {
                const tbody = document.querySelector('#usersTable tbody');
                try {
                    const users = await ApiService.get('users');
                    if (!users || users.length === 0) {
                        tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">No users found.</td></tr>';
                        return;
                    }

                    tbody.innerHTML = users.map(u => `
                    <tr>
                        <td>${u.userId}</td>
                        <td style="font-weight:500">${u.username}</td>
                        <td>${u.email}</td>
                        <td>${u.roleId}</td>
                        <td>${u.branchId || '-'}</td>
                        <td class="text-right">
                             <button class="btn btn-secondary btn-sm" onclick="editUser(${u.userId})"><i class="fa-solid fa-pen"></i></button>
                             <button class="btn btn-danger btn-sm" onclick="deleteUser(${u.userId})"><i class="fa-solid fa-trash"></i></button>
                        </td>
                    </tr>
                `).join('');
                } catch (err) {
                    tbody.innerHTML = `<tr><td colspan="6" style="color:var(--danger)">Error loading users.</td></tr>`;
                }
            }

            function editUser(id) {
                alert('Edit user ' + id);
            }

            async function deleteUser(id) {
                if (!confirm('Delete this user?')) return;
                try {
                    await ApiService.delete(`users/${id}`);
                    loadUsers();
                } catch (err) {
                    alert('Delete failed: ' + err.message);
                }
            }
        </script>
    </asp:Content>