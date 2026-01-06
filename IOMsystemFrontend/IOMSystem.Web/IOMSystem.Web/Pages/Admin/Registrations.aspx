<%@ Page Title="Admin Registrations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Registrations.aspx.cs" Inherits="Pages_Admin_Registrations" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Pending Approvals</h3>
                <span class="badge badge-warning" id="pendingCount">0 Pending</span>
            </div>
            <div class="card-body" style="padding: 0;">
                <div class="table-responsive">
                    <table class="table" id="regTable">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Email</th>
                                <th>Role</th>
                                <th>Branch</th>
                                <th>Requested At</th>
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
            document.addEventListener('DOMContentLoaded', loadRegistrations);

            async function loadRegistrations() {
                const tbody = document.querySelector('#regTable tbody');
                try {
                    // Assuming endpoint matches backend: GET /api/registrations/pending
                    const reqs = await ApiService.get('registrations/pending');

                    document.getElementById('pendingCount').innerText = reqs.length + ' Pending';

                    if (!reqs || reqs.length === 0) {
                        tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">No pending registrations.</td></tr>';
                        return;
                    }

                    tbody.innerHTML = reqs.map(r => `
                    <tr>
                        <td>${r.attemptId}</td>
                        <td style="font-weight:500">${r.email}</td>
                        <td>${r.roleId || 'User'}</td>
                        <td>${r.branchId || '-'}</td>
                        <td>${new Date(r.requestDate).toLocaleDateString()}</td>
                        <td class="text-right">
                             <button class="btn btn-primary btn-sm" onclick="approve(${r.attemptId})"><i class="fa-solid fa-check"></i> Approve</button>
                             <button class="btn btn-danger btn-sm" onclick="reject(${r.attemptId})"><i class="fa-solid fa-xmark"></i> Reject</button>
                        </td>
                    </tr>
                `).join('');
                } catch (err) {
                    tbody.innerHTML = `<tr><td colspan="6" style="color:var(--danger)">Error loading registrations.</td></tr>`;
                }
            }

            async function approve(id) {
                if (!confirm('Approve this user?')) return;
                try {
                    // Backend requires ApprovedByUserId in body, hardcoding 1 for now (Admin)
                    await ApiService.post(`registrations/approve/${id}`, 1);
                    loadRegistrations();
                } catch (err) {
                    alert('Approval failed: ' + err.message);
                }
            }

            async function reject(id) {
                const reason = prompt('Enter rejection reason:');
                if (!reason) return;

                try {
                    // Backend requires ActionByUserId and RejectionReason in DTO
                    const dto = {
                        actionByUserId: 1, // Hardcoded Admin ID
                        rejectionReason: reason
                    };
                    await ApiService.post(`registrations/reject/${id}`, dto);
                    loadRegistrations();
                } catch (err) {
                    alert('Rejection failed: ' + err.message);
                }
            }
        </script>
    </asp:Content>