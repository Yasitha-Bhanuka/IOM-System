<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="WebformsUI.Pages.Admin.Users" Async="true" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <h1 class="page-header">User Management</h1>

        <div class="row" style="margin-bottom:20px;">
            <div class="col-md-12">
                <button type="button" class="btn btn-primary" onclick="openCreateModal()">
                    Create New User
                </button>
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="false" GridLines="None" ClientIDMode="Static">

                <Columns>
                    <asp:BoundField DataField="UserId" HeaderText="ID" />
                    <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                    <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:d}" />

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <button type="button" class="btn btn-xs btn-info" onclick=`<%# GetEditUserScript(
                                Eval("UserId"), Eval("UserEmail"), Eval("FullName"), Eval("BranchName"),
                                Eval("RoleName"), Eval("IsActive")) %>`>
                                Edit
                            </button>

                            <button type="button" class="btn btn-xs btn-danger" onclick=`<%#
                                GetDeleteUserScript(Eval("UserId")) %>`>
                                Delete
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="userModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" id="userModalLabel"></h4>
                    </div>

                    <div class="modal-body">
                        <form id="userForm">
                            <input type="hidden" id="userId" />

                            <div class="form-group">
                                <label>Email</label>
                                <input type="email" id="email" class="form-control" required />
                            </div>

                            <div class="form-group" id="passwordGroup">
                                <label>Password</label>
                                <input type="password" id="password" class="form-control" />
                            </div>

                            <div class="form-group">
                                <label>Full Name</label>
                                <input type="text" id="fullName" class="form-control" required />
                            </div>

                            <div class="form-group">
                                <label>Branch</label>
                                <input type="text" id="branch" class="form-control" required />
                            </div>

                            <div class="form-group">
                                <label>Role</label>
                                <select id="role" class="form-control">
                                    <option>User</option>
                                    <option>Admin</option>
                                    <option>Manager</option>
                                </select>
                            </div>

                            <div class="checkbox" id="activeGroup">
                                <label>
                                    <input type="checkbox" id="isActive" /> Is Active
                                </label>
                            </div>
                        </form>
                    </div>

                    <div class="modal-footer">
                        <button class="btn btn-default" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary" onclick="saveUser()">Save</button>
                    </div>

                </div>
            </div>
        </div>

        <script>
            var apiBase = '<%= System.Web.Configuration.WebConfigurationManager.AppSettings["ApiBaseUrl"] %>';

            function openCreateModal() {
                $('#userModalLabel').text('Create User');
                $('#userId').val('');
                $('#email').prop('readonly', false).val('');
                $('#passwordGroup').show();
                $('#password').val('');
                $('#fullName').val('');
                $('#branch').val('');
                $('#role').val('User');
                $('#isActive').prop('checked', true);
                $('#activeGroup').hide();
                $('#userModal').modal('show');
            }

            function openEditModal(id, email, name, branch, role, isActive) {
                $('#userModalLabel').text('Edit User');
                $('#userId').val(id);
                $('#email').val(email).prop('readonly', true);
                $('#passwordGroup').hide();
                $('#fullName').val(name);
                $('#branch').val(branch);
                $('#role').val(role);
                $('#isActive').prop('checked', isActive);
                $('#activeGroup').show();
                $('#userModal').modal('show');
            }

            async function deleteUser(id) {
                if (!confirm('Delete this user?')) return;
                await fetch(apiBase + '/api/Users/' + id, { method: 'DELETE' });
                location.reload();
            }
        </script>

    </asp:Content>