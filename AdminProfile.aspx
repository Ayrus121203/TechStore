<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="TechStore.AdminProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
            $(window).on('load', function () {
                $('#myModal').modal('show');
            });
        </script>
<div class="container-xl px-4 mt-4">
    <!-- Account page navigation-->
    <nav class="nav nav-borders">
        <a class="nav-link active ms-0" href="AdminProfile">Profile</a>
        <a class="nav-link" href="Orders">Orders</a>
    </nav>
    <hr class="mt-0 mb-4">
    <div class="row">
        <div class="col-xl-8">
            <!-- Account details card-->
            <div class="card mb-4">
                <div class="card-header">Account Details</div>
                <div class="card-body">
                    <asp:Repeater ID="rptrUserDetails" runat="server">
                        <ItemTemplate>
                            <!-- Form Group (username)-->
                            <div class="mb-3">
                                <asp:Label ID="Label1" CssClass="small mb-1" runat="server" Text="Username"></asp:Label>
                                <asp:TextBox ID="txtUsername" ReadOnly="true" CssClass="form-control" PlaceHolder="Enter Your Username" Text='<%#Eval("Username") %>' runat="server"></asp:TextBox>
                            </div>
                            <!-- Form Row-->
                            <div class="row gx-3 mb-3">
                                <!-- Form Group (first name)-->
                                <div class="col-md-12">
                                    <asp:Label ID="Label2" CssClass="small mb-1" runat="server" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" ReadOnly="true" CssClass="form-control" PlaceHolder="Enter Your Name" Text='<%#Eval("Name") %>' runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!-- Form Group (email address)-->
                            <div class="mb-3">
                                <asp:Label ID="Label3" CssClass="small mb-1" runat="server" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" ReadOnly="true" CssClass="form-control" PlaceHolder="Enter Your Email" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                            </div>
                            <!-- Form Row-->
                            <div class="row gx-3 mb-3">
                                <!-- Form Group (phone number)-->
                                <div class="col-md-12">
                                    <asp:Label ID="Label4" CssClass="small mb-1" runat="server" Text="Phone Number"></asp:Label>
                                    <asp:TextBox ID="txtPhoneNumber" ReadOnly="true" CssClass="form-control" PlaceHolder="Enter Your Phone Number" Text='<%#Eval("PhoneNumber") %>' runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!-- Save changes button-->
                            
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="row gx-3 mb-3">
                                <!-- Form Group (device address)-->
                                <div class="col-md-12">
                                    <asp:Label ID="Label5" CssClass="small mb-1" runat="server" Text="Device Address"></asp:Label>
                                    <asp:TextBox ID="txtDeviceAddress" ReadOnly="true" CssClass="form-control" TextMode="Password" PlaceHolder="Enable Device Proximity Authentication" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        <asp:Button ID="btnEnableSecurityQues" CssClass="btn btn-secondary" runat="server" Text="Enable Security Question" OnClick="btnEnableSecurityQues_Click" />


                    <!-- Modal -->
                    <section id="displaymodal" runat="server">
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-content">
                                <div class="card-body text-center"> 
                                    <img src="Images/TechStoreLogo.png" height="60" width="200" />
                                    
                                    <div class="comment-box text-center">
                                        <br />
                                        <div>
                                            <br />
                                                <img src="Images/secuirtyquestionsdisplay.png" height="180" width="200" />
                                            <br />
                                        </div>
                                        <br />
                                    <br />
                                        <h4>Choose to enable 1 or more security question with us.</h4>
                                        <br />
                                        <h5>This is useful when recovering your account or changing your password</h5>
                                </div>
                            </div>
                        </div>
                          <div class="modal-body">

                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Later</button>
                            <asp:Button ID="btnRedirectToSecQues" CssClass="btn btn-primary" runat="server" Text="Lets Do It" OnClick="btnRedirectToSecQues_Click" />
                          </div>
                        </div>
                      </div>
                    </div>
        </section>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
