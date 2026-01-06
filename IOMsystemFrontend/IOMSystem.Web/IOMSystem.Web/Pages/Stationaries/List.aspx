<%@ Page Title="Stationaries" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Pages_Stationaries_List" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Stationary Items</h3>
                <button type="button" class="btn btn-primary" onclick="openModal()">
                    <i class="fa-solid fa-plus" style="margin-right: 5px;"></i> Add Item
                </button>
            </div>
            <div class="card-body" style="padding: 0;">
                <div class="table-responsive">
                    <table class="table" id="stationariesTable">
                        <thead>
                            <tr>
                                <th>Code</th>
                                <th>Item Name</th>
                                <th>Type</th>
                                <th>Status</th>
                                <th class="text-right">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="5" class="text-center">Loading...</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div id="itemModal" class="modal-overlay"
            style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.5); z-index:1000; justify-content:center; align-items:center;">
            <div class="card" style="width: 400px; margin-top: 100px;">
                <div class="card-header">
                    <h3 class="card-title" id="modalTitle">Add Item</h3>
                    <button type="button" class="btn btn-secondary btn-sm" onclick="closeModal()">X</button>
                </div>
                <div class="card-body">
                    <form id="itemForm" onsubmit="event.preventDefault(); saveItem();">
                        <div class="form-group">
                            <label class="form-label">Location Code (ID)</label>
                            <input type="text" id="locationCode" class="form-control" required
                                placeholder="e.g. STA-001" />
                        </div>
                        <div class="form-group">
                            <label class="form-label">Item Name</label>
                            <input type="text" id="itemName" class="form-control" required />
                        </div>
                        <div class="form-group">
                            <label class="form-label">Type</label>
                            <select id="itemType" class="form-control">
                                <option value="General">General</option>
                                <option value="Electronics">Electronics</option>
                                <option value="Furniture">Furniture</option>
                            </select>
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
                        <input type="hidden" id="isEdit" value="false" />
                    </form>
                </div>
            </div>
        </div>

        <script>
            document.addEventListener('DOMContentLoaded', loadStationaries);

            async function loadStationaries() {
                const tbody = document.querySelector('#stationariesTable tbody');
                try {
                    const items = await ApiService.get('stationaries');
                    if (!items || items.length === 0) {
                        tbody.innerHTML = '<tr><td colspan="5" style="text-align:center;">No items found.</td></tr>';
                        return;
                    }

                    tbody.innerHTML = items.map(p => `
                    <tr>
                        <td style="font-family:monospace">${p.locationCode}</td>
                        <td style="font-weight:500">${p.stationeryName}</td>
                        <td>${p.stationeryType || 'General'}</td>
                        <td>${p.isActive ? '<span class="badge badge-success">Active</span>' : '<span class="badge badge-danger">Inactive</span>'}</td>
                        <td class="text-right">
                             <button class="btn btn-secondary btn-sm" onclick="editItem('${p.locationCode}', '${p.stationeryName}', '${p.stationeryType}', ${p.isActive})"><i class="fa-solid fa-pen"></i></button>
                             <button class="btn btn-danger btn-sm" onclick="deleteItem('${p.locationCode}')"><i class="fa-solid fa-trash"></i></button>
                        </td>
                    </tr>
                `).join('');
                } catch (err) {
                    tbody.innerHTML = `<tr><td colspan="5" style="color:var(--danger)">Error loading data.</td></tr>`;
                }
            }

            function openModal() {
                document.getElementById('isEdit').value = 'false';
                document.getElementById('locationCode').value = '';
                document.getElementById('locationCode').disabled = false;
                document.getElementById('itemName').value = '';
                document.getElementById('itemType').value = 'General';
                document.getElementById('isActive').checked = true;
                document.getElementById('modalTitle').innerText = 'Add Item';
                document.getElementById('itemModal').style.display = 'flex';
            }

            function editItem(code, name, type, active) {
                document.getElementById('isEdit').value = 'true';
                document.getElementById('locationCode').value = code;
                document.getElementById('locationCode').disabled = true;
                document.getElementById('itemName').value = name;
                document.getElementById('itemType').value = type || 'General';
                document.getElementById('isActive').checked = active;
                document.getElementById('modalTitle').innerText = 'Edit Item';
                document.getElementById('itemModal').style.display = 'flex';
            }

            function closeModal() {
                document.getElementById('itemModal').style.display = 'none';
            }

            async function saveItem() {
                const isEdit = document.getElementById('isEdit').value === 'true';
                const code = document.getElementById('locationCode').value;
                const dto = {
                    locationCode: code,
                    stationeryName: document.getElementById('itemName').value,
                    stationeryType: document.getElementById('itemType').value,
                    isActive: document.getElementById('isActive').checked
                };

                try {
                    if (isEdit) {
                        await ApiService.put(`stationaries/${code}`, dto);
                    } else {
                        await ApiService.post('stationaries', dto);
                    }
                    closeModal();
                    loadStationaries();
                } catch (err) {
                    alert('Failed to save: ' + err.message);
                }
            }

            async function deleteItem(code) {
                if (!confirm('Delete this item?')) return;
                try {
                    await ApiService.delete(`stationaries/${code}`);
                    loadStationaries();
                } catch (err) {
                    alert('Delete failed: ' + err.message);
                }
            }
        </script>
    </asp:Content>