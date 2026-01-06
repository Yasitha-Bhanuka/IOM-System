<%@ Page Title="Branches" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs"
    Inherits="Pages_Branches_List" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Branch Management</h3>
                <button type="button" class="btn btn-primary" onclick="openCreateModal()">
                    <i class="fa-solid fa-plus" style="margin-right: 5px;"></i> Add Branch
                </button>
            </div>
            <div class="card-body" style="padding: 0;">
                <div class="table-responsive">
                    <table class="table" id="branchesTable">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Branch Name</th>
                                <th>Location/City</th>
                                <th>Contact</th>
                                <th>Status</th>
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

        <!-- Simple Modal for Create/Edit -->
        <div id="branchModal" class="modal-overlay"
            style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.5); z-index:1000; justify-content:center; align-items:center;">
            <div class="card" style="width: 400px; margin-top: 100px;">
                <div class="card-header">
                    <h3 class="card-title" id="modalTitle">Add Branch</h3>
                    <button type="button" class="btn btn-secondary btn-sm" onclick="closeModal()">X</button>
                </div>
                <div class="card-body">
                    <form id="branchForm" onsubmit="event.preventDefault(); saveBranch();">
                        <input type="hidden" id="branchId" value="0" />
                        <div class="form-group">
                            <label class="form-label">Branch Name</label>
                            <input type="text" id="branchName" class="form-control" required />
                        </div>
                        <div class="form-group">
                            <label class="form-label">Location</label>
                            <input type="text" id="location" class="form-control" required />
                        </div>
                        <div class="form-group">
                            <div style="display: flex; align-items: center; gap: 0.5rem;">
                                <input type="checkbox" id="isActive" checked />
                                <label for="isActive">Active</label>
                            </div>
                        </div>
                        <div class="text-right" style="margin-top: 1rem;">
                            <button type="button" class="btn btn-secondary" onclick="closeModal()">Cancel</button>
                            <button type="submit" class="btn btn-primary">Save</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>

        <script>
            document.addEventListener('DOMContentLoaded', loadBranches);

            async function loadBranches() {
                const tbody = document.querySelector('#branchesTable tbody');
                try {
                    const branches = await ApiService.get('branches');
                    if (!branches || branches.length === 0) {
                        tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">No branches found.</td></tr>';
                        return;
                    }

                    tbody.innerHTML = branches.map(b => `
                    <tr>
                        <td>${b.branchId}</td>
                        <td style="font-weight:500">${b.branchName}</td>
                        <td>${b.location || '-'}</td>
                        <td>-</td>
                        <td>${b.isActive ? '<span class="badge badge-success">Active</span>' : '<span class="badge badge-danger">Inactive</span>'}</td>
                        <td class="text-right">
                             <button class="btn btn-secondary btn-sm" onclick="editBranch(${b.branchId}, '${b.branchName}', '${b.location}', ${b.isActive})"><i class="fa-solid fa-pen"></i></button>
                             <button class="btn btn-danger btn-sm" onclick="deleteBranch(${b.branchId})"><i class="fa-solid fa-trash"></i></button>
                        </td>
                    </tr>
                `).join('');
                } catch (err) {
                    tbody.innerHTML = `<tr><td colspan="6" style="color:var(--danger)">Error loading branches.</td></tr>`;
                }
            }

            function openCreateModal() {
                document.getElementById('branchId').value = 0;
                document.getElementById('branchName').value = '';
                document.getElementById('location').value = '';
                document.getElementById('isActive').checked = true;
                document.getElementById('modalTitle').innerText = 'Add Branch';
                document.getElementById('branchModal').style.display = 'flex';
            }

            function editBranch(id, name, loc, active) {
                document.getElementById('branchId').value = id;
                document.getElementById('branchName').value = name;
                document.getElementById('location').value = loc;
                document.getElementById('isActive').checked = active;
                document.getElementById('modalTitle').innerText = 'Edit Branch';
                document.getElementById('branchModal').style.display = 'flex';
            }

            function closeModal() {
                document.getElementById('branchModal').style.display = 'none';
            }

            async function saveBranch() {
                const id = parseInt(document.getElementById('branchId').value);
                const dto = {
                    branchId: id,
                    branchName: document.getElementById('branchName').value,
                    location: document.getElementById('location').value,
                    isActive: document.getElementById('isActive').checked
                };

                try {
                    if (id === 0) {
                        await ApiService.post('branches', dto);
                    } else {
                        await ApiService.put(`branches/${id}`, dto);
                    }
                    closeModal();
                    loadBranches();
                } catch (err) {
                    alert('Failed to save branch: ' + err.message);
                }
            }

            async function deleteBranch(id) {
                if (!confirm('Are you sure you want to delete this branch?')) return;
                try {
                    await ApiService.delete(`branches/${id}`);
                    loadBranches();
                } catch (err) {
                    alert('Failed to delete: ' + err.message);
                }
            }
        </script>
    </asp:Content>